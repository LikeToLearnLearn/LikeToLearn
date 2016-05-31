using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
using System.IO;

public class Recive : MonoBehaviour {

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "127.0.0.1";//"8.8.8.8";//  presentIP; Fix me!!!! 
    private const float waitingTime = 2.0f;
    private const string presentIP = "liketolearn.cloudapp.net"; //"semconliketolearn.cloudapp.net"; ////"192.168.254.154"; // Kurts ipadress

    private Ping ping;
    private float pingStartTime;
   
    private Parser parse;
    public Course c { get; private set; }
    private System.Collections.Generic.List<Course> courseList;
    public bool online;

    private string access_token;
    private string token_type;

    private int errorNumber = 0;
    private bool RecentlyBeenOfLine;
   
    private int version;
    int newVersion;
    private string coursecode;

   
    void Start ()
    {
        RecentlyBeenOfLine = false;

        parse = null;// new Parser(null, null, 0);
        online = false;
        checkOnline();
        c = null;
        coursecode = GameController.control.getCurrentCourseCode();
        if (coursecode != "") version = GameController.control.getCurrentCourseVersion(coursecode);
        else version = -10;

    }

    public void checkOnline()
    {
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }
    
    void Update()
    {
        // Kontroll av internetanslutning
        if (ping != null)
            {
                bool stopCheck = true;
                if (ping.isDone)
                {
                    if (ping.time >= 0)
                        InternetAvailable();
                    else
                        InternetIsNotAvailable();
                }
                else if (Time.time - pingStartTime < waitingTime)
                    stopCheck = false;
                else
                    InternetIsNotAvailable();
                if (stopCheck)
                ping = null
                ;
            }
         //Om det kommer en ny version på en gammal kurs
        if (parse != null && parse.HasNewVersion && parse.coursecode == GameController.control.getCurrentCourseCode())
        {
            int v = int.Parse(parse.version);
            //Debug.Log("Den gamla versionen av kurs " + parse.coursecode + " är " + GameController.control.getCurrentCourseVersion(parse.coursecode) + " och den nya verionen är " + v + ".");

           
            if (v != GameController.control.getCurrentCourseVersion(parse.coursecode))
            {
                //Debug.Log("Den verion som tas emot i recive är: " + v);
                GameController.control.setCurrentCourseVersion(parse.coursecode, v);

                getNewQuestions(GameController.control.name);
                getCurrentMoment(GameController.control.name);

                parse.HasNewVersion = false;
                parse.HasNewCourseCode = false;
            }
        }
        // Om det kommer en ny kurs
        if (parse != null && parse.HasNewCourseCode && parse.coursecode != GameController.control.getCurrentCourseCode())
        {
            Debug.Log("Det kommer en ny kurs med kurskod " + parse.coursecode);
            getNewQuestions(GameController.control.name);
            getCurrentMoment(GameController.control.name);
            parse.HasNewCourseCode = false;
        }
    }

    public bool Online()
    {
        checkOnline();   
        return online;
    }
  

    private IEnumerator WaitForRequest(WWW www, System.Collections.Generic.List<Course> courseList, int function)
    {
        yield return www;
        
        Debug.Log("I WaitForRequest tas " + www.text + " emot.");
        
        if (www.error == null)
        {
            //Debug.Log("Waitforrequest: www.text: " + www.text);
            if (www.text.Length > 0)
            {
                parse = new Parser(www.text, courseList, function); 
               
                c = parse.c;
                if (c != null) Debug.Log("Recive.c = " + c + " i Recive.cs:s WaitForRequest. Den kursen har kurskod:  " + c.getCoursecode() + " Exempel på en fråga är: " + c.GetQuestion(4).question + " Den frågan kan tex ha frågeID:t: " + c.GetQuestion(4).questionId);
                if (parse.HasNewAccess_token)
                {
                    access_token = parse.access_token;
                }

                if (parse.HasNewToken_type)
                {
                    token_type = parse.token_type;
                }

            }
            else
            {
                Debug.Log("no data received");
            }

        }
        else
        {
            errorNumber++;
            Debug.LogError(errorNumber +": Failed to fetch data in recive.waitforrequest: " + www.error);
        }
    }

          
    private void InternetIsNotAvailable()
    {
        Debug.Log("No Internet :(");
        online = false;
        RecentlyBeenOfLine = true;   
    }


    private void InternetAvailable()
    {
        Debug.Log("Internet is available! ;)");
        online = true;
        if (RecentlyBeenOfLine) authentication();
        RecentlyBeenOfLine = false;
    }

    public void setCourseList(System.Collections.Generic.List<Course> coruses)
    {
        courseList = coruses;
    }

    public void authentication()
    {
        if (online)                                          //Kontroller görs att spelet har kontakt med internet
        {
            WWWForm form = new WWWForm();
            form.AddField("username", "jlong");              // Hårdkodat användarnamn
            form.AddField("password", "chalmers2016!");      // Hårdkodat lösenord
            form.AddField("grant_type", "password");        
            byte[] rawData = form.data;
                                                            // En url för rätt flik i REST API:n skapas:
            string url = string.Format(presentIP + ":8181/oauth/token");    

                                                             // Servernamn och serverlösenord kodas:
            String encoded = System.Convert.ToBase64String
                (System.Text.Encoding.ASCII.GetBytes("liketolearn-restapi:123456"));

            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Basic " + encoded); // Det kodade serverlösenordet läggs i headers  
       
            if (!GameController.control.testmode)            // Kontroll om spelet befinner sig i testmode
            {
                WWW www = new WWW(url, rawData, headers);                   // Anropet genomförs   
                StartCoroutine(WaitForRequest(www, courseList, 2));         // Unity börjar vänta på svar
            }
        }
    }

    public bool Login(string username, string password)
    {
       Dictionary<String, String> headers1 = new Dictionary<string, string>();
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);

        WWWForm form = new WWWForm();
        form.AddField("password", password);
        form.AddField("userid", username);

        byte[] rawData1 = form.data;

        string url = string.Format(presentIP + ":8181/liketolearn/login");
        
        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, null, 1));
        }
        else return true;
        
        return parse.Authorization(null); 
    }


    public void checkCourseAndVersion(string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);
        string url1 = string.Format(presentIP + ":8181/liketolearn/version");

        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        headers1.Add("Authorization", token_type + " " + access_token);

        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url1, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, courseList, 2));
        }
    }

    public bool getCurrentMoment(string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);

        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/finishedmoments");
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);

        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url1, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, courseList, 2));
        }
        return parse.HasNewResult;
    }


    public void getNewQuestions(string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);

        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/questions");
        headers1.Add("Authorization", token_type + " " + access_token);

        //Debug.Log("Recives getNewQuestions körs" );

        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url1, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, courseList, 2));
        }
    }


    public void sendStatistics(string questionID, string answer, string trueOrFalse, string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("questionid", questionID);
        form1.AddField("answer", answer);
        form1.AddField("correctness", trueOrFalse);
        form1.AddField("userid", userid);
        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/statistics");
        headers1.Add("Authorization", token_type + " " + access_token);


        if (online && !GameController.control.testmode)
        {
            var www = new WWW(url1, rawData1, headers1);
        }         
    }


    public void DoneMoment(string userid, string momentcode, float time)
    {        
        int takenSeconds = (int) time;
        int minutes = 0;
        int hours = 0;
        int seconds = 0;
        
        hours = takenSeconds / 3600;
        minutes = (takenSeconds % 3600) / 60;
        seconds = (takenSeconds % 3600) % 60;

        string t = "";
        if (hours != 0) t = hours + "h ";
        if (minutes != 0) t = t + minutes + "min ";
        if( hours == 0) t = t + seconds + "s";

        Debug.Log("I DoneMoment registeraras att " + userid + " har klarat moment " + momentcode + " på tiden " + t);

        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);
        form1.AddField("momentcode", momentcode);
        form1.AddField("time", t);
        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/time");
        headers1.Add("Authorization", token_type + " " + access_token);

        if (online && !GameController.control.testmode)
        {
            var www = new WWW(url1, rawData1, headers1);
        }

    }


    

   
}

   


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
    private const string pingAddress = presentIP; 
    private const float waitingTime = 2.0f;
    private const string presentIP = "127.0.0.1"; // "192.168.254.154"; // Kurts ipadress

    private Ping ping;
    private float pingStartTime;

    public string newCoursecode { get; private set; }
    public System.String newMomentcode { get; private set; }
    public string newQuestionc { get; private set; }
    public string newAnswerc { get; private set; }

    public string newCourse { get; private set; }
    public int newMoment { get; private set; }
    public string newQuestion { get; private set; }
    public string newAnswer { get; private set; }

    public bool HasNewCourse { get; private set; } // indicated if the object is carrying a new course
    public bool HasNewMoment { get; private set; } // indicated if the object is carrying an moment
    public bool HasNewQuestion { get; private set; } // indicated if the object is carrying an question
    public bool HasNewAnswer { get; private set; } // indicated if the object is carrying an answer

    private Parser parse;
    public Course c { get; private set; }
    private System.Collections.Generic.List<Course> courseList;
    private bool online;

    private string access_token;
    private string token_type;
    private string refresh_token;
    private int errorNumber = 0;

    // Use this for initialization
    void Start ()
    {
        HasNewCourse = false;
        HasNewMoment = false;
        HasNewQuestion = false;
        HasNewAnswer = false;

        parse = new Parser(null, null, 0);
        online = false;
        checkOnline();
        c = null; 

    }

    public /*bool*/ void checkOnline()
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

    // Update is called once per frame
    void Update()
    {
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
    }

    public bool Online()
    {
        checkOnline();   
        return online;
    }

  /*  void ConnectToServer()
    {
        Network.Connect(presentIP, 8080);
    }

    public void UploadJSON()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "data065");

        WWW www = new WWW(presentIP + ":8080/greeting", form);
        Debug.Log("Nu försöker jag skicka: " + form + " till greeting");

        testJson();

    }*/

  /*  public void testJson()
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("coursecode", "iugu");
        form1.AddField("momentcode", "9845u");
        form1.AddField("question", "1 + 2");
        form1.AddField("answer", "3");

        string url = string.Format(presentIP +":8080/test");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till test");
        StartCoroutine(WaitForRequest(www, courseList));

    }*/

  

    private IEnumerator WaitForRequest(WWW www, System.Collections.Generic.List<Course> courseList, int function)
    {
        //Debug.Log("Nu är vi i WaitForRequest innan första yield return");
        yield return www;
        Debug.Log("Nu är vi i WaitForRequest efter yield return. www.text = " + www.text);
        if (www.error == null)
        {
            //Debug.Log("Waitforrequest: www.text: " + www.text);
            if (www.text.Length > 0)
            {
                //var 
                parse = new Parser(www.text, courseList, function); 
               
                Debug.Log(www.text + " was received i Recive.cs");
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

                if (parse.HasNewRefresh_token)
                {
                    refresh_token = parse.refresh_token;
                }


                //else Debug.Log(www.text + " blev authotization");
                /* if (parse.HasNewResult)
                {
                    newCoursecode = parse.coursecode;
                    Debug.Log(newCoursecode + " was received i Recive.cs");
                    newMomentcode = parse.momentcode;
                    Debug.Log(newCoursecode + " was received i Recive.cs");
                    newQuestion = parse.question;
                    Debug.Log(newQuestion + " was received i Recive.cs");
                    newAnswer = parse.answer;
                    Debug.Log(newAnswer + " was received i Recive.cs");
                    //createNewCourse();
                    //parse.HasNewResult = false;
                 }*/

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

  /*  void createNewCourse()
    {
        c = new CurrentCourse(newCoursecode);
        Debug.Log(c + "  c i createNewCourse i Recive.cs");
        c.setCoursecode(newCoursecode);
        c.setLevel(int.Parse(newMomentcode));
                
        if (newCoursecode!= null && courseList!= null)
        {
            bool newCourse = true;
            foreach (Course co in courseList)
            {
                Debug.Log(co + " co in createNewCourse");
                if (co.getCoursecode().Equals(newCoursecode))
                {
                    newCourse = false;
                    c = co;
                }
            }

            if (newCourse)
            {
                HasNewCourse = true;
                courseList.Add(c);
            }
            createNewMoment(c);
        }
    }*/

   /* void createNewMoment(Course c)
    {
        if (!c.momentcodes.ContainsKey(int.Parse(newMomentcode)))
        {
            int x = 1;
            foreach (int y in c.levels) x++;
            c.momentcodes.Add(int.Parse(newMomentcode), x + 1 );
            
        }
        int level = c.momentcodes[int.Parse(newMomentcode)]; //int.Parse(newMomentcode);

        Debug.Log(level + newQuestion + newAnswer + " was received i createNewMoment i Recive.cs");
        c.AddQuestion(level, newQuestion, newAnswer);
    }*/
      
    private void InternetIsNotAvailable()
    {
        Debug.Log("No Internet :(");
        online = false;
        
    }

    private void InternetAvailable()
    {
        Debug.Log("Internet is available! ;)");
        online = true;
    }

    public void setCourseList(System.Collections.Generic.List<Course> coruses)
    {
        courseList = coruses;
    }

    public void sendResult(string questionID, string answer, string rightOrWrong, string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("questionid", questionID);
        //form1.AddField("question", question);
        form1.AddField("answer", answer);
        form1.AddField("correctness", rightOrWrong);
        form1.AddField("userid", userid);
        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8080/liketolearn/statistics");
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);

        var www = new WWW(url1, rawData1, headers1);
        //Debug.Log("Nu försöker jag skicka: " + form1 + " till statistics");

        //StartCoroutine(WaitForRequest(www, null, 2));
                 
    }

    public void authentication(/*string username, string password*/)
    {
        // curl -X POST -vu liketolearn-restapi:123456 http://localhost:8080/oauth/token -H "Accept: application/json" -d "password=chalmers2016!&username=jlong&grant_type=password&scope=write&client_secret=123456&client_id=liketolearn-restapi"

        //if (username == "") username = "jlong";
        //if (password == "") password = "chalmers2016!";
        if (online)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", "jlong");
            form.AddField("password", "chalmers2016!");

            form.AddField("grant_type", "password");

            Dictionary<String, String> headers = new Dictionary<string, string>();
            byte[] rawData = form.data;
            string url = string.Format(presentIP + ":8080/oauth/token");

            String encoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("liketolearn-restapi:123456"));
            //Debug.Log("krypterad grej: " + encoded);
            headers.Add("Authorization", "Basic " + encoded);


            // Post a request to an URL with our custom headers
            WWW www = new WWW(url, rawData, headers);
            //Debug.Log("I authentication");
            StartCoroutine(WaitForRequest(www, courseList, 2));
        }
            //--------------------------------------------------------------------------------------------
    }




    public bool getNewQuestions(string userid)
    {
        /* string url = string.Format(presentIP + ":8080/questions");
         var www = new WWW(url);
         StartCoroutine(WaitForRequest(www, courseList));
         if (parse == null) return false;*/

        //--------------------Över stecket finns koden som fungerade innan vi la till login

        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);

        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8080/liketolearn/questions");
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);


        // Post a request to an URL with our custom headers
        WWW www1 = new WWW(url1, rawData1, headers1);
        Debug.Log("I recive.GetQuestion: access_token = " + access_token + ", token_type = " + token_type + ", refresh_token = " + refresh_token);
        StartCoroutine(WaitForRequest(www1, courseList, 2));
        //------------------------------

        //return true; // 
        return parse.HasNewResult;
    }

    public bool Authorization(string username, string password)
    {
           
       Dictionary<String, String> headers1 = new Dictionary<string, string>();
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);

        WWWForm form = new WWWForm();
        form.AddField("password", password);
        form.AddField("userid", username);

        byte[] rawData1 = form.data;

        string url = string.Format(presentIP + ":8080/liketolearn/login");
        WWW www1 = new WWW(url, rawData1, headers1);
        ////var www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www1, null, 1));

        //while (!parse.HasCheckedLoggin) Debug.Log(" Nu väntar vi på resultat från loggin");
        Debug.Log("Svaret blev: " + parse.authorization);

       return parse.authorization; 
        
    }

}

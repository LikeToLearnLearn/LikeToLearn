﻿using UnityEngine;
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
    private const string presentIP =  "semconliketolearn.cloudapp.net"; //"127.0.0.1"; //"192.168.254.154"; // Kurts ipadress

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
    public bool online;

    private string access_token;
    private string token_type;
    private string refresh_token;
    private int errorNumber = 0;
    private bool RecentlyBeenOfLine;
    private bool done;


    // Use this for initialization
    void Start ()
    {
        HasNewCourse = false;
        HasNewMoment = false;
        HasNewQuestion = false;
        HasNewAnswer = false;
        RecentlyBeenOfLine = false;

        parse = new Parser(null, null, 0);
        online = false;
        checkOnline();
        c = null;
        done = false;

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
  

    private IEnumerator WaitForRequest(WWW www, System.Collections.Generic.List<Course> courseList, int function)
    {
        done = false;
        //Debug.Log("Nu är vi i WaitForRequest innan första yield return");
        yield return www;
        done = true;
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

    public void sendStatistics(string questionID, string answer, string trueOrFalse, string userid)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("questionid", questionID);
        //form1.AddField("question", question);
        form1.AddField("answer", answer);
        form1.AddField("correctness", trueOrFalse);
        form1.AddField("userid", userid);
        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/statistics");
        headers1.Add("Authorization", token_type + " " + access_token);


        //Debug.Log("Nu försöker jag skicka: " + form1 + " till statistics");

        if (online && !GameController.control.testmode)
        {
            var www = new WWW(url1, rawData1, headers1);
            //StartCoroutine(WaitForRequest(www, null, 2));
        }         
    }

    public void authentication(/*string username, string password*/)
    {
        // curl -X POST -vu liketolearn-restapi:123456 http://localhost:8080/oauth/token -H "Accept: application/json" -d "password=chalmers2016!&username=jlong&grant_type=password&scope=write&client_secret=123456&client_id=liketolearn-restapi"

        //if (username == "") username = "jlong";
        //if (password == "") password = "chalmers2016!";
        if (online)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", "jlong");// Kryptera ??
            form.AddField("password", "chalmers2016!"); // Kryptera ??

            form.AddField("grant_type", "password");

            Dictionary<String, String> headers = new Dictionary<string, string>();
            byte[] rawData = form.data;
            string url = string.Format(presentIP + ":8181/oauth/token");

            String encoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("liketolearn-restapi:123456")); // riktig kryptering???
            //Debug.Log("krypterad grej: " + encoded);
            headers.Add("Authorization", "Basic " + encoded);


            // Post a request to an URL with our custom headers

            //Debug.Log("I authentication");
            if (online && !GameController.control.testmode)
            {
                WWW www = new WWW(url, rawData, headers);
                StartCoroutine(WaitForRequest(www, courseList, 2));
            }
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

        Debug.Log(" Userid i recive är nu = " + userid);
        WWWForm form1 = new WWWForm();
        form1.AddField("userid", userid);

        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        byte[] rawData1 = form1.data;
        string url1 = string.Format(presentIP + ":8181/liketolearn/questions");
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);


        // Post a request to an URL with our custom headers

        //Debug.Log("I recive.GetQuestion: access_token = " + access_token + ", token_type = " + token_type + ", refresh_token = " + refresh_token);
        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url1, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, courseList, 2));
        }
        //------------------------------

        //return true; // 
        return parse.HasNewResult;
    }

    public void DoneMoment(string userid, string momentcode, float time)
    {
        float hour = time / 3600;
        string h = hour.ToString("f0");
        float minuts = (time - (hour * 3600)) / 60;
        string min = minuts.ToString("f0");
        float seconds = minuts - (minuts * 60);
        string s = seconds.ToString("f0");
        string t = h + "h " + min + "min " + s + " seconds";
        Debug.Log(userid + " har klarat momentet: " + momentcode + ", på tiden " + t);

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
            //StartCoroutine(WaitForRequest(www, null, 2));
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

        ////var www = new WWW(url, form);
        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url, rawData1, headers1);
            StartCoroutine(WaitForRequest(www1, null, 1));

        }
        else return true;
        //Debug.Log(" Nu väntar vi på resultat från loggin"); // sätta en wait, tills någon väcker upp den... semaforer??? // ha en timeout ca 5 ca
        /* int sc = 0;
       while (!parse.HasCheckedLoggin && sc < 100)
        {
            while (sc < 100 && !parse.HasCheckedLoggin) { System.Threading.Thread.Sleep(10); sc++; }
        }*/
        //System.Threading.Thread.Sleep(1000);

        //StartCoroutine(DoLast());
        
        Debug.Log("Svaret blev: " + parse.Authorization(null));// parse.authorization);

       return parse.Authorization(null); 
        
    } 




    public IEnumerator CheckLogin(string username, string password)
    {
    
        Dictionary<String, String> headers1 = new Dictionary<string, string>();
        headers1.Add("Authorization", token_type /*"Bearer"*/ + " " + access_token);

        WWWForm form = new WWWForm();
        form.AddField("password", password);
        form.AddField("userid", username);

        byte[] rawData1 = form.data;

        string url = string.Format(presentIP + ":8181/liketolearn/login");

        ////var www = new WWW(url, form);
        if (online && !GameController.control.testmode)
        {
            WWW www1 = new WWW(url, rawData1, headers1);
            yield return www1;
            if(www1.text == "true")
            {
                done = true;
                yield return "success";
            }
            else {
                yield return "fail";
                done = false;
            }

        }
    }


    public bool Loogin()
    {
        Debug.Log(" done = " + done);
        return done;
        
    }
}

   

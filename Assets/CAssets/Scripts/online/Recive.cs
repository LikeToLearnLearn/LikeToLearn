using UnityEngine;
using System.Collections;using UnityEngine;


    

public class Recive : MonoBehaviour {

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = presentIP; 
    private const float waitingTime = 2.0f;
    private const string presentIP = "192.168.254.169"; // Kurts ipadress

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

    


    // Use this for initialization
    void Start ()
    {
        HasNewCourse = false;
        HasNewMoment = false;
        HasNewQuestion = false;
        HasNewAnswer = false;

        parse = new Parser(null);
        checkOnline(); 

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
        //checkOnline();
        return online;
    }

    void ConnectToServer()
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

    }

    public void testJson()
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("coursecode", "iugu");
        form1.AddField("momentcode", "9845u");
        form1.AddField("question", "1 + 2");
        form1.AddField("answer", "3");

        string url = string.Format(presentIP +":8080/school");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till school");
        StartCoroutine(WaitForRequest(www));

    }

    public bool getNewQuestions()
    {
        string url = string.Format(presentIP + ":8080/test");
        var www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return parse.HasNewResult;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            if (www.text.Length > 0)
            {
                parse = new Parser(www.text);
                Debug.Log(www.text + " was received i Recive.cs");
                if (parse.HasNewResult)
                {
                    newCoursecode = parse.coursecode;
                    newMomentcode = parse.momentcode;
                    newQuestion = parse.question;
                    newAnswer = parse.answer;
                    createNewCourse();
                 }
                                                                                                
            }
            else
            {
                Debug.Log("no data received");
            }

        }
        else
        {
            Debug.LogError("Failed to fetch data: " + www.error);
        }

    }

    void createNewCourse()
    {
        c = new CurrentCourse(newCoursecode);
        c.setCoursecode(newCoursecode);
                
        if (newCoursecode!= null)
        {
            bool newCourse = true;
            foreach (Course co in courseList)
            {
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
    }

    void createNewMoment(Course c)
    {
        int level = int.Parse(newMomentcode);           
        c.AddQuestion(level, newQuestion, newAnswer);
    }
      
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

}

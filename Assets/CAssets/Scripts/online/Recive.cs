using UnityEngine;
using System.Collections;using UnityEngine;


    

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

    


    // Use this for initialization
    void Start ()
    {
        HasNewCourse = false;
        HasNewMoment = false;
        HasNewQuestion = false;
        HasNewAnswer = false;

        parse = new Parser(null);
        online = false;
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

        string url = string.Format(presentIP +":8080/test");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till test");
        StartCoroutine(WaitForRequest(www));

    }

    public bool getNewQuestions()
    {
        string url = string.Format(presentIP + ":8080/questions");
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
                    Debug.Log(newCoursecode + " was received i Recive.cs");
                    newMomentcode = parse.momentcode;
                    Debug.Log(newCoursecode + " was received i Recive.cs");
                    newQuestion = parse.question;
                    Debug.Log(newQuestion + " was received i Recive.cs");
                    newAnswer = parse.answer;
                    Debug.Log(newAnswer + " was received i Recive.cs");
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
        Debug.Log(c + "  c i createNewCourse i Recive.cs");
        c.setCoursecode(newCoursecode);
                
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
    }

    void createNewMoment(Course c)
    {
        int level = int.Parse(newMomentcode);
        Debug.Log(level + newQuestion + newAnswer + " was received i createNewMoment i Recive.cs");
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

    public void sendResult(string coursecode, string momentcode, string question, string answer, string rightOrWrong)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("coursecode", coursecode);
        form1.AddField("momentcode", momentcode);
        form1.AddField("question", question);
        form1.AddField("answer", answer);
        form1.AddField("correctness", rightOrWrong);

        string url = string.Format(presentIP + ":8080/statistics");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till statistics");
        //StartCoroutine(WaitForRequest(www));

    }

}

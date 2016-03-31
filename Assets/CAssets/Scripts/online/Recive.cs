using UnityEngine;
using System.Collections;


public class Recive : MonoBehaviour {

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

    public Recive(System.Collections.Generic.List<Course> coruses)
    {
        parse = new Parser(null);
        courseList = coruses;
        c = null;
        
    }

    public void setCourseList(System.Collections.Generic.List<Course> coruses)
    {
        courseList = coruses;
    }


    // Use this for initialization
    void Start ()
    {
        HasNewCourse = false;
        HasNewMoment = false;
        HasNewQuestion = false;
        HasNewAnswer = false;

        parse = new Parser(null);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void ConnectToServer()
    {
        Network.Connect("192.168.254.169", 8080);
    }

    public void UploadJSON()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "data065");

        WWW www = new WWW("127.0.0.1:8080/greeting", form);
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

        string url = string.Format("127.0.0.1:8080/school");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till school");
        StartCoroutine(WaitForRequest(www));

    }

    public bool getNewQuestions()
    {
        string url = string.Format("127.0.0.1:8080/test");
        var www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return parse.HasResult;
    }

    private IEnumerator WaitForRequest(/*Action<EricssonApiParser> fn,*/ WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            if (www.text.Length > 0)
            {
                parse = new Parser(www.text);
                Debug.Log(www.text + " was received i Recive.cs");
                if (parse.HasResult)
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

        Debug.Log("Nu försöker jag skapa en ny kurs i createNewCourse. Den blev: " + c);
        Debug.Log("Nu försöker jag sätta kurskoden i " + c + "till" + newCoursecode + "Detta görs i createNewCourse");

        if (newCoursecode!= null)
        {
            bool newCourse = true;
            Debug.Log("Innan jag går in i loopen i createNewCourse är courselist = " + courseList);
            foreach (Course co in courseList)
            {
                Debug.Log("En kurs som ligger i courseList har kurskoden: " + co.getCoursecode());
                if (co.getCoursecode().Equals(newCoursecode))
                {
                    Debug.Log("Det finnsen kurs med samma kurskod som jag letar efter i createNewCourse. Koden är: " + newCoursecode);
                    newCourse = false;
                    c = co;
                    Debug.Log("Nu försöker jag en gammal kurs i createNewCourse. Den blev: " + c);
                }
            }

            if (newCourse)
            {
                HasNewCourse = true;
                courseList.Add(c);
                Debug.Log("När jag är nästan klar i createNewCourse ser courseList ut så här: " + courseList);

            }
            createNewMoment(c);
            
        }
    }

    void createNewMoment(Course c)
    {
        Debug.Log("newMomentcode i början av createNewMoment är: " + newMomentcode);
        //Debug.Log("Vad blir System.Int32.Parse(newMomentcode) i createNewMoment??? Jo: " + System.Int32.Parse(newMomentcode)); // Seriöst!!!!

        int level = 1;// int.Parse(newMomentcode);
        Debug.Log("Level som skapas i createNewMoment är: " + level);

       /* if (!c.questions.ContainsKey(level))
        {
            Debug.Log("Nu försöker jag lägga till en ny level i createNewMoment. Den bör bli: " + level + " Innan ser c.questions[level] ut så här: " +  c.questions[level].ToString());
            c.questions[level] = new System.Collections.Generic.List<string>();
            Debug.Log("I createNewMoment ser c.questions[level] ut så här efter att jag till level : " + c.questions[level].ToString());

            HasNewMoment = true;
        }*/
        Debug.Log("I createNewMoment försöker jag skapa en ny fråga med level: " + level + " , och frågan: " + newQuestion + ", och svaret: " + newAnswer);
        c.AddQuestion(level, newQuestion, newAnswer);
        //createNewQuestion(c, level);
    }

    void createNewQuestion(Course c, int level)
    {
        Debug.Log("I createNewQuestion försöker jag skapa en ny fråga med level: " + level + " , och frågan: " + newQuestion + ", och svaret: " + newAnswer);
        c.AddQuestion(level, newQuestion, newAnswer);
        HasNewQuestion = true;
    }

}

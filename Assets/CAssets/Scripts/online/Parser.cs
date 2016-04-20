using UnityEngine;
using System.Collections;
using System.Linq;

public class Parser {

    public string coursecode { get; private set; } 
    public string momentcode { get; private set; } 
    public string question { get; private set; }
    public string answer { get; private set; }
    public string questionID { get; private set; }
    public string access_token { get; private set; }
    public string token_type { get; private set; }
    public string refresh_token { get; private set; }

    private bool HasResult; // indicated if the object is carrying an result
    public bool HasNewResult { get; set; } // indicated if the object is carrying a new result
    public bool HasNewAccess_token { get; set; } // indicated if the object has a new access_token
    public bool HasNewToken_type { get; set; } // indicated if the object has a new token_type
    public bool HasNewRefresh_token { get; set; } // indicated if the object has a new token_type


    private enum State { open, array, obj }; // keeps track of the mode the parser has reached 
    public System.Collections.Generic.List<Course> courseList { get; set; }
    public Course c { get; private set; }
    private int defaultAnswer = 0;
    public bool authorization{ get; set; }
    public bool HasCheckedLoggin;
   

    public Parser(string data, System.Collections.Generic.List<Course> courseList, int function)
    {
        this.courseList = courseList;
        coursecode = string.Empty; ;
        momentcode = string.Empty;
        question = string.Empty;
        answer = string.Empty;
        questionID = string.Empty;

        HasResult = false;
        HasNewResult = false;
        HasNewAccess_token = false;
        HasNewRefresh_token = false;
        authorization = false;
        if (data != null && function == 1) //Fix me!!
            Authorization(data);
        if (data != null && function == 2) //Fix me!!
            parseJson(data);

        //else if (data != null) Authorization(data); // Fix me!!
        HasCheckedLoggin = false;

        //else if (data != null) Authorization(data);
        //Debug.Log("Vi är i parser");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void parseJson(string data)
    {
        //Debug.Log("Vi är i början av parseJson");
        State st = State.open; // start outside any data structure
        string newCoursecode = string.Empty; // most recently read timestamp that may be saved
        string newMomentcode = string.Empty; // most recently read value that may be saved
        string newQuestion = string.Empty; 
        string newAnswer = string.Empty;
        string newQuestionID = string.Empty;
        string newAccess_token = string.Empty;
        string newToken_type = string.Empty;
        string newRefreshToken = string.Empty;

        bool save = false; // indicates if the candidate value should replace the result value

         for (int i = 0; i < data.Length; i++) // iterate over the characters
         {
            //Debug.Log(" data i parser: " + data);
             switch (st)
             {
                 case State.open:
                     if (data[i] == '{') // look for opening JSON array // Fix me!! [
                         st = State.array; // switch to array state
                    
                    break;
                 case State.array:
                     if (data[i] == '"') // look for opening JSON object // Fix me!! {
                         st = State.obj; // switch to object state
                     save = false; // new object; reset save state
                    
                    break;
                 case State.obj:
                    if (data[i] == '}') // looking for end of JSON object
                     {
                         if (save) // the object read should replace the return values
                         {
                            if (coursecode != newCoursecode && newCoursecode != null)
                            {
                                coursecode = newCoursecode;
                                HasNewResult = true;
                            }

                            if (momentcode != newMomentcode && newMomentcode != null )
                            {
                                momentcode = newMomentcode;
                                HasNewResult = true;
                            }

                            if (question != newQuestion && newQuestion != null)
                                                {
                                question = newQuestion;
                                HasNewResult = true;
                            }

                            if (answer != newAnswer && newAnswer != null)
                            {
                                
                                if (newAnswer == "ul")
                                {
                                    defaultAnswer++;
                                    answer = "" + defaultAnswer;
                                }
                                else answer = newAnswer;
                                HasNewResult = true;
                            }
                            if (questionID != newQuestionID && newQuestionID != null)
                            {
                                questionID = newQuestionID;
                                HasNewResult = true;
                            }
                            if (access_token != newAccess_token && newAccess_token != null && newAccess_token != "")
                            {
                                access_token = newAccess_token;
                                HasNewAccess_token = true;
                            }
                            if (token_type!= newToken_type && newToken_type != null && newToken_type != "")
                            {
                                token_type = newToken_type;
                                HasNewToken_type = true;
                            }
                           if (refresh_token != newRefreshToken && newRefreshToken != null)
                            {                             
                                refresh_token = newRefreshToken;
                                HasNewRefresh_token = true;
                            }

                            if (HasNewResult)
                            { 
                                Debug.Log("Tillfälligt i Parser: corsecode sparas som: " + coursecode + ", momentcode sparas som: " + momentcode + " questionID sparas som: " + questionID + ", question sparas som: " + question + ", answer sparas som: " + answer);
                                createNewCourse();
                                HasNewResult = false;
                                save = false;
                            }
                            if(HasNewAccess_token)
                            {
                                Debug.Log(" Det finns ett nytt access_token. Det är: " + access_token);
                                //HasNewAccess_token = false;
                            }
                            if (HasNewToken_type)
                            {
                                Debug.Log(" Det finns en ny token_type. Det är: " + token_type);
                                //HasNewRefresh_token = false;
                            }
                            if (HasNewRefresh_token)
                            {
                                Debug.Log(" Det finns en ny refresh_token. Det är: " + refresh_token);
                            }
                        }

                     }
                    //Debug.Log("Vi är i parsern men inte i någon if-sats.");
                    if (data[i] == 'a' && data[i + 1] == 'c' && data[i + 2] == 'c' && data[i + 3] == 'e' && data[i + 4] == 's' && data[i + 5] == 's' && data[i + 6] == '_' && data[i + 7] == 't' && data[i + 8] == 'o' && data[i + 9] == 'k' && data[i + 10] == 'e' && data[i + 11] == 'n')
                    {
                        //Debug.Log(" Vi kom in i access_tokens if-sats.");
                        i += "access_token\":".Length; // skip forward to the access_token data
                        int j = i;
                        while (data[j] != ',') // find end of acess_token data
                            j++;
                        newAccess_token = (data.Substring(i + 1, j - (i + 2))); // parse access_token

                        i = j; // jump
                    }
                    if (data[i] == 't' && data[i + 1] == 'o' && data[i + 2] == 'k' && data[i + 3] == 'e' && data[i + 4] == 'n' && data[i + 5] == '_' && data[i + 6] == 't' && data[i + 7] == 'y' && data[i + 8] == 'p' && data[i + 9] == 'e')
                    {
                        //Debug.Log(" Vi kom in i tokens_types if-sats.");
                        i += "token_type\":".Length; // skip forward to the token_type data
                        int j = i;
                        while (data[j] != ',') // find end of token_type data
                            j++;
                       newToken_type = (data.Substring(i + 1, j - (i + 2))); // parse token_type

                        i = j; // jump
                    }
                    if (data[i] == 'r' && data[i + 1] == 'e' && data[i + 2] == 'f' && data[i + 3] == 'r' && data[i + 4] == 'e' && data[i + 5] == 's' && data[i + 6] == 'h' && data[i + 7] == '_' && data[i + 8] == 't' && data[i + 9] == 'o' && data[i + 10] == 'k' && data[i + 11] == 'e' && data[i + 12] == 'n')
                    {
                        //Debug.Log(" Vi kom in i tokens_types if-sats.");
                        i += "refresh_token\":".Length; // skip forward to the refresh_token data
                        int j = i;
                        while (data[j] != ',') // find end of refresh_token data
                            j++;
                        newRefreshToken = (data.Substring(i + 1, j - (i + 2))); // parse refresh_token

                        i = j; // jump
                    }

                    if (data[i] == 'c' && data[i + 1] == 'o' && data[i + 2] == 'u' && data[i + 3] == 'r' && data[i + 4] == 's' && data[i + 5] == 'e' && data[i + 6] == 'c' && data[i + 7] == 'o' && data[i + 8] == 'd' && data[i + 9] == 'e')
                    {
                        i += "coursecode\":".Length; // skip forward to the coursecode data
                        int j = i;
                        while (data[j] != ',') // find end of coursecode data
                            j++;
                        newCoursecode = (data.Substring(i +1 , j - (i + 2))); // parse coursecode
                        
                        i = j; // jump to momentcode
                    }

                      else if (data[i] == 'm' && data[i + 1] == 'o' && data[i + 2] == 'm' && data[i + 3] == 'e' && data[i + 4] == 'n' && data[i + 5] == 't' && data[i + 6] == 'c' && data[i + 7] == 'o' && data[i + 8] == 'd' && data[i + 9] == 'e')
                    {
                        i += "momentcode\":".Length; // skip forward to the momentcode data
                        int j = i;
                        while (data[j] != ',') // find end of momentcode data
                            j++;
                        newMomentcode = (data.Substring(i + 1, j - (i + 2 ))); // parse momentcode
                        i = j; // jump to question
                    }
                    // if data[i...] == "questionID"

                    else if (data[i] == 'q' && data[i + 1] == 'u' && data[i + 2] == 'e' && data[i + 3] == 's' && data[i + 4] == 't' && data[i + 5] == 'i' && data[i + 6] == 'o' && data[i + 7] == 'n' && data[i + 8] == 'i' && data[i + 9] == 'd')
                    {
                        i += "questionid\":".Length; // skip forward to the momentcode data
                        int j = i;
                        while (data[j] != ',') // find end of momentcode data
                            j++;
                        newQuestionID = (data.Substring(i + 1, j - (i + 2))); // parse momentcode
                        Debug.Log(" newQuestionID i Parser = " + newQuestionID);
                        i = j; // jump to question
                    }
                    // if data[i...] == "question"

                    else if (data[i] == 'q' && data[i + 1] == 'u' && data[i + 2] == 'e' && data[i + 3] == 's' && data[i + 4] == 't' && data[i + 5] == 'i' && data[i + 6] == 'o' && data[i + 7] == 'n')
                    {
                        i += "question\":".Length; // skip forward to the question data
                        int j = i;
                        while (data[j] != ',') // find end of question data
                            j++;
                        newQuestion = (data.Substring(i + 1, j - (i + 2) )); // parse question
                    }

                     else if (data[i] == 'a' && data[i + 1] == 'n' && data[i + 2] == 's' && data[i + 3] == 'w' && data[i + 4] == 'e' && data[i + 5] == 'r')
                    {
                        i += "answer\":".Length; // skip forward to the answer data
                        int j = i;
                        while (data[j] != '}') // find end of answere data
                            j++;
                        newAnswer = (data.Substring(i + 1, j - (i + 2))); // parse answer
                    }
                    if (data[i] == 't' && data[i + 1] == 'r' && data[i + 2] == 'u' && data[i + 3] == 'e')
                    {
                        /* i += "coursecode\":".Length; // skip forward to the coursecode data
                            int j = i;
                            while (data[j] != ',') // find end of coursecode data
                                j++;
                            newCoursecode = (data.Substring(i + 1, j - (i + 2))); // parse coursecode

                            i = j; // jump to momentcode*/
                        Debug.Log(" true i parser ");    
                        authorization = true;
                        HasCheckedLoggin = true;
                        //return true;
                    }

                    else if (data[i] == 'f' && data[i + 1] == 'a' && data[i + 2] == 'l' && data[i + 3] == 's' && data[i + 4] == 'e')
                    {
                        /*i += "momentcode\":".Length; // skip forward to the momentcode data
                        int j = i;
                        while (data[j] != ',') // find end of momentcode data
                            j++;
                        newMomentcode = (data.Substring(i + 1, j - (i + 2))); // parse momentcode
                        i = j; // jump to question*/
                        authorization = false;
                        HasCheckedLoggin = true;
                        //return false;
                    }


                    HasResult = true; // we now have a result

                    save = true;
                    break;
              }

         }

    }

    void createNewCourse()
    {
        c = new CurrentCourse(coursecode);
        //Debug.Log(c + " = c i createNewCourse i Parser.cs");
        c.setCoursecode(coursecode);
        c.setLevel(int.Parse(momentcode));

        if (coursecode != null && courseList != null)
        {
            bool newCourse = true;
            foreach (Course co in courseList)
            {
                //Debug.Log(co + " = co in createNewCourse i Parser:s foreachloop");


                if (co == null)
                {
                    //courseList.Remove(co);
                    //Debug.Log(" co = null Vi hoppar över den!!");
                }
                else
                {
                    //Debug.Log(co.getCoursecode() + " = co:s kurskod in createNewCourse i Parser:s foreachloop");

                    if (co.getCoursecode().Equals(coursecode))
                    {
                        newCourse = false;
                        c = co;
                    }
                }
            }

            if (newCourse)
            {
                //HasNewCourse = true;
                courseList.Add(c);
                //Debug.Log(c + " was added in courselist");
            }
            createNewMoment(c);
        }
    }

   /* void createNewMoment(Course c)
    {
        int level = int.Parse(momentcode); 
        //Debug.Log(level + question + answer + " was received i createNewMoment i Parser.cs");
        c.AddQuestion(level, question, answer);
    }*/

    void createNewMoment(Course c)
    {
        if (!c.momentcodes.ContainsKey(int.Parse(momentcode)))
        {
            int x = 0;
            if (c.levels == null)
            {
                c.levels = c.questions.Keys.ToList();
                c.levels.Add(x);
                //c.levelDictionary.Add(x, momentcode);
            }

            else foreach (int y in c.levels) x++;
            c.momentcodes.Add(int.Parse(momentcode), x);

        }
        int level = c.momentcodes[int.Parse(momentcode)]; //int.Parse(newMomentcode);

        Debug.Log(level +", " + questionID + ", " + question + ", " + answer + " was received i createNewMoment i Recive.cs");
        c.AddQuestion(level, questionID, question, answer);
    }


    public bool Authorization(string data)
    {
       /* State st = State.open; // start outside any data structure
        bool save = false;*/

        for (int i = 0; i < data.Length; i++) // iterate over the characters
        {
           /* switch (st)
            {
                case State.open:
                    if (data[i] == '[') // look for opening JSON array
                        st = State.array; // switch to array state

                    break;
                case State.array:
                    if (data[i] == '{') // look for opening JSON object
                        st = State.obj; // switch to object state
                    save = false; // new object; reset save state

                    break;
                case State.obj:
                    if (data[i] == '}') // looking for end of JSON object
                    {
                       /* if (save) // the object read should replace the return values
                        {
                            if (coursecode != newCoursecode && newCoursecode != null)
                            {
                                coursecode = newCoursecode;
                                HasNewResult = true;
                            }

                            if (momentcode != newMomentcode && newMomentcode != null)
                            {
                                momentcode = newMomentcode;
                                HasNewResult = true;
                            }

                            if (question != newQuestion && newQuestion != null)
                            {
                                question = newQuestion;
                                HasNewResult = true;
                            }

                            if (answer != newAnswer && newAnswer != null)
                            {

                                if (newAnswer == "ul")
                                {
                                    defaultAnswer++;
                                    answer = "" + defaultAnswer;
                                }
                                else answer = newAnswer;
                                HasNewResult = true;
                            }
                            if (HasNewResult)
                            {
                                Debug.Log("Tillfälligt i Parser: corsecode sparas som: " + coursecode + ", momentcode sparas som: " + momentcode + ", question sparas som: " + question + ", answer sparas som: " + answer);
                                createNewCourse();
                                HasNewResult = false;
                                save = false;
                            }

                        }*/

                   // }

                if (data[i] == 't' && data[i + 1] == 'r' && data[i + 2] == 'u' && data[i + 3] == 'e')
                {
                /* i += "coursecode\":".Length; // skip forward to the coursecode data
                    int j = i;
                    while (data[j] != ',') // find end of coursecode data
                        j++;
                    newCoursecode = (data.Substring(i + 1, j - (i + 2))); // parse coursecode

                    i = j; // jump to momentcode*/
                    authorization = true;
                    HasCheckedLoggin = true;
                    return true;
                }

                else if (data[i] == 'f' && data[i + 1] == 'a' && data[i + 2] == 'l' && data[i + 3] == 's' && data[i + 4] == 'e')
                {
                /*i += "momentcode\":".Length; // skip forward to the momentcode data
                int j = i;
                while (data[j] != ',') // find end of momentcode data
                    j++;
                newMomentcode = (data.Substring(i + 1, j - (i + 2))); // parse momentcode
                i = j; // jump to question*/
                    authorization = false;
                    HasCheckedLoggin = true;
                    return false;
                }
                

                /*HasResult = true; // we now have a result

                save = true;
                break;*/
       }
        //return false;
      return authorization;
        //return true;
    }

 
}

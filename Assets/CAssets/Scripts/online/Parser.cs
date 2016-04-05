using UnityEngine;
using System.Collections;

public class Parser {

    public string coursecode { get; private set; } 
    public string momentcode { get; private set; } 
    public string question { get; private set; }
    public string answer { get; private set; }

    private bool HasResult; // indicated if the object is carrying an result
    public bool HasNewResult { get; private set; } // indicated if the object is carrying a new result

    private enum State { open, array, obj }; // keeps track of the mode the parser has reached 

    public Parser(string data)
    {
        coursecode = string.Empty; ;
        momentcode = string.Empty;
        question = string.Empty;
        answer = string.Empty;

        HasResult = false;
        if (data != null) parseJson(data);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void parseJson(string data)
    {
        State st = State.open; // start outside any data structure
        string newCoursecode = string.Empty; // most recently read timestamp that may be saved
        string newMomentcode = string.Empty; // most recently read value that may be saved
        string newQuestion = string.Empty; 
        string newAnswer = string.Empty;

        bool save = false; // indicates if the candidate value should replace the result value

         for (int i = 0; i < data.Length; i++) // iterate over the characters
         {
             switch (st)
             {
                 case State.open:
                     if (data[i] == '{') // look for opening JSON array
                         st = State.array; // switch to array state
                    
                    break;
                 case State.array:
                     if (data[i] == '"') // look for opening JSON object
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
                                answer = newAnswer;
                                HasNewResult = true;
                            }
                            Debug.Log("Tillfälligt i Parser: corsecode sparas som: " + coursecode + ", momentcode sparas som: " + momentcode + ", question sparas som: " + question + ", answer sparas som: " + answer);
                         }

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

                    HasResult = true; // we now have a result

                    save = true;
                    break;
              }

         }

    }
}

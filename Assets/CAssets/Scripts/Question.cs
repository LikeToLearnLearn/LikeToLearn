using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Question {
	private static System.Random rnd = new System.Random();
    //public Dictionary<string, string> a = new Dictionary<string, string>();
    public Dictionary<string, List<string>> a = new Dictionary<string, List<string>>();
    private List<string> answers = new List<string>();
    private List<string> alt = new List<string>();
	private Course course;
    public string coursecode;
    public string momentcode;
	public string question;
	public string answer;
	private int index;
    private Connection connection;

	public Question(Course course, int level, string question, string answer)
	{
		this.course = course;
        coursecode = course.getCoursecode();
        momentcode = "" + level;
		this.question = question;
		this.answer = answer;
		this.index = rnd.Next(1000);

        //GameObject conn = GameObject.Find("ConnectionHandler");
        //connection = new Connection();
            //conn.GetComponent<Connection>();

        AddAlternative(answer);
	}
		
	public void Answer(string givenAnswer)
	{
        //connection = new Connection();

        answers.Add(givenAnswer);
        if (IsCorrect() && answers.Count == 1)
        {
            course.LogAnswerCorrect(question);
            //a.Add(givenAnswer, "rigtht");// Fix: behöver kunna svara fel många gånger på samma fråga.
            if (!a.ContainsKey(givenAnswer))
            {
                a.Add(givenAnswer, new List<string>());
               
             }

                a[givenAnswer].Add("true");
                Debug.Log("La till rätt svar i listan i Question.");
           /* else
            {
                a[givenAnswer].Add("right");
                Debug.Log("La till rätt svar i listan i Question på redan befintlig nyckel.");
            }*/
            
            //connection.sendResult(coursecode, momentcode, question, "right");
        }

        else
        {
            if (!a.ContainsKey(givenAnswer))
            {
                a.Add(givenAnswer, new List<string>());
            }

            a[givenAnswer].Add("false");
            Debug.Log("La till fel svar i listan i Question.");

           /* else
            {
                a[givenAnswer].Add("wrong");
                Debug.Log("La till fel svar i listan i Question på redan befintlig nyckel.");
            }*/
            //a.Add(givenAnswer, "wrong");

            answers.Add(givenAnswer);
        }
            //connection.sendResult(coursecode, momentcode, question, "wrong");
    }

	public bool IsCorrect()
	{
		int n = answers.Count;
        return n > 0 && answers[n - 1] == answer;
	}

	public string GetAlternative()
	{
        //Debug.Log(" alt.Count i Questions är nu: " + alt.Count);
		index %= alt.Count - 1;
		return alt[index++];
	}

	public void AddAlternative(string alternative)
	{
		alt.Add(alternative);
	}


	public string GetQuestion()
	{
		return question;
	}
}

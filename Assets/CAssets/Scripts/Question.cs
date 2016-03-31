using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Question {
	private static System.Random rnd = new System.Random();
	private List<string> answers = new List<string>(); 
	private List<string> alt = new List<string>();
	private Course course;
    private string coursecode;
    private string momentcode;
	private string question;
	private string answer;
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

        
		AddAlternative(answer);
	}
		
	public void Answer(string givenAnswer)
	{
        connection = new Connection();
        connection.sendResult(coursecode, momentcode, question, givenAnswer);
        answers.Add(givenAnswer);
		if (IsCorrect() && answers.Count == 1) {
			course.LogAnswerCorrect(question);
		}
	}

	public bool IsCorrect()
	{
		int n = answers.Count;
        //GameObject.DestroyObject(connection, 0);
        return n > 0 && answers[n - 1] == answer;
	}

	public string GetAlternative()
	{
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

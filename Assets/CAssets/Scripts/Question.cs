using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Question {
	private static Random rnd = new Random();
	private List<string> answers; 
	private List<string> alt;
	private Course c;
	private string q;
	private string a;
	private int i;

	public string question {
		get { return q; }
	}

	public Question(Course c, string q, string a)
	{
		this.c = c;
		this.q = q;
		this.a = a;
		i = rnd.Next(1000);
		AddAlternative(a);
	}

	public void PutAnswer(string a)
	{
		answers.Add(a);
		if (answers.Count == 1 && RightAnswer())
			c.LogAnswerCorrect(a);
	}

	public bool RightAnswer()
	{ 
		return answers[answers.Count - 1] == a;
	}

	public void AddAlternative(string alternative)
	{
		alt.Add(alternative);
	}

	public string GetAlternative()
	{
		i = (i + 1) % alt.Count;
		return alt[i];
	}
}

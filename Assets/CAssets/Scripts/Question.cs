using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Question {
	static Random rnd = new Random();
	List<string> answers; 
	List<string> alt;
	Course c;
	string q;
	string a;
	int i;

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

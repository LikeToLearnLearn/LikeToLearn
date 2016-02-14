using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Question {
	private static Random rnd = new Random();
	private List<string> answers = new List<string>(); 
	private List<string> alt = new List<string>();
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
		this.i = rnd.Next(1000);
		AddAlternative(a);
	}

	public void Answer(string a)
	{
		answers.Add(a);
		if (answers.Count == 1 && Correct())
			c.LogAnswerCorrect(a);
	}

	public bool Correct()
	{ 
		return answers[answers.Count - 1] == a;
	}

	public string Alternative()
	{
		i = (i + 1) % alt.Count;
		return alt[i];
	}

	public void AddAlternative(string alternative)
	{
		alt.Add(alternative);
	}

}

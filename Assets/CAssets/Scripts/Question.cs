using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Question {
	private static Random rnd = new Random();
	private List<string> answers = new List<string>(); 
	private List<string> alt = new List<string>();
	private Course course;
	private string question2; // TODO: change name when old accessor is removed
	private string answer;
	private int index;

	public Question(Course course, string question, string answer)
	{
		this.course = course;
		this.question2 = question;
		this.answer = answer;
		this.index = rnd.Next(1000);
		AddAlternative(answer);
	}
		
	public void Answer(string givenAnswer)
	{
		answers.Add(givenAnswer);
		if (answers.Count == 1 && Correct()) {
			course.LogAnswerCorrect(givenAnswer);
		}
	}

	public bool IsCorrect()
	{
		int n = answers.Count;
		return n > 0 && answers[n - 1] == answer;
	}

	public string GetAlternative()
	{
		index %= alt.Count;
		return alt[index++];
	}

	public void AddAlternative(string alternative)
	{
		alt.Add(alternative);
	}


	public string GetQuestion()
	{
		return question2;
	}

	// TODO: remove this methods when the minigames are updated...

	public string Alternative()
	{
		Console.WriteLine("Depricated method name Alternative called, change to GetAlternative");
		return GetAlternative();
	}

	public bool Correct()
	{
		Console.WriteLine("Depricated method name Correct caquestion2n2ge to IsCorrect");
		return IsCorrect();
	}

	public string question {
		get {
			Console.WriteLine("Depricated accessor question called, change to GetQuestion");
			return GetQuestion();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public abstract class Course {

	bool testMode = false;
	int testLevel = 0;
	Dictionary<int, List<string>> questions = new Dictionary<int, List<string>>();
	Dictionary<string, string> answers = new Dictionary<string, string>();
	Dictionary<string, int> results = new Dictionary<string, int>();
	static Random rnd = new Random();

	public virtual Question GetQuestion(int alternatives)
	{
		int level = testMode ? testLevel : CurrentLevel();
		List<string> qs = questions[level];
		string q = qs[rnd.Next(qs.Count)];
		string a = answers[q];
		var added = new List<string>();
		var res = new Question(this, q, a);
		List<string> ans = Enumerable.ToList(answers.Values);
		while (alternatives > 0) {
			string cand = ans[rnd.Next(ans.Count)];
			if (a != cand && added.IndexOf(cand) == -1) {
				added.Add(cand);
				res.AddAlternative(cand);
				alternatives--;
			}
		}
		return res;
	}

	private int CurrentLevel()
	{
		var levels = new List<int>();
		foreach (var key in questions.Keys)
			levels.Add(key);
		levels.Sort();
		foreach (int level in levels) {
			foreach (string question in questions[level]) {
				if (results[question] < 2) // number of correct answers needed
					return level;
			}
		}
		return levels[levels.Count - 1];
	}

	public virtual void SetTestMode(int level)
	{
		//if (!questions.ContainsKey(level))
		//	throw new KeyNotFoundException();
		testMode = true;
		testLevel = level;
	}

	public virtual void UnSetTestMode()
	{
		testMode = false;
	}

	public virtual void AddQuestion(int level, string question, string answer)
	{
		if (!questions.ContainsKey(level))
			questions[level] = new List<string>();
		questions[level].Add(question);
		answers[question] = answer;
		results[question] = 0;
	}

	public virtual void LogAnswerCorrect(string question)
	{
		if (!results.ContainsKey(question))
			results[question] = 0;
		results[question]++;
		GameController.control.AddExp(1); // fixme
	}

}

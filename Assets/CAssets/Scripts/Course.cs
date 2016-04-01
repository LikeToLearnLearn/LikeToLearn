using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Course {

	bool testMode = false;
	int testLevel = 0;
    int nr = 0;
    public Dictionary<int, List<string>> questions = new Dictionary<int, List<string>>();
Dictionary<string, string> answers = new Dictionary<string, string>();
	Dictionary<string, int> results = new Dictionary<string, int>();
	static System.Random rnd = new System.Random();

    private string coursecode = "defaulCourse";

    
   public string getCoursecode()
    {
        return coursecode;
    }

    public void setCoursecode( string coursecode)
    {
        this.coursecode = coursecode;
    }

    public virtual Question GetQuestion(int alternatives)
	{
        Debug.Log( "in getquestion 1");

        int level = 1;// testMode ? testLevel : CurrentLevel();
		List<string> qs = questions[level];
        Debug.Log(qs + "in getquestion 2");
		string q = qs[rnd.Next(qs.Count)];
        Debug.Log(q + "in getquestion 3");
        string a = answers[q /*+ "" + 2*/];
        Debug.Log(a + "in getquestion 4");
        var added = new List<string>();
        Debug.Log(added + "in getquestion 5");
        var res = new Question(this, level,  q, a);
        Debug.Log(res + "in getquestion 6");
        List<string> ans = Enumerable.ToList(answers.Values);
        Debug.Log(ans + "in getquestion 7");
        while (alternatives > 0) {
            Debug.Log( "in getquestion 8");
            string cand = ans[rnd.Next(ans.Count)];
            Debug.Log(qs + "in getquestion 9");
            if (a != cand && added.IndexOf(cand) == -1) {
                Debug.Log(qs + "in getquestion 10");
                added.Add(cand);
                Debug.Log(qs + "in getquestion 11");
                res.AddAlternative(cand);
				alternatives--;
			}
		}
		return res;
	}

	public virtual int CurrentLevel()
	{
		var levels = questions.Keys.ToList();
		levels.Sort();
		int result = 0;
		foreach (int level in levels) {
			if (4 < questions[level].Count(x => results[x] > 0)) {
				result = level + 1;
			} else {
				break;
			}
		}
		return result;
	}

	public virtual void SetTestMode(int level)
	{
		testMode = true;
		testLevel = level;
	}

	public virtual void UnSetTestMode()
	{
		testMode = false;
	}

	public virtual void AddQuestion(int level, string question, string answer)
	{
        Debug.Log("questions.ContainsKey(level) = " + questions.ContainsKey(level));
        nr++;
		if (!questions.ContainsKey(level)) {
            //questions[level] = new List<string>();
            Debug.Log(" Innan jag lagt till en fråga i questions: " + questions.ToString());
            questions.Add(level, new List<string>());
            questions[level].Add(question);
            Debug.Log(" Efter att jag lagt till en fråga i questions[level]: " + questions[level]);
        }

		/*if (!questions[level].Exists(q => q == question)) {
			questions[level].Add(question);
		}*/

        else
        {
            questions[level].Add(question);
            
        }

		if (!answers.ContainsKey(question/* + "" + nr*/)) {
            //answers[question] = answer;
            answers.Add(question/* + "" + nr*/, answer /*+ "" + nr*/);
            Debug.Log("answer " + answers.ContainsKey(answer /*+ "" + nr*/));
		}

		if (!results.ContainsKey(question)) {
			results.Add(question, 0);
		}
	}

	public virtual void LogAnswerCorrect(string question)
	{
		++results[question];
	}

}

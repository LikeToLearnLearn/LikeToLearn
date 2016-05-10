using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Course {

	bool testMode = false;
	int testLevel = 1;
    public Dictionary<int, List<string>> questions = new Dictionary<int, List<string>>();
    Dictionary<string, string> answers = new Dictionary<string, string>();
    Dictionary<string, string> questionIDs = new Dictionary<string, string>();
    Dictionary<string, int> results = new Dictionary<string, int>();
	static System.Random rnd = new System.Random();
    int level = 1;
    public Dictionary<int, int> momentcodes = new Dictionary<int, int>();
    public Dictionary<int, string> levelDictionary = new Dictionary<int,string >();
    public List<int> levels;
    public float takenTime = 0;

    private string coursecode = "defaultCourse";
    public Dictionary<string, float> doneMoments = new Dictionary<string, float>();
  
    void start()
    {
        levels = questions.Keys.ToList();
        levels.Add(0);
    }

    void update()
    {
        levels = questions.Keys.ToList();
        if(GameController.control.recive.online && doneMoments.Count > 0 && GameController.control.name != "testmode")
        {
            List<string> keys = new List<string>(doneMoments.Keys);
            foreach (string key in keys)
            {
                GameController.control.recive.DoneMoment(GameController.control.name, key, doneMoments[key]);
                Debug.Log(" Från sparfilen skickades att " + GameController.control.name + " har klartat momentet: " + key + ", på tiden: " + doneMoments[key]);
            }

        }
    }

  /*  public void AddTakenTime(float t)
    {
        takenTime = takenTime + t;
    }*/

    public void ResetTakenTime()
    {
        takenTime = 0;
        Debug.Log("Nu börjar en ny tidtaging för ett nytt moment. takenTime = " + takenTime);
    }

    public string getCoursecode()
    {
        return coursecode;
    }

    public void setLevel(int x)
    {
        level = x;
    }

    public int getLevel( int x)
    {
        return level;
    }

    public void setCoursecode( string coursecode)
    {
        this.coursecode = coursecode;
    }

    public virtual Question GetQuestion(int alternatives)
	{

        int level = testMode ? testLevel : CurrentLevel();
          //Debug.Log("I Courses GetQuestions level: " + level);
        
          List<string> qs = null;
          if (questions[level] != null) { qs = questions[level]; }
          //Debug.Log("I GetQuestions questions[level]: " + questions[level]);
        
          string q = null;
          if (questions[level] != null) q = qs[rnd.Next(qs.Count)];
          //Debug.Log("I GetQuestions q: " + q);
        
          string a = null;
          if(q != null) a =answers[q];
          //Debug.Log("I GetQuestions a: " + a);
        
          var added = new List<string>();
          //Debug.Log("I GetQuestions added: " + added);

        
         var res = new Question(this, level, questionIDs[q], q, a);
         //Debug.Log("I GetQuestions res: " + res);

        
         List<string> ans = Enumerable.ToList(answers.Values);  
          //Debug.Log("I GetQuestions ans: " + ans);
          //while (true /*alternatives > 0*/)
          for(int i= alternatives; i>0; i--)
        {
              string cand = ans[rnd.Next(ans.Count)];
              //Debug.Log("I GetQuestions cand: " + cand);
              if (a != cand && added.IndexOf(cand) == -1)
            {
                  added.Add(cand);
                  res.AddAlternative(cand);
                  alternatives--;
            }
         }
          return res;
       
	}

	public virtual int CurrentLevel() //Fixa så det inte går att levla upp över den högsta level som finns!!!!
	{
		levels = questions.Keys.ToList();
		levels.Sort();
		int result = 0;
        foreach (int level in levels)
        {
            Debug.Log("Den här kursen har för närvarade " + levels.Count + " moment. Det finns en level som heter " + level);
            var xs = questions[level];
            var y = level + 1;
            if (xs.Count <= xs.Count(x => results[x] > 1) && y > result) // byt 1:an till en 3:a.
            {
                result = y;
                Debug.Log( "I currenLevel registeras att: " + GameController.control.name + " har klarat moment " + level + " på tiden " + takenTime);
                if (GameController.control.recive.online && GameController.control.name != "testmode")
                {
                    GameController.control.recive.DoneMoment(GameController.control.name, levelDictionary[level], takenTime);   
                }
                else doneMoments.Add(levelDictionary[level], takenTime);
                    
                ResetTakenTime();
                
            }
            if (result == levels.Count || result > levels.Count)
            {
                if (levels.Count == 1) result = 0;
                else result = rnd.Next(levels.Count);
            }
        }
        Debug.Log("Den level som returneras är " + result);
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

    public virtual void AddQuestion(int level, string questionID, string question, string answer)
	{
        //Debug.Log("I AddQuestions; level = " + level + "question = " + question + "answer = " + answer);
        if (!questions.ContainsKey(level))
        {
            //Debug.Log("I AddQuestions if: " + level + ", " + question + ", " + answer);
            //questions[level] = new List<string>();
            questions.Add(level, new List<string>());
            questions[level].Add(question);
        }

		/*if (!questions[level].Exists(q => q == question)) {
			questions[level].Add(question);
		}*/

        else
        {
            // 
            if (!questions[level].Contains(question))
            questions[level].Add(question);
            //Debug.Log("I AddQuestions questions[level]: "  + questions[level]);

        }

		if (!answers.ContainsKey(question)) {
            //answers[question] = answer;
            answers.Add(question, answer);
            //Debug.Log("I AddQuestions answers[question]: " + answers[question]);
        }
        if (!questionIDs.ContainsKey(question))
        {
            //answers[question] = answer;
            questionIDs.Add(question, questionID);
            //Debug.Log("I AddQuestions answers[question]: " + answers[question]);
        }

        if (!results.ContainsKey(question)) {
			results.Add(question, 0);
            //Debug.Log("I AddQuestions result[question]: " + results[question]);
        }
	}

	public virtual void LogAnswerCorrect(string question)
	{
		++results[question];
	}

}

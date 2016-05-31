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
    public Dictionary<string, string> answers = new Dictionary<string, string>();
    public Dictionary<string, string> questionIDs = new Dictionary<string, string>();
    public Dictionary<string, int> results = new Dictionary<string, int>();
	static System.Random rnd = new System.Random();
    int level = 0;
    public Dictionary<int, int> momentcodes = new Dictionary<int, int>();
    public Dictionary<int, string> levelDictionary = new Dictionary<int,string >();
    public List<int> levels;
    public float takenTime = 0;

    private string coursecode = "defaultCourse";
    public Dictionary<string, float> doneMoments = new Dictionary<string, float>();

    int tillfällig = 0;
      
    void start()
    {
        //levels = questions.Keys.ToList();
        //levels.Add(0);
        level = GameController.control.GetCurrentLevel(coursecode);
        takenTime = GameController.control.GetTakenTime(coursecode);
    }

    void update()
    {
        level = GameController.control.GetCurrentLevel(coursecode);
        levels = questions.Keys.ToList();
        if(GameController.control.recive.online/* && doneMoments.Count > 0*/ && GameController.control.name != "testmode")
        {
            List<string> keys = new List<string>(doneMoments.Keys);
            foreach (string key in keys)
            {
                if (doneMoments[key] != 0)
                {
                    GameController.control.recive.DoneMoment(GameController.control.name, key, doneMoments[key]);
                    Debug.Log(" Från sparfilen skickades att " + GameController.control.name + " har klartat momentet: " + key + ", på tiden: " + doneMoments[key]);
                }

                doneMoments[key] = 0;
            }
        }
    }


    public void ResetTakenTime()
    {
        takenTime = 0;
        GameController.control.ResetTakenTime(coursecode);
        Debug.Log("Nu börjar en ny tidtaging för ett nytt moment");
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

        /*List<int> keys = new List<int>(questions.Keys);
        foreach (int key in keys) Debug.Log(" Finns i questions: " + key);
        Debug.Log("Finns questions[" +level + "] i GetQuestion?  ... "+ questions.ContainsKey(level));*/ // + questions[level]);
          if (questions[level] != null) { qs = questions[level]; }
       
        
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
        tillfällig++;
        levels = questions.Keys.ToList();
		levels.Sort();
		int result = GameController.control.GetCurrentLevel(coursecode);
        foreach (int level in levels)
        {
            //Debug.Log("Kursen med kurskoden " + coursecode + " har för närvarade " + levels.Count + " levels. Det finns en level som heter " + level);
            var xs = questions[level];
            var y = GameController.control.GetCurrentLevel(coursecode) + 1;
            //if (xs.Count <= xs.Count(x => results[x] > 1) && y > result) // byt 1:an till en 3:a????
            if(tillfällig%5 == 0)
            {
                tillfällig = 1;
                result = y;
                if (GameController.control.name != "testmode" && !doneMoments.ContainsKey(levelDictionary[GameController.control.GetCurrentLevel(coursecode)])) doneMoments.Add(levelDictionary[GameController.control.GetCurrentLevel(coursecode)], takenTime);
                

                if (GameController.control.recive.online && GameController.control.name != "testmode" && doneMoments[levelDictionary[GameController.control.GetCurrentLevel(coursecode)]] != 0)
                {
                   Debug.Log( "I currenLevel registeras att: " + GameController.control.name + " har klarat level " + GameController.control.GetCurrentLevel(coursecode) + " på tiden " + takenTime);
                   GameController.control.recive.DoneMoment(GameController.control.name, levelDictionary[GameController.control.GetCurrentLevel(coursecode)], takenTime);
                    //if(!doneMoments.ContainsKey(levelDictionary[level]))
                    doneMoments[levelDictionary[GameController.control.GetCurrentLevel(coursecode)]] = 0;
                }

                ResetTakenTime();
                
            }
            if (result == levels.Count || result > levels.Count)
            {
                if (levels.Count == 1) result = 0;
                else result = rnd.Next(levels.Count-1);
            }
        }
        //List<string> keys = new List<string>(doneMoments.Keys);
        //foreach (string key in keys) Debug.Log(" Finns i questions: " + key + " " + doneMoments[key]);
    
        GameController.control.SetCurrentLevel(coursecode, result);
        //Debug.Log("Den level som returneras är " + result + ", i gamecontroller registeras att level är: " + GameController.control.GetCurrentLevel(coursecode));
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

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public enum Language {	English, Swedish };
	public enum Item {
		Fish, Brick, OneCoin, TenCoin, HundredBill, ThousandBill,
		RedBalloon, YellowBalloon, BlueBalloon, GreenBalloon,
		FiveCoin, TwentyBill, BlueBrick, GreenBrick, YellowBrick,
		GlassBlock
	};

    public string name;
    private string password;
    public Recive recive;
    public bool testmode;

	// global configuration data that all instances use
	[Serializable]
	class GlobalData {
		public string currentGame = null;
		public Language lang = Language.English;
		public int gameCount = 0; // serial number
		public Dictionary<string, int> games = new Dictionary<string, int>();
	}
	// data associated with a given game instance
	[Serializable]
	public class GameData {
		public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
		public string currentScene = "city_centralisland";
		public List<Course> coruses = new List<Course>();
		public List<Question> questions = new List<Question>();
		public Course currentCourse = null;
        public Dictionary<string, int> CurrentLevel = new Dictionary<string, int>();
        public int experiencePoints = 0;
        public string password = "buu";
        public Dictionary<string,float> takenTime= new Dictionary<string, float>();
        public Dictionary<string, int> CurrentCourseVersion = new Dictionary<string, int>();
	}

	public static GameController control;

	GameData data = null;
	GlobalData global = null;
	SceneHandler sceneHandler;

    public string getCurrentCourseCode()
    {
        if (data != null) return data.currentCourse.getCoursecode();
        else return "";
    }

    public int getCurrentCourseVersion(string coursecode)
    {
        if (data != null && data.CurrentCourseVersion.ContainsKey(coursecode))
            return data.CurrentCourseVersion[coursecode];
        return 0;
    }

    public void setCurrentCourseVersion(string coursecode, int version)
    {
        if (data.CurrentCourseVersion.ContainsKey(coursecode))
            data.CurrentCourseVersion[coursecode] = version;
        else data.CurrentCourseVersion.Add(coursecode, version);

        //Debug.Log("I setCurrentCourseVersion tas " + version + " emot. data.CurrentCourseVersion[" + coursecode + "] blir: " + data.CurrentCourseVersion[coursecode]);
    }

    public void addTakenMomentTime(float time)
    {
        if(data != null)
        {
            if (data.takenTime.ContainsKey(data.currentCourse.getCoursecode())) data.takenTime[data.currentCourse.getCoursecode()] = data.takenTime[data.currentCourse.getCoursecode()] + time;
            else data.takenTime.Add(data.currentCourse.getCoursecode(), time);
            data.currentCourse.takenTime = data.currentCourse.takenTime + time; 
        }
    }

    public void ResetTakenTime(string courseCode)
    {
        if (data != null)
        {
            if (data.takenTime.ContainsKey(courseCode)) data.takenTime[courseCode] = 0;
        }
    }


    public float GetTakenTime(string courseCode)
    {
        if (data != null)
        {
            if (data.takenTime.ContainsKey(courseCode)) return data.takenTime[courseCode];
            else return 0;
        }
        return 0;
    }

    public void SetCurrentLevel(string courseCode, int level)
    {
        if (data != null)
        {
            if (!data.CurrentLevel.ContainsKey(courseCode)) data.CurrentLevel.Add(courseCode, level);
            else data.CurrentLevel[courseCode] = level;

            Debug.Log("I GameControllers SetCurrentLevel sätts data.CurrentLevel[" + courseCode + "] till: " + data.CurrentLevel[courseCode]);
        }
    }

    public int GetCurrentLevel(string courseCode)
    {
        if (data.CurrentLevel.ContainsKey(courseCode)) return data.CurrentLevel[courseCode];
        else return 0;
    }

   /* public float getTakenMomentTime()
    {
        if(data!= null)
        {
            return data.takenTime;
        }
        else return 0;
    }

    public void resetTakenMomentTime()
    {
        data.takenTime = 0;
    }*/


    public int unlockedWorldLevel {  // not tested
		get { return (int) Math.Log10(data.experiencePoints); }
	}

	public Dictionary<Item, int> inventory {
		get { return data.inventory; }
	}

	public Dictionary<string, int> stringInventory {
		get {
			var inv = new Dictionary<string, int>();
			foreach (var entry in data.inventory)
				inv[entry.Key.ToString()] = entry.Value;
			return inv;
		}
	}

	public bool gotSavedGames {
		get { return global.games != null && global.games.Count > 0; }
	}

	void Awake()
	{
		if (control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;
		} else if (control != this) {
			Destroy(gameObject);
		}
	}

	void Start()
	{
		GameObject sco = GameObject.Find("SceneHandlerO");
		sceneHandler = sco.GetComponent<SceneHandler>();

        GameObject conn = GameObject.Find("ConnectionHandler");
        recive = conn.GetComponent<Recive>();

        testmode = false;

        // auto every fifth secound
        InvokeRepeating("Save", 5, 5);
        InvokeRepeating("SendResults", 5, 5);
    }

    void Update()
    {
       if (recive.c != null) setCurrentcourse(recive.c);
        //if (data != null && data.password == "") Debug.Log("OBS! Nu är lösenordet = null!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");   
    }

    void setName(string name)
    {
        name = name.Trim();
        if (NameTaken(name) || NameInvalid(name)) return;

        else this.name = name;
    }

   /* public Recive GetRecive()
    {
        return recive;
    }*/

   

    void SendResults()
    {
        if (recive != null && data!= null && !testmode)
        {
            if ( recive.Online()) 
            {
                Question q = null;
                List<Question> temp = data.questions;
                for (int i = 0; i < temp.Count; ++i)
                {
                    q = temp[i];
                    Dictionary<string, List<string>> tmp = q.a;
                    if ( tmp!= null && tmp.Count > 0)
                    {
                        List<string> keys = new List<string>(tmp.Keys);
                        foreach (string key in keys)
                        {
                            List<string> ans = tmp[key];
                            foreach (string answ in ans)
                            {
                                recive.sendStatistics(q.getQuestionID(), key, answ, name);
                                Debug.Log("Skickar: frågeid: " + q.getQuestionID()+ ", svar: " + " " + key + ", rätt eller fel: " + answ + ", användarid: " + name);
                                //data.questions[i].a.Remove(key);
                            }
                            data.questions[i].a.Remove(key);
                        }
                    }     
                }
            }
        }
    }

    public void AskForNewQuestions()
    {
        if (recive != null && ! testmode)
        {            //Debug.Log(" Nu är vi i AskForNewQuestions");
            if (recive.Online()) 
            {
                //recive.getNewQuestions(name);
                recive.checkCourseAndVersion(name);
            }
        }
    }

    void OnEnable()
	{
		LoadGlobal();
	}

	void OnDisable()
	{
		Save();
	}

	private void Save()
	{
		if (global != null) {
			SaveGlobal();
			if (data != null) {
				SaveGame();
			}
		}
	}

    public void setPassword(string password)
    {
        data.password = password;
    }

	public void NewGame(string name, string password)
	{
		name = name.Trim();
		if ((NameTaken(name) || NameInvalid(name)) && recive.Online()) return;
        //recive.authentication();
        //recive.CheckLogin(name, password);
        //recive.Loogin();
        if (recive.Login(name, password) || !recive.Online())
        {
            this.name = name;
            this.password = password;
            global.currentGame = name;

            if (name == "testmode") // fix me for testmode
            {
                testmode = true;
                //var filePath = SaveFileName(name);
                //if(File.Exists(filePath)) 
                    
                //data = File.Exists(filePath) ?
                //(GameData)ReadFile(filePath) :
                //new GameData();
                
               // if (data == null)
                {
                    
                   // print("Failed to load game save");
                    //data = new GameData();
                }
            }
            //else
            {
                global.games[global.currentGame] = global.gameCount++;
                data = new GameData();
                //data.password = password;
                setPassword(password);
                //Debug.Log("Det lösenord som sparas är: " + data.password);
            }
            // add player to math course for now
            Course m = new MultiplicationCourse();
            data.coruses.Add(m); 

            if (recive.c == null) setCurrentcourse(m);

            //Debug.Log("Nu sätts " + recive.c + " till currentcourse i GameControllers NewGame.");
            //GameObject conn = GameObject.Find("ConnectionHandler");
            //recive = conn.GetComponent<Recive>();
            recive.setCourseList(data.coruses);
            
           AskForNewQuestions();
            // one less variation to test if we save and load every time
            //if (name != "testmode")
            
            SaveGame();
            LoadGame(name, password);
            
        }
	}

    public void setCurrentcourse( Course m)
    {
        if (data != null)
        {
             if(!data.coruses.Contains(m))
                data.coruses.Add(m);
            Course old = data.currentCourse;
            data.currentCourse = m;
            if (old != null) data.coruses.Remove(old);
        }
    }
	public Item TranslateItem(string name)
	{
		return (Item) Enum.Parse(typeof(Item), name, true);
	}
	
	public bool NameTaken(string name)
	{
		var cand = name.Trim().ToLower();
		foreach (var key in global.games.Keys)
			if (cand == key.ToLower())
				return true;
		return false;
	}

	public bool NameInvalid(string name)
	{
		return name == null
			|| name != name.Trim()
			|| name.Length < 1;
	}

	private string SaveFileName(string name)
	{

		return Application.persistentDataPath
			+ "/game_" + global.games[name] + ".dat";
	}

	private void WriteFile(string filePath, object o)
	{
		var file = File.Create(filePath);
		var bf = new BinaryFormatter();
		bf.Serialize(file, o);
		file.Close();
	}

	private void SaveGame()
	{
		var scene = SceneManager.GetActiveScene().name;
		if (scene != "startmenu")
			data.currentScene = scene;
		if (global.currentGame != null)
			WriteFile(SaveFileName(global.currentGame), data);
        //Debug.Log("lösenordet sparas som: " + data.password);
	}

	private void SaveGlobal()
	{
		WriteFile(Application.persistentDataPath + "/global.dat", global);
	}

	private object ReadFile(string filePath)
	{
		object o = null;
		var bf = new BinaryFormatter();
		var file = File.Open(filePath, FileMode.Open);
		try {
			o = bf.Deserialize(file);
		} catch (SerializationException e) {
			// class definition probably changed, discard this save
		}
		file.Close();
		return o;
	}

	public void LoadGame(string name, string password)
	{
        //Debug.Log("I GameControllers LoadGame är password = " + password); 
        if (recive.Login(name, password) || !recive.Online())
        {
            
            global.currentGame = name;
            this.name = name.Trim();
            if (name == "testmode") testmode = true;

            var filePath = SaveFileName(name);
            GameData content = null;
            if (File.Exists(filePath))
            {
                content = (GameData)ReadFile(filePath);
                //Debug.Log(" Det fanns en sparfil för " + name + " Lösenordet är: " + content.password);
            }
            else
            {
                content = new GameData();
                //Debug.Log(" Det fanns ingen sparfil för " + name);
            }

            if (content == null)
            {
                print("Failed to load game save");
                content = new GameData();
            }

            data = content;

            if (recive.Online()) setPassword(password);

            //Debug.Log(" Det sparade lösenordet är: " + content.password
            //+ " Den sparade currencourse är: " + content.currentCourse + "CurrentScene är: " + content.currentScene);

           if (data.password != password)
            {
                Debug.Log("wrong password" + "! Det rätta lösenordet är: " + content.password);
                return;
            }

            
            sceneHandler.ChangeScene("new", data.currentScene);
            //GameObject conn = GameObject.Find("ConnectionHandler");
            //recive = conn.GetComponent<Recive>();
            Course m = new MultiplicationCourse();
            if(!data.coruses.Contains(m)) data.coruses.Add(m);
            recive.setCourseList(data.coruses);
            Debug.Log(" När detta spelet börjar finns det " + data.coruses.Count + " kurser i sparfilen för " + name);
            string s = "Det är:";
            foreach (Course c in data.coruses) s = s + " " + c.getCoursecode();
            Debug.Log(s);
            //recive.authentication();
            AskForNewQuestions();
            if (data.currentCourse == null) setCurrentcourse(m);
        }
    }

	public void DeleteGame(string name) // not tested
	{
		var s = name.Trim();
		var filePath = SaveFileName(name);
		global.games.Remove(name);
		File.Delete(filePath);
		if (global.currentGame == s)
			global.currentGame = null;
	}

	public List<string> GetNames()
	{
		var names = new List<String>(global.games.Keys);
		names.Sort();
		if (global.currentGame != null) {
			names.Remove(global.currentGame);
			names.Insert(0, global.currentGame);
		}
		return names;
	}

	private void LoadGlobal()
	{
		var filePath = Application.persistentDataPath + "/global.dat";
		var blob = File.Exists(filePath) ?
			(GlobalData) ReadFile(filePath) :
			new GlobalData();
		if (blob == null) {
			print("Failed to load global save");
			blob = new GlobalData();
		}
		global = blob;
	}

	public int GetAmount(Item item)
	{
		return data.inventory.ContainsKey(item) ?
			data.inventory[item] : 0;
	}

	public void AddItems(Item item, int count)
	{
		data.inventory[item] = GetAmount(item) + count;
	}

	public void AddItem(Item item)
	{
		AddItems(item, 1);
	}

	public void AddItems(string name, int count)
	{
		AddItems(TranslateItem(name), count);
	}

	public void AddItem(string name)
	{
		AddItem(TranslateItem(name));
	}

	public int RemoveItems(Item item, int count) 
	{
		int have = GetAmount(item);
		int take = count > have ? have : count;
		data.inventory[item] = have - take;
		return take;
	}

	public int RemoveItem(Item item) 
	{
		return RemoveItems(item, 1);
	}

	public int RemoveItems(string name, int count)
	{
		return RemoveItems(TranslateItem(name), count);
	}

	public int RemoveItem(string name)
	{
		return RemoveItem(TranslateItem(name));
	}

	public int GetBalance()
	{
		return GetAmount(Item.OneCoin)
			+ GetAmount(Item.FiveCoin) * 5
			+ GetAmount(Item.TenCoin) * 10
			+ GetAmount(Item.TwentyBill) * 20
			+ GetAmount(Item.HundredBill) * 100
			+ GetAmount(Item.ThousandBill) * 1000;
	}

	public void AddBalance(int n)
	{
		AddItems(Item.ThousandBill, n / 1000);
		AddItems(Item.HundredBill, (n % 1000) / 100);
		AddItems(Item.TwentyBill, (n % 100) / 20);
		AddItems(Item.TenCoin, (n % 20) / 10);
		AddItems(Item.FiveCoin, (n % 10) / 5);
		AddItems(Item.OneCoin, n % 5);
	}

	public Question GetQuestion(int alternatives)
	{
        //Debug.Log(" nu är vi i gamecontrollers GetQuestion");
        if (data.currentCourse == null) {
			print("Player don't have a current course selected!");
			return null;
		}
		var q = data.currentCourse.GetQuestion(alternatives);
		data.questions.Add(q);
        //Debug.Log(" nu läggs frågan " + q + " till i data.questions");
		return q;
	}

	public void AddExp(int gainedExp)
	{
		data.experiencePoints += Math.Abs(gainedExp);
	}
}

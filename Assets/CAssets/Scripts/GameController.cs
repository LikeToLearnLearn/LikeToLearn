using UnityEngine;
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
	class GameData {
		public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
		public string currentScene = "city_centralisland";
		public List<Course> coruses = new List<Course>();
		public List<Question> questions = new List<Question>();
		public Course currentCourse = null;
		public int experiencePoints = 0;
	}

	public static GameController control;

	GameData data = null;
	GlobalData global = null;
	SceneHandler sceneHandler;
    Recive recive;

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

        // auto save every fifth secound
        InvokeRepeating("Save", 5, 5);
	}

    void Update()
    {
        AskForNewQuestions();
        SendRessults();       
    }

    void SendRessults()
    {
        //Debug.Log(" data = " + data);
        if (recive != null && data!= null)
        {
            //Debug.Log(" data.questions = " + data.questions);
            if (recive.Online() )
            {
                Debug.Log(" data.qusetions är nu " + data.questions.Count + " lång");
                Question q = null;
                //foreach (Question q in data.questions)
                for (int i = 0; i < data.questions.Count; ++i)
                {
                    q = data.questions[i];
                    //Debug.Log(" q är nu = " + q + " i GameController");
                    Dictionary<string, string> tmp = q.a;
                    foreach (KeyValuePair<string, string> ans in tmp)                        
                    {
                        recive.sendResult(q.coursecode, q.momentcode, q.question, ans.Key, ans.Value);
                        Debug.Log("Försöker skicka :" + q.coursecode + q.momentcode + q.question + ans.Key + ans.Value);
                        q.a.Remove(ans.Key);
                    }
                    //data.questions.Remove(q);
                }

            }
        }

    }

    void AskForNewQuestions()
    {
        if (recive != null)
        {
            //Debug.Log(recive.Online() + " online?");
            if (false/*recive.Online()*/)
            {
                if (recive.getNewQuestions())
                {
                    setCurrentcourse(recive.c);
                }
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

	public void NewGame(string name)
	{
		name = name.Trim();
		if (NameTaken(name) || NameInvalid(name)) return;
		global.currentGame = name;
		data = new GameData();
		global.games[global.currentGame] = global.gameCount++;

		// add player to math course for now
		Course m = new MultiplicationCourse();
		data.coruses.Add(m);
        //data.currentCourse = m;
        setCurrentcourse(m);
        GameObject conn = GameObject.Find("ConnectionHandler");
        recive = conn.GetComponent<Recive>();
        recive.setCourseList(data.coruses);
        
        // one less variation to test if we save and load every time
        SaveGame();
		LoadGame(name);
	}

    public void setCurrentcourse( Course m)
    {
        if (data != null) data.currentCourse = m;
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

	public void LoadGame(string name)
	{
		global.currentGame = name;
		var filePath = SaveFileName(name);
		var content = File.Exists(filePath) ?
			(GameData) ReadFile(filePath) :
			new GameData();
		if (content == null) {
			print("Failed to load game save");
			content = new GameData();
		}
		data = content;
		sceneHandler.ChangeScene("new", data.currentScene);
        //recive = new Recive(data.coruses); // Man fick visst inte skapa nya monobehaviorsaker med hjälp av new ....
        GameObject conn = GameObject.Find("ConnectionHandler");
        recive = conn.GetComponent<Recive>();
        recive.setCourseList(data.coruses);
                
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
        Debug.Log(" nu är vi i gamecontrollers GetQuestion");
        if (data.currentCourse == null) {
			print("Player don't have a current course selected!");
			return null;
		}
		var q = data.currentCourse.GetQuestion(alternatives);
		data.questions.Add(q);
        Debug.Log(" nu läggs frågan " + q + " till i data.questions");
		return q;
	}

	public void AddExp(int gainedExp)
	{
		data.experiencePoints += Math.Abs(gainedExp);
	}
}

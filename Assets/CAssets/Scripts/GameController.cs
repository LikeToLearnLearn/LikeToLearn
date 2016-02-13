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
		RedBalloon, YellowBalloon, BlueBalloon, GreenBalloon
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
		//public int questionMode = 3;
		//public string questionType = "math";
		public string currentScene = "city_centralisland";
		public List<Course> coruses = new List<Course>();
		public List<Question> questions = new List<Question>();
		public Course currentCourse = null;
		public int experiencePoints = 0;
	}

	public static GameController control;

	GameData data;
	GlobalData global;
	SceneHandler sceneHandler;
	/*
	public int questionMode {
		get { return data.questionMode; }
		set { data.questionMode = value; }			
	}

	public int questionType {
		get { return data.questionType; }
		set { data.questionType = value; }			
	}
	*/

	public int unlockWorldLevel {  // not tested
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
			data = new GameData();
		} else if (control != this) {
			Destroy(gameObject);
		}
	}

	void Start()
	{
		GameObject sco = GameObject.Find("SceneHandlerO");
		sceneHandler = sco.GetComponent<SceneHandler>();
	}

	void OnEnable()
	{
		LoadGlobal();
	}

	void OnDisable()
	{
		if (global != null) {
			SaveGlobal();
			if (data != null)
				SaveGame();
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
		Course m = new MultiplicationCoruse();
		data.coruses.Add(m);
		data.currentCourse = m;

		// one less variation to test if we save and load every time
		SaveGame();
		LoadGame(name);
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
			+ GetAmount(Item.TenCoin) * 10
			+ GetAmount(Item.HundredBill) * 100
			+ GetAmount(Item.ThousandBill) * 1000;
	}

	public void AddBalance(int n)
	{
		AddItems(Item.ThousandBill, n / 1000);
		AddItems(Item.HundredBill, (n % 1000) / 100);
		AddItems(Item.TenCoin, (n % 100) / 10);
		AddItems(Item.OneCoin, n % 10);
	}

	public Question GetQuestion(int alternatives)
	{
		var q = data.currentCourse.GetQuestion(alternatives);
		data.questions.Add(q);
		return q;
	}

	public void AddExp(int gainedExp)
	{
		data.experiencePoints += Math.Abs(gainedExp);
	}
}

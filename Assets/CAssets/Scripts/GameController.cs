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
	public enum Item { Fish, Brick, OneCoin, TenCoin, HundredBill, ThousandBill };

	// global configuration data that all instances use
	[Serializable]
	class GlobalData {
		public string currentGame = "";
		public Language lang = Language.English;
		public int gameCount = 0;
		public Dictionary<string, int> games = new Dictionary<string, int>();
	}

	// data associated with a given game instance
	[Serializable]
	class GameData {
		public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
		public int questionMode = 3;
		public string currentScene = "city_centralisland";
	}

	public static GameController control;

	GameData data;
	GlobalData global;
	SceneHandler sceneHandler;

	public int questionMode {
		get { return data.questionMode; }
		set { data.questionMode = value; }			
	}

	public Dictionary<Item, int> inventory {
		get { return data.inventory; }
		set { data.inventory = value; }
	}

	public bool GotSavedGames()
	{
		return global.games.Count > 0;
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
		if (global != null) SaveGlobal();
		if (data != null) SaveGame();
	}

	public void NewGame(string name)
	{
		name = name.Trim();
		if (NameTaken(name) || NameInvalid(name)) return;
		global.currentGame = name;
		data = new GameData();
		global.games[global.currentGame] = global.gameCount++;
		SaveGame();
		LoadGame(name);
	}
	
	public bool NameTaken(string name)
	{
		return global.games.ContainsKey(name);
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
			+ "/game_"
			+ global.games[name]
			+ ".dat";
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
		if (scene != "startmenu") data.currentScene = scene;
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

	public List<string> GetNames()
	{
		var names = new List<String>(global.games.Keys);
		names.Remove(global.currentGame);
		names.Sort();
		names.Insert(0, global.currentGame);
		return names;
	}

	private void LoadGlobal()
	{
		var filePath = Application.persistentDataPath + "/global.dat";
		var g = File.Exists(filePath) ?
			(GlobalData) ReadFile(filePath) :
			new GlobalData();
		if (g == null) {
			print("Failed to load global save");
			g = new GlobalData();
		}
		global = g;
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

}

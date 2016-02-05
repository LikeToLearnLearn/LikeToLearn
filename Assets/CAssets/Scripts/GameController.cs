using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class GameController : MonoBehaviour {

	public enum Language {	English, Swedish };
	public enum Item { Fish, Brick, OneCoin, TenCoin, HundredBill, ThousandBill };

	// global configuration data that all instances use
	[Serializable]
	class GlobalData {
		public string currentGame = "";
		public Language lang = Language.English;
		public LinkedList<String> games = new LinkedList<String>();
	}

	// data associated with a given game instance
	[Serializable]
	class GameData {
		public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
	}

	public static GameController control;
	private GameData data;
	private GlobalData global;

	public string name {
		get { return global.currentGame; }
		set { global.currentGame = value; }
	}

	public Dictionary<Item, int> inventory {
		get { return data.inventory; }
		set { data.inventory = value; }
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

	void OnEnable()
	{
		Load();
	}

	void OnDisable()
	{
		Save();
	}

	public void Save()
	{
		SaveGlobal();
		SaveGame();
	}

	public void NewGame()
	{
		//if (NameTaken())
		//	print(" Should not happen..");
		global.games.AddFirst(global.currentGame);
		data = new GameData();
	}

	public bool NameTaken() {
		return global.games.Contains(global.currentGame);
	}

	private void SaveGame()
	{
		var filePath = Application.persistentDataPath
			+ "/game_" + global.currentGame.GetHashCode() + ".dat";
		var file = File.Create(filePath);
		var bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}

	private void SaveGlobal()
	{
		var filePath = Application.persistentDataPath + "/global.dat";
		var file = File.Create(filePath);
		var bf = new BinaryFormatter();
		bf.Serialize(file, global);
		file.Close();
	}

	public void Load()
	{
		LoadGlobal();
		LoadGame();
	}

	private void LoadGame()
	{
		var filePath = Application.persistentDataPath
			+ "/game_" + global.currentGame.GetHashCode() + ".dat";
		if (File.Exists(filePath)) {
			var bf = new BinaryFormatter();
			var file = File.Open(filePath, FileMode.Open);
			try {
				data = (GameData) bf.Deserialize(file);
			} catch (SerializationException e) {
				print("Failed to load game save, probably old format");
				data = new GameData();
			}
			file.Close();
		} else {
			data = new GameData();
		}
	}

	private void LoadGlobal()
	{
		var filePath = Application.persistentDataPath + "/global.dat";
		if (File.Exists(filePath)) {
			var bf = new BinaryFormatter();
			var file = File.Open(filePath, FileMode.Open);
			try {
				global = (GlobalData) bf.Deserialize(file);
			} catch (SerializationException e) {
				print("Failed to load global save, probably old format");
				global = new GlobalData();
			}
			file.Close();
		} else {
			global = new GlobalData();
		}
	}

	public int GetAmount(Item item)
	{
		if (data.inventory.ContainsKey(item))
			return data.inventory[item];
		return 0;
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

}

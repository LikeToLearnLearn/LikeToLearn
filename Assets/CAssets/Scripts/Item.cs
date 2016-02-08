using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	public static string[] items = { 
	"OneCoin", 
	"TenCoin", 
	"HundredBill", 
	"ThousandBill",
	"Brick", 
	"Fish" 
	};


	//This is used for translating item names
	public static Dictionary<string, string> itemNames = new Dictionary<string, string>() {
		{ "OneCoin", "1 coin" },
		{ "TenCoin", "10 coin" },
		{ "HundredBill", "100 bill" },
		{ "ThousandBill", "1000 bill" },
		{ "Brick", "Brick" },
		{ "Fish", "Fish" }
	};
		


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void translateItems(Dictionary<string, string> newNames){
		itemNames = newNames;
	}

}

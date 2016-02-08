﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetupAllShops : MonoBehaviour {

	public string[] shopItems;
	//public Transform newButton;
	public Transform[] cityShops;


	public static Dictionary<string, int> itemValues;

	static Dictionary<string, int> allshop = new Dictionary<string, int>() {
		{ "OneCoin", 1 },
		{ "TenCoin", 10 },
		{ "HundredBill", 100 },
		{ "ThousandBill", 1000 },
		{ "Brick", 5 },
		{ "Fish", 12 }
	};

	static Dictionary<string, int> smallshop = new Dictionary<string, int>() {
		{ "Brick", 5 },
		{ "Fish", 12 }
	};
		



	//Sets up all shops in the city.
	//Name the gameObjects different things for different shop items.
	//Drag the shops into the ShopHandlers shop-list to add them.
	void Start () {



		foreach (Transform shop in cityShops) {
			if (shop.name.Equals ("allshop")) {
				itemValues = allshop;
			} 
			else if(shop.name.Equals("smallshop")){
				itemValues = smallshop;
			}else {
				itemValues = allshop;
			}
	
			Transform shopGUI = shop.FindChild("ShopGUI");
			//shopGUI.GetComponent<ShopController> ().setUpShop(itemValues);
			shopGUI.GetComponent<ShopGUIScript> ().setUpItems(itemValues);
			shopGUI.gameObject.SetActive (false);
		
		}

	}

	// Update is called once per frame
	void Update () {

	}
}

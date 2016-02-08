using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class BuildGUIScript : ItemDisplayAbstract {


	//Temporary inventory, use player's real inventory later
	/*static Dictionary<Inventory.Item, int> tempInv = new Dictionary<Inventory.Item, int>() {
		{ Inventory.Item.OneCoin, 1 },
		{ Inventory.Item.TenCoin, 10 },
		{ Inventory.Item.HundredBill, 100 },
		{ Inventory.Item.ThousandBill, 1000 },
		{ Inventory.Item.Brick, 5 },
		{ Inventory.Item.Fish, 12 }
	};*/

	//Dictionary<Inventory.Item, int> items;

	// Use this for initialization
	void Start () {
		base.Start (5);	
//		Debug.Log ("extended");
		Dictionary<string, int> oldDictionary = base.inventory.getInventoryAsDictionary ();
		Dictionary<string, int> newDictionary = base.hideEmptyItems (oldDictionary);
		setUpShop (newDictionary);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changePage(int change){
		base.changePage (change);
	}


	public void selectItem(string s){
		base.selectItem (s);
	}

	public void actionButtonPush(){
		base.actionButtonPush ();
	}







}

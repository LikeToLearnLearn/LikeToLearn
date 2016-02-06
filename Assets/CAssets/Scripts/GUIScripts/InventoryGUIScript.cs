using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryGUIScript : ItemDisplayAbstract {

	static Dictionary<Inventory.Item, int> tempInv = new Dictionary<Inventory.Item, int>() {
		{ Inventory.Item.OneCoin, 1 },
		{ Inventory.Item.TenCoin, 10 },
		{ Inventory.Item.HundredBill, 2 },
		{ Inventory.Item.ThousandBill, 2 },
		{ Inventory.Item.Brick, 50 },
		{ Inventory.Item.Fish, 12 }
	};

	public Transform guiMenuControl;

	// Use this for initialization
	void Start () {
		base.Start (10);	
		setUpShop (tempInv);
	}

	// Update is called once per frame
	void Update () {

	}

	public void setUpShop(Dictionary<Inventory.Item, int> itemDictionary){
		base.setUpShop (itemDictionary);

	}


	public void changePage(int change){
		base.changePage (change);
	}


	public void selectItem(string s){
		base.selectItem (s);


		int nr = base.items [base.chosenItem];
		string itemText = s;
		string itemPrice = "" + nr;	//The amount
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = "Amount: " + itemPrice;

	}


	public void actionButtonPush(){
		//close inventory
	}
}

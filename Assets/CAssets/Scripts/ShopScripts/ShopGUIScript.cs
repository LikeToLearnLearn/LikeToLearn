using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class ShopGUIScript : ItemDisplayAbstract {



	// Use this for initialization
	void Start () {
		base.Start (4);	
	
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
		string itemPrice = "" + nr;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;

	}
		

	public void actionButtonPush(){
		base.inv.AddItem (chosenItem);
		Debug.Log("BOUGHT ITEM " + chosenItem.ToString());
	}

}

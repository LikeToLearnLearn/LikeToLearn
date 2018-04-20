using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryGUIScript : ItemDisplayAbstract {


	public Transform guiMenuControl;

	// Use this for initialization
	void Start () {
		base.Start (10);	
		Dictionary<string, int> oldDictionary = GameController.control.stringInventory;
        //Dictionary<string, int> oldDictionary2 = base.hideMoney (oldDictionary);
        //Dictionary<string, int> newDictionary = base.hideEmptyItems (oldDictionary2);
        Dictionary<string, int> newDictionary = base.hideEmptyItems(oldDictionary);
        setUpItems (newDictionary);
	}

	
    // Update is called once per frame
	void Update () {

	}

	void OnEnable(){
		//Debug.Log("updating inventory extended");
		Dictionary<string, int> oldDictionary = GameController.control.stringInventory;
		Dictionary<string, int> oldDictionary2 = base.hideMoney (oldDictionary);
		Dictionary<string, int> newDictionary = base.hideEmptyItems (oldDictionary2);
		setUpItems (newDictionary);
		SetMoney ();
	}


	/*public void setUpItems(Dictionary<string, int> itemDictionary){
		base.setUpItems (itemDictionary);

	}


	public void changePage(int change){
		base.changePage (change);
	}*/


	public override void selectItem(string s){
		base.selectItem (s);

		int nr = base.items [base.chosenItem];
		string itemText = s;
		string itemPrice = "" + nr;	//The amount
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = "Amount: " + itemPrice;

	}

	private void SetMoney(){
		int money = GameController.control.GetBalance();
		transform.FindChild ("Panel").FindChild ("MoneyPanel").FindChild ("MoneyText").GetComponent<Text> ().text = "Money: " + money.ToString();
	}


	/*public void actionButtonPush(){
	     base.actionButtonPush();
		//close inventory
	}*/
}

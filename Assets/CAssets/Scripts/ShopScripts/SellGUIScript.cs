using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class SellGUIScript : ItemDisplayAbstract {

	Button payButton;

	// Use this for initialization
	void Start () {
		base.Start (4);	
		payButton = transform.FindChild ("Panel").FindChild ("Options").FindChild ("ActionButton").GetComponent<Button> ();
		payButton.interactable = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void setUpShop(string name, string descr){
		setTitle (name);
		setDescription (descr);
	}

	public override void setTitle(string name){
		Text t = transform.FindChild("Panel").FindChild("Description").FindChild("ShopName").GetComponent<Text>();
		t.text = name;
	}
	public void setDescription(string descr){
		Text t = transform.FindChild("Panel").FindChild("Description").FindChild("ShopDescription").GetComponent<Text>();
		t.text = descr;
	}


	public override void selectItem(string s){
		base.selectItem (s);
		int nr = base.items [base.chosenItem];
		payButton.interactable = false;
		//GameController.control.stringInventory.ContainsKey (base.chosenItem)

		if (GameController.control.stringInventory.ContainsKey (base.chosenItem) && GameController.control.stringInventory[base.chosenItem] > 0) {
			string itemText = s;
			string itemPrice = "Sell for: " + nr;
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;
			payButton.interactable = true;
		} else {
			string itemText = "You don't " + '\n' +  "have this " + '\n' +  "item!";
			string itemPrice = "Sell for:" + nr;
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;
		}


	}


	public override void actionButtonPush(){

		Transform sellGUI = transform.parent.parent.FindChild("SellingGUI");

		sellGUI.gameObject.SetActive (true);

		sellGUI.GetComponent<SellScript>().SetUpPayGUI(base.GetChosenItemValue(), base.chosenItem, 1);

		gameObject.SetActive(false);

	}


	public void CancelShop()
	{
		gameObject.SetActive(false);
	}

}

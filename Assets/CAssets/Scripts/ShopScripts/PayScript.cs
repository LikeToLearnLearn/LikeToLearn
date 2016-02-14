using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PayScript : MonoBehaviour {

	Dictionary<string, int> inventory;

	static Dictionary<string, string> CoinValues = new Dictionary<string, string>() {
		{ "1", "OneCoin" },
		{ "5", "FiveCoin" },
		{ "10", "TenCoin" },
		{ "20", "TwentyBill" },
		{ "100", "HundredBill" },
		{ "1000", "ThousandBill" }
	};

	// Use this for initialization
	void Start () {


			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUpPayGUI(int price){

		//Add this init somewhere, probably in GameController
		GameController.control.AddItems ("OneCoin", 0);
		GameController.control.AddItems ("FiveCoin", 3);
		GameController.control.AddItems ("TenCoin", 0);
		GameController.control.AddItems ("TwentyBill", 0);
		GameController.control.AddItems ("HundredBill", 0);
		GameController.control.AddItems ("ThousandBill", 0);

		inventory = GameController.control.stringInventory;

		Transform val = transform.FindChild ("PayPanel").FindChild ("Values");
		foreach(Transform type in val){
			//Debug.Log("type: " + type.name);
			Text t = type.FindChild ("Text").GetComponent<Text> ();
			t.text = "" + 0;
		}

		SetItemPrice (price);
	}

	public void SetItemPrice(int price){
		Text t = transform.FindChild ("PayPanel").FindChild ("PriceText").GetComponent<Text>();
		t.text = "You need to pay: " + price;
	}

	public void AmountTextDecrease(string value){
		Text t = transform.FindChild ("PayPanel").FindChild ("Values").
			FindChild (value).FindChild ("Text").GetComponent<Text>();
		int amount = int.Parse (t.text);
		if (amount > 0) {
			amount = amount - 1;
			t.text = "" + amount;
		}
	}

	public void AmountTextIncrease(string value){
		
		Text t = transform.FindChild ("PayPanel").FindChild ("Values").
			FindChild (value).FindChild ("Text").GetComponent<Text>();
		int amount = int.Parse (t.text);

		string currentValue = CoinValues[value];

		if (amount < inventory[currentValue]) {
			amount = amount + 1;
			t.text = "" + amount;
		}

	}

}

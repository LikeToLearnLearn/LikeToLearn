using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SellScript : MonoBehaviour {

	Dictionary<string, int> inventory;
	int toPay;
	int itemPrice;
	int itemAmount;
	string item;

	public static Dictionary<string, string> CoinValues = new Dictionary<string, string>() {
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

	public void SetUpPayGUI(int price, string chosenItem, int amount){

		//Add this init somewhere, probably in GameController
		GameController.control.AddItems ("OneCoin", 0);
		GameController.control.AddItems ("FiveCoin", 0);
		GameController.control.AddItems ("TenCoin", 0);
		GameController.control.AddItems ("TwentyBill", 0);
		GameController.control.AddItems ("HundredBill", 0);
		GameController.control.AddItems ("ThousandBill", 0);

		inventory = GameController.control.stringInventory;

		itemAmount = amount;
		item = chosenItem;
		itemPrice = price;
		SetItemPrice (itemPrice);

		Texture tex = Resources.Load(item) as Texture;
		RawImage im = transform.FindChild("PayPanel").FindChild ("FinishPanel").FindChild("Amount").
            FindChild("ImagePanel").FindChild("RawImage").GetComponent<RawImage>();
		im.texture = tex;

		Text amountText = transform.FindChild("PayPanel").FindChild ("FinishPanel").FindChild("Amount").
            FindChild("AmountPanel").FindChild("AmountText").GetComponent<Text>();
		amountText.text = "" + amount;

	}

	public void SetItemPrice(int price){
		toPay = price;
		Text t = transform.FindChild ("PayPanel").FindChild ("TextPanel").FindChild ("PriceText").GetComponent<Text>();
		t.text = "You will get: " + price;
		Text t2 = transform.FindChild ("PayPanel").FindChild ("TextPanel").FindChild ("MoneyText").GetComponent<Text>();
		t2.text = "You have: " + GameController.control.GetBalance ();
	}

	/*public void AmountTextDecrease(string value){
		Text t = transform.FindChild ("PayPanel").FindChild ("ValuePanel").FindChild ("Values").
			FindChild (value).FindChild ("Text").GetComponent<Text>();
		int amount = int.Parse (t.text);
		if (amount > 0) {
			amount = amount - 1;
			t.text = "" + amount;
		}

	}

	public void AmountTextIncrease(string value, int increaseAmount){

		Text t = transform.FindChild ("PayPanel").FindChild ("ValuePanel").FindChild ("Values").
			FindChild (value).FindChild ("Text").GetComponent<Text>();
		int amount = int.Parse (t.text);

		string currentValue = CoinValues[value];

		if (amount < inventory[currentValue]) {
			amount = amount + increaseAmount;
			t.text = "" + amount;
		}

	}*/


	public void ItemAmountDecrease(int decreaseAmount){
		Text t = transform.FindChild ("PayPanel").FindChild ("FinishPanel").FindChild ("Amount").
            FindChild("AmountPanel").FindChild ("AmountText").GetComponent<Text>();
		int amount = int.Parse (t.text);
		if (amount - decreaseAmount >= 0) {
			amount = amount - decreaseAmount;
			t.text = "" + amount;
			itemAmount = amount;
		} else {
			amount = 0;
			t.text = "" + amount;
			itemAmount = amount;
		}
		SetItemPrice (amount * itemPrice);
	}

	public void ItemAmountIncrease(int increaseAmount){

		Text t = transform.FindChild ("PayPanel").FindChild ("FinishPanel").FindChild ("Amount").
            FindChild("AmountPanel").FindChild ("AmountText").GetComponent<Text>();
		int amount = int.Parse (t.text);

		if (inventory[item] >= amount + increaseAmount) {
			amount = amount + increaseAmount;
			t.text = "" + amount;
			itemAmount = amount;
			SetItemPrice (amount * itemPrice);
		}
	}
		

	public void FinishPayment(){
		GameController.control.RemoveItems (item, itemAmount);
		GameController.control.AddBalance (toPay);

		gameObject.SetActive (false);
	}

	public void QuitPayment(){
		gameObject.SetActive (false);
	}

}

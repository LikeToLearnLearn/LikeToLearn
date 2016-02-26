using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PayScript : MonoBehaviour {

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

    public static Dictionary<string, int> CoinsToValues = new Dictionary<string, int>() {
        { "OneCoin", 1 },
        { "FiveCoin", 5 },
        { "TenCoin", 10 },
        { "TwentyBill", 20 },
        { "HundredBill", 100 },
        { "ThousandBill", 1000 }
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

		Transform val = transform.FindChild ("PayPanel").FindChild ("ValuePanel").FindChild ("Values");
		foreach(Transform type in val){
			Text t = type.FindChild ("Text").GetComponent<Text> ();
			t.text = "" + 0;
		}
			
		itemAmount = amount;
		item = chosenItem;
		itemPrice = price;
		SetItemPrice (itemPrice);

		Texture tex = Resources.Load(item) as Texture;
		RawImage im = transform.FindChild("PayPanel").FindChild ("FinishPanel").FindChild("Amount").
			FindChild("RawImage").GetComponent<RawImage>();
		im.texture = tex;

		Text amountText = transform.FindChild("PayPanel").FindChild ("FinishPanel").FindChild("Amount").
            FindChild("AmountText").GetComponent<Text>();
        amountText.text = "" + amount;

    }

	public void SetItemPrice(int price){
		toPay = price;
		Text t = transform.FindChild ("PayPanel").FindChild ("TextPanel").FindChild ("PriceText").GetComponent<Text>();
		t.text = "You need to pay: " + price;
		Text t2 = transform.FindChild ("PayPanel").FindChild ("TextPanel").FindChild ("MoneyText").GetComponent<Text>();
		t2.text = "You have: " + GameController.control.GetBalance ();
	}

	public void AmountTextDecrease(string value){
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

	}
		

	public void ItemAmountDecrease(int decreaseAmount){
		Text t = transform.FindChild ("PayPanel").FindChild ("FinishPanel").FindChild ("Amount").
			FindChild ("AmountText").GetComponent<Text>();
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
			FindChild ("AmountText").GetComponent<Text>();
		int amount = int.Parse (t.text);

		amount = amount + increaseAmount;
		t.text = "" + amount;
		itemAmount++;
		SetItemPrice (amount * itemPrice);
	}

	public void CheckPayment(){
		int total = 0;
		int[] typeAmounts = new int[10];
		int i = 0;

		Transform val = transform.FindChild ("PayPanel").FindChild ("ValuePanel").FindChild ("Values");
		foreach(Transform type in val){
			Text t = type.FindChild ("Text").GetComponent<Text> ();
			int amount = int.Parse (t.text) * int.Parse (type.name);

			total = total + amount;
			//Debug.Log("type amount for " + type.name + " is: " + t.text);
			typeAmounts [i] = int.Parse (t.text);
			i++;
		}

		if (total == toPay) {
			FinishPayment (typeAmounts);
		} else {
			Text t = transform.FindChild ("PayPanel").FindChild ("FinishPanel").FindChild ("PayText").GetComponent<Text>();
			t.text = "Wrong!" + '\n' + "You paid " + total;
		}

	}

	private void FinishPayment(int[] amounts){

		GameController.control.RemoveItems ("OneCoin", amounts[0]);
		GameController.control.RemoveItems ("FiveCoin", amounts[1]);
		GameController.control.RemoveItems ("TenCoin", amounts[2]);
		GameController.control.RemoveItems ("TwentyBill", amounts[3]);
		GameController.control.RemoveItems ("HundredBill", amounts[4]);

		GameController.control.AddItems (item, itemAmount);

		gameObject.SetActive (false);

	}

	public void QuitPayment(){
		gameObject.SetActive (false);
	}

}

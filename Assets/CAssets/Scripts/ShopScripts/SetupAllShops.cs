using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetupAllShops : MonoBehaviour {

	public string[] shopItems;
	public Transform[] cityShops;
	public Transform[] sellShops;


	public static Dictionary<string, int> itemValues;

	static Dictionary<string, int> allshop = new Dictionary<string, int>() {
		{ "OneCoin", 1 },
		{ "TenCoin", 10 },
		{ "HundredBill", 100 },
		{ "ThousandBill", 1000 },
		{ "Brick", 5 },
		{ "Fish", 12 }
	};

    static Dictionary<string, int> bank = new Dictionary<string, int>() {
        { "OneCoin", 1 },
        { "FiveCoin", 5 },
        { "TenCoin", 10 },
        { "TwentyBill", 20 },
		{ "HundredBill", 100 }
    };
		

    static Dictionary<string, int> smallshop = new Dictionary<string, int>() {
		{ "Brick", 5 },
		{ "Fish", 12 }
	};

	static Dictionary<string, int> brickshop = new Dictionary<string, int>() {
		{ "Brick", 0 },
		{ "BlueBrick", 0 },
		{ "GreenBrick", 0 },
		{ "YellowBrick", 1 },
		{ "GlassBlock", 10 }
	};

	static Dictionary<string, int> glassshop = new Dictionary<string, int>() {
		{ "GlassBlock", 10 }
	};

	static Dictionary<string, int> allsell = new Dictionary<string, int>() {
		{ "Fish", 5 },
		{ "Brick", 2 },
		{ "BlueBrick", 2 },
		{ "GreenBrick", 2 },
		{ "YellowBrick", 2 },
		{ "GlassBlock", 5 }
	};

	static Dictionary<string, int> fishsell = new Dictionary<string, int>() {
		{ "Fish", 12 },
	};
		



	//Sets up all shops in the city.
	//Name the gameObjects different things for different shop items.
	//Drag the shops into the ShopHandlers shop-list to add them.
	void Start () {

		string name = "Shop name";
		string descr = "Description";

		foreach (Transform shop in cityShops) {
			if (shop.name.Equals ("allshop")) {
				itemValues = allshop;
				name = "The Everything Shop";
				descr = "This shop sells everything.";
			} 
			else if(shop.name.Equals("smallshop")){
				itemValues = smallshop;
				name = "The Small Shop";
				descr = "This shop sells a few things.";
			}else if(shop.name.Equals("brickshop")){
				itemValues = brickshop;
				name = "The Brick Shop";
				descr = "This shop sells bricks of all types.";
			}else if(shop.name.Equals("glassshop")){
				name = "The Glass Shop";
				descr = "This shop sells glass items.";
				itemValues = glassshop;
			}
            else if (shop.name.Equals("bank"))
            {
                itemValues = bank;
				name = "The Bank";
				descr = "This is the bank. Here you can exchange your money into different values by 'buying' money.";
            }
            else {
				itemValues = smallshop;
				name = "The Small Shop";
				descr = "This shop sells a few things.";
			}
	
			Transform shopGUI = shop.FindChild("ShopGUI");
			shopGUI.GetComponent<ShopGUIScript> ().setUpShop(name, descr);
			shopGUI.GetComponent<ShopGUIScript> ().setUpItems(itemValues);
			shopGUI.gameObject.SetActive (false);
		}



		foreach (Transform shop in sellShops) {
			if (shop.name.Equals ("allsell")) {
				itemValues = allsell;
				name = "Flea Market";
				descr = "Here you can sell all your stuff! (For a slightly worse price...)";
			} 
			else if(shop.name.Equals ("fishsell")){
				Debug.Log ("Setting up fish shop");
				itemValues = fishsell;
				name = "The Fish Market";
				descr = "Here you can sell fish.";
			}
			else {
				itemValues = allsell;
				name = "Flea Market";
				descr = "Here you can sell all your stuff! (For a slightly worse price...)";
			}

			Transform sellGUI = shop.FindChild("SellGUI");
			sellGUI.GetComponent<SellGUIScript> ().setUpShop(name, descr);
			sellGUI.GetComponent<SellGUIScript> ().setUpItems(itemValues);
			sellGUI.gameObject.SetActive (false);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}

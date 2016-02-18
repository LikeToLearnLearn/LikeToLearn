using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetupAllShops : MonoBehaviour {

	public string[] shopItems;
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

    static Dictionary<string, int> bank100 = new Dictionary<string, int>() {
        { "OneCoin", 100 },
        { "FiveCoin", 100 },
        { "TenCoin", 100 },
        { "TwentyBill", 100 },
    };

    static Dictionary<string, int> bank20 = new Dictionary<string, int>() {
        { "OneCoin", 20 },
        { "FiveCoin", 20 },
        { "TenCoin", 20 },
    };

    static Dictionary<string, int> bank10 = new Dictionary<string, int>() {
        { "OneCoin", 10 },
        { "FiveCoin", 10 },
    };

    static Dictionary<string, int> bank5 = new Dictionary<string, int>() {
        { "OneCoin", 5 },
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
			}else if(shop.name.Equals("brickshop")){
				itemValues = brickshop;
			}else if(shop.name.Equals("glassshop")){
				itemValues = glassshop;
			}
            else if (shop.name.Equals("bank100"))
            {
                itemValues = bank100;
            }
            else if (shop.name.Equals("bank20"))
            {
                itemValues = bank20;
            }
            else if (shop.name.Equals("bank10"))
            {
                itemValues = bank10;
            }
            else if (shop.name.Equals("bank5"))
            {
                itemValues = bank5;
            }
            else {
				itemValues = allshop;
			}
	
			Transform shopGUI = shop.FindChild("ShopGUI");
			shopGUI.GetComponent<ShopGUIScript> ().setUpItems(itemValues);
			shopGUI.gameObject.SetActive (false);
		
		}

	}

	// Update is called once per frame
	void Update () {

	}
}

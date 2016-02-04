using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetupAllShops : MonoBehaviour {

	public string[] shopItems;
	//public Transform newButton;
	public Transform[] cityShops;


	public static Dictionary<Inventory.Item, int> itemValues;

	static Dictionary<Inventory.Item, int> miscShop = new Dictionary<Inventory.Item, int>() {
		{ Inventory.Item.OneCoin, 1 },
		{ Inventory.Item.TenCoin, 10 },
		{ Inventory.Item.HundredBill, 100 },
		{ Inventory.Item.ThousandBill, 1000 },
		{ Inventory.Item.Brick, 5 },
		{ Inventory.Item.Fish, 12 }
	};

	static Dictionary<Inventory.Item, int> buildShop = new Dictionary<Inventory.Item, int>() {
		{ Inventory.Item.OneCoin, 1 },
		{ Inventory.Item.TenCoin, 10 },
		{ Inventory.Item.HundredBill, 100 },
		{ Inventory.Item.Brick, 10 },
		{ Inventory.Item.Fish, 12 }
	};



	// Use this for initialization
	//Sets up all shops in the city.
	//Drag the shops into the ShopHandlers shop-list to add more
	void Start () {



		foreach (Transform shop in cityShops) {
			if (shop.name.Equals ("miscshop")) {
				itemValues = miscShop;
			} else {
				itemValues = buildShop;
			}
	
			Transform shopGUI = shop.FindChild("ShopGUI");
			shopGUI.GetComponent<ShopController> ().setUpShop(itemValues);
			shopGUI.gameObject.SetActive (false);
		
		}

	}

	// Update is called once per frame
	void Update () {

	}
}

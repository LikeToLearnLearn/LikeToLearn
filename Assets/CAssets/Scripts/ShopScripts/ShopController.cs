﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {

	//Sets up and controls the shop its linked to

	//Further development:
	//Every shop takes in a list of id's?
	//And uses the id's to read in name, picture etc?
	public string[] shopItems;
	public Transform newButton;
	int currentPage;
	int itemsPerPage;

	//Temporary until inventory is implemented
	enum Item { Brick1, Fish };
	static Dictionary<Item, int> itemValue = new Dictionary<Item, int>() {
		{ Item.Brick1, 3 },
		{ Item.Fish, 15 }
	};


	List<string> inStock = new List<string>();
	Dictionary<string, int> shopItemList = new Dictionary<string, int>();

	// Use this for initialization
	void Start () {
		currentPage = 0;
		itemsPerPage = 4;

		foreach (KeyValuePair<Item, int> item in itemValue) {
			inStock.Add (item.Key.ToString());
			shopItemList.Add (item.Key.ToString(), item.Value);

		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUpShop(){

		//Clear the button panel
		Transform itemButtons = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
		foreach (Transform button in itemButtons.transform) {
			GameObject.Destroy (button.gameObject);
		}

		//Set up the right number of buttons (currently 4 per page)
		/*int start = currentPage * 4;
		for (int i = start; (i <= start + 3) && i < shopItems.Length; i++) {
			string s = shopItems [i];
			Transform clone = (Transform)Instantiate (newButton, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.parent = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
			Text t = clone.FindChild ("Text").GetComponent<Text>();
			t.text = s;
		}*/

		//Set up the right number of buttons (currently 4 per page)
		int start = currentPage * 4;
		for (int i = start; (i <= start + 3) && i < inStock.Count; i++) {

			string s = inStock [i];
			Transform clone = (Transform)Instantiate (newButton, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.parent = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
			Text t = clone.FindChild ("Text").GetComponent<Text>();
			t.text = s;
		}


		setScrollButtons ();

	}

	public void setScrollButtons(){
		Button prevButton = 
			transform.FindChild ("Panel").FindChild ("ButtonPanel").FindChild ("PrevButton").GetComponent<Button>();
		Button nextButton = 
			transform.FindChild ("Panel").FindChild ("ButtonPanel").FindChild ("NextButton").GetComponent<Button>();

		if (currentPage == 0) {
			prevButton.interactable = false;
		} else {
			prevButton.interactable = true;
		}
		//If the last item on page is lower than last existing item
		int lastItem = (currentPage * itemsPerPage) + 3;
		//lastitem = 0*4 + 3 = 3
		//lastitem = 1*4 + 3 = 7
		//if 4 < 4
		if (lastItem + 1 < shopItems.Length) {
			nextButton.interactable = true;
		}
		else {
			nextButton.interactable = false;
		}
	}

	public void changePage(int change){
		currentPage = currentPage + change;
		setUpShop ();
	}

	public void selectItem(UnityEngine.EventSystems.BaseEventData baseEvent){
		Debug.Log(baseEvent.selectedObject.name + " triggered an event!");
		
	}

}

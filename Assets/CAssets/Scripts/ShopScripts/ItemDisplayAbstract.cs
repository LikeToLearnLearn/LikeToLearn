using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;



public abstract class ItemDisplayAbstract : MonoBehaviour {


	//Used to display items. For shops, inventory, building, or other.
	//Takes a Dictionary of Items and int, where int is:
	//For shops: the item's price
	//For inventory & build: how many of the items the player has

	//Overwrite the actionButtonPush() method to give it different results.
	//For example adding the item to inventory for shops,
	//or placing it for build

	//Needs the following GUI elements in the following hierarchy, but their appearance can be different:
	//GameObject (can be named anything)
	//	EventSystem
	//	Canvas (can be named anything)
	//		Panel (a panel with some layout)
	//			ButtonPanel (a panel with some layout)
	//				PrevButton (a button)
	//				ItemButtons (a Panel with some layout)
	//				NextButton	(a button)
	//			Options (a panel with some layout)
	//				ActionButton (a button)



	public Transform newButton;	//The button to be used for the items
	public PlayerInventory inventory;

	public Dictionary<string, int> items;	
	public string chosenItem;
	int currentPage;
	int itemsPerPage;
	Button actionButton;

	List<string> inStock = new List<string>();

	//Call setUpShop() in Start() if GUI is to be displayed immediately on start
	//Call setUpShop() in some other script if the GUI should be displayed on for example a trigger
	public void Start (int perPage) {
		
		currentPage = 0;
		itemsPerPage = perPage;

		actionButton = 
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ActionButton").GetComponent<Button>();

		inventory = GameObject.Find ("GameController").transform.FindChild ("InventoryHandlerO").GetComponent<PlayerInventory>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void setUpShop(Dictionary<string, int> itemDictionary){
//		Debug.Log ("original setupshop");
		items = itemDictionary;

		foreach (KeyValuePair<string, int> item in items) {
			inStock.Add (item.Key.ToString());
		}

		updateShop ();


	}

	public void updateShop(){
//		Debug.Log ("original updateshop");
		//Clear the button panel
		Transform itemButtons = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
		foreach (Transform button in itemButtons.transform) {
			GameObject.Destroy (button.gameObject);
		}



		//Set up the right number of buttons (currently 4 per page)
		//Debug.Log ("nr of items: " + items.Count);
		int start = currentPage * itemsPerPage;
		for (int i = start; (i <= start + (itemsPerPage - 1)) && i < inStock.Count; i++) {
			
			string s = inStock [i];


			Transform clone = (Transform)Instantiate (newButton, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.parent = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
			//Debug.Log ("clone parent: " + clone.parent);

			Text t = clone.FindChild ("Text").GetComponent<Text>();
			t.text = s;
			t.color = Color.clear;	//To hide the text

			Texture tex = Resources.Load(s) as Texture;
			RawImage im = clone.gameObject.GetComponent<RawImage>();
			im.texture = tex;

		}
			
		setScrollButtons ();

	}


	public void setScrollButtons(){
//		Debug.Log ("original setscrollbuttons");
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
		int lastItem = (currentPage * itemsPerPage) + (itemsPerPage - 1);

		if (lastItem + 1 < inStock.Count) {
			nextButton.interactable = true;
		}
		else {
			nextButton.interactable = false;
		}
	}

	public void changePage(int change){
//		Debug.Log ("original changepage");
		currentPage = currentPage + change;
		updateShop ();
	}
		

	public void selectItem(string s){
//		Debug.Log ("original clickbutton");

		//chosenItem = Inventory.itemStringToEnum [s];
		chosenItem = s;



	}

	public void actionButtonPush(){
		Debug.Log("ACTION BUTTON PUSHED ON " + chosenItem.ToString());
	}

	public Dictionary<string, int> hideEmptyItems(Dictionary<string, int> itemDictionary){
		Dictionary<string, int> newDictionary = new Dictionary<string, int> ();
		foreach(string item in itemDictionary.Keys){
			if (itemDictionary[item] > 0) {
				newDictionary.Add (item, itemDictionary[item]);
			}
		}
		return newDictionary;
	}



}

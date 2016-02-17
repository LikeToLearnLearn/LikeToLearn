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
	public Dictionary<string, int> inventory;
	Vector3 normalScale;

	public Dictionary<string, int> items;	
	public string chosenItem;
	int currentPage;
	int itemsPerPage;
	Button actionButton;

	List<string> inStock = new List<string>();

	//Call setUpItems() in Start() if GUI is to be displayed immediately on start
	//Call setUpItems() in some other script if the GUI should be displayed on for example a trigger
	public void Start (int perPage) {
		
		currentPage = 0;
		itemsPerPage = perPage;
		normalScale = new Vector3 (1.0f, 1.0f, 1.0f);

		actionButton = 
			transform.FindChild ("Panel").FindChild ("Options").FindChild ("ActionButton").GetComponent<Button>();

		inventory = GameController.control.stringInventory;



		updateItems ();
	}

	// Update is called once per frame
	void Update () {
		

	}

	/*public virtual void OnEnable(){
	}*/

	public virtual void setUpItems(Dictionary<string, int> itemDictionary){
		Debug.Log("setting up items");
		items = itemDictionary;

		inStock.Clear ();
		foreach (KeyValuePair<string, int> item in items) {
			inStock.Add (item.Key.ToString());
		}
		updateItems ();



	}

	public virtual void updateItems(){
		Debug.Log("updating items");
		//Clear the button panel
		Transform itemButtons = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
		foreach (Transform button in itemButtons.transform) {
			GameObject.Destroy (button.gameObject);
		}


		//Set up the right number of buttons (currently 4 per page)
		int start = currentPage * itemsPerPage;

		for (int i = start; (i <= start + (itemsPerPage - 1)) && i < inStock.Count; i++) {
			
			string s = inStock [i];

			Transform clone = (Transform)Instantiate (newButton, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.SetParent(transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons"));

			Text t = clone.FindChild ("Text").GetComponent<Text>();
			t.text = s;
			t.color = Color.clear;	//To hide the text

			clone.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			Button b = clone.GetComponent<Button> ();
			b.enabled = true;

			Texture tex = Resources.Load(s) as Texture;
			RawImage im = clone.gameObject.GetComponent<RawImage>();
			im.texture = tex;

		}
			

	}


	public virtual void setScrollButtons(){
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

	public virtual void changePage(int change){
		currentPage = currentPage + change;
		updateItems ();
	}
		

	public virtual void selectItem(string s){

		chosenItem = s;



	}

	public virtual int GetChosenItemValue(){
		return items [chosenItem];
			
	}

	public virtual void actionButtonPush(){
		Debug.Log("ACTION BUTTON PUSHED ON " + chosenItem.ToString());
	}

	public virtual Dictionary<string, int> hideEmptyItems(Dictionary<string, int> itemDictionary){
		Dictionary<string, int> newDictionary = new Dictionary<string, int> ();
		foreach(string item in itemDictionary.Keys){
			if (itemDictionary[item] > 0) {
				newDictionary.Add (item, itemDictionary[item]);
			}
		}
		return newDictionary;
	}

	public virtual Dictionary<string, int> hideMoney(Dictionary<string, int> itemDictionary){
		Dictionary<string, int> newDictionary = new Dictionary<string, int> ();
		foreach(string item in itemDictionary.Keys){
			if (  !((item.Equals("OneCoin")) ||  (item.Equals("FiveCoin")) ||  (item.Equals("TenCoin")) || 
				(item.Equals("TwentyBill")) || (item.Equals("HundredBill")) || (item.Equals("ThousandBill"))) ) {
				newDictionary.Add (item, itemDictionary[item]);
			}
		}

		return newDictionary;
	}



}

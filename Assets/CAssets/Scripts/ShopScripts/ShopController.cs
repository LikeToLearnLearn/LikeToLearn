using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;



public class ShopController : MonoBehaviour {

	//Sets up and controls the shop it's linked to

	public Transform newButton;
	public Transform image;
	Dictionary<Inventory.Item, int> items;
	Inventory.Item chosenItem;

	int currentPage;
	int itemsPerPage;


	List<string> inStock = new List<string>();

	// Use this for initialization
	void Start () {
		currentPage = 0;
		itemsPerPage = 4;

			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUpShop(Dictionary<Inventory.Item, int> itemDictionary){
		items = itemDictionary;




		foreach (KeyValuePair<Inventory.Item, int> item in items) {
			inStock.Add (item.Key.ToString());
		}

		updateShop ();


	}

	public void updateShop(){

		//Clear the button panel
		Transform itemButtons = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
		foreach (Transform button in itemButtons.transform) {
			GameObject.Destroy (button.gameObject);
		}
			


		//Set up the right number of buttons (currently 4 per page)
		int start = currentPage * 4;
		for (int i = start; (i <= start + 3) && i < inStock.Count; i++) {
			string s = inStock [i];

			Transform clone = (Transform)Instantiate (newButton, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.parent = transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
	
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
		//Debug.Log("lastItem = " + lastItem);
		//Debug.Log("lastItem + 1  = " + (lastItem + 1));

		if (lastItem + 1 < inStock.Count) {
			//Debug.Log("ENTERED IF!");
			nextButton.interactable = true;
		}
		else {
			nextButton.interactable = false;
		}
	}

	public void changePage(int change){
		currentPage = currentPage + change;
		updateShop ();
	}

	public void selectItem(UnityEngine.EventSystems.BaseEventData baseEvent){
		Debug.Log(baseEvent.selectedObject.name + " triggered an event!");
		
	}


	public void clickButton(string s){
		//Debug.Log("BUTTON CLICK! " + s);
		//Debug.Log("object: " + transform.FindChild("Panel").FindChild("BuyOptions").FindChild("ItemText").name);

		chosenItem = Inventory.itemStringToEnum [s];

		int p = items [chosenItem];
		string itemText = s;
		string itemPrice = "" + p + " kr";
		transform.FindChild ("Panel").FindChild ("BuyOptions").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("BuyOptions").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;


	}

	public void buyItem(){
		
	}

}

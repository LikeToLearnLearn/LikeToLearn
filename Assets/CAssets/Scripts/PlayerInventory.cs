using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

	private Dictionary<string, int> acquiredItems;

	// Use this for initialization
	void Start () {
		acquiredItems = new Dictionary<string, int>();
		setupInventory ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Dictionary<string, int> getInventoryAsDictionary(){
		return acquiredItems;
	}

	public void setupInventory(){
		foreach (string i in Item.items) {
			acquiredItems.Add (i, 0);
		}
	}

	public void loadInventory(Dictionary<string, int> loadedItems){
		acquiredItems = loadedItems;
	}

	public void addItems(string item, int amount){
		acquiredItems [item] = acquiredItems [item] + amount;
	}
	public void addItem(string item){
		addItems (item, 1);
	}

	//returns true if removal was successful (there was enough items)
	public bool removeItems(string item, int amount){
		if (acquiredItems [item] - amount >= 0) {
			acquiredItems [item] = acquiredItems [item] - amount;
			return true;
		} else {
			return false;
		}

	}
	public bool removeItem(string item){
		return removeItems (item, 1);
	}


	public bool itemExists(string item){
		return acquiredItems.ContainsKey (item);
	}
	public int getItemAmount(string item){
		return acquiredItems [item];
	}

}

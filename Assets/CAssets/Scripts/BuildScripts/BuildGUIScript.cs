using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class BuildGUIScript : ItemDisplayAbstract {



	//Dictionary<Inventory.Item, int> items;

	// Use this for initialization
	void Start () {
		base.Start (5);	
//		Debug.Log ("extended");
		Dictionary<string, int> oldDictionary = GameController.control.stringInventory;
		Dictionary<string, int> oldDictionary2 = base.hideMoney (oldDictionary);
		Dictionary<string, int> newDictionary = base.hideEmptyItems (oldDictionary2);
		setUpItems (newDictionary);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*public void changePage(int change){
		base.changePage (change);
	}


	public void selectItem(string s){
		base.selectItem (s);
	}
*/
	public void actionButtonPush(){

	}







}

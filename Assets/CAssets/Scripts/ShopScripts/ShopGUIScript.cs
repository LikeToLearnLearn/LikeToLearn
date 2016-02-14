using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class ShopGUIScript : ItemDisplayAbstract {



	// Use this for initialization
	void Start () {
		base.Start (4);	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*public void setUpShop(Dictionary<string, int> itemDictionary){
		base.setUpItems (itemDictionary);

	}


	public void changePage(int change){
		base.changePage (change);
	}*/


	public override void selectItem(string s){
		base.selectItem (s);


		int nr = base.items [base.chosenItem];
		string itemText = s;
		string itemPrice = "" + nr;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;

	}
		

	public override void actionButtonPush(){

		//GameController.control.AddItem(chosenItem);
		//Debug.Log("BOUGHT ITEM " + chosenItem.ToString());


		Transform payGUI = transform.parent.parent.FindChild("PayGUI");


		payGUI.gameObject.SetActive (true);

		payGUI.GetComponent<PayScript> ().SetUpPayGUI (base.GetChosenItemValue());
		gameObject.SetActive(false);

	}

}

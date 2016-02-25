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


	public override void selectItem(string s){
		base.selectItem (s);


		int nr = base.items [base.chosenItem];
		string itemText = s;
		string itemPrice = "" + nr;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemText").GetComponent<Text> ().text = itemText;
		transform.FindChild ("Panel").FindChild ("Options").FindChild ("ItemPrice").GetComponent<Text> ().text = itemPrice;

	}
		

	public override void actionButtonPush(){

		Transform payGUI = transform.parent.parent.FindChild("PayGUI");

		payGUI.gameObject.SetActive (true);


        

        if (transform.parent.name.StartsWith("bank"))
        {
            int amount = BankExchange();
            Debug.Log("amount to buy:  " + amount);
            payGUI.GetComponent<PayScript>().SetUpPayGUI(base.GetChosenItemValue(), base.chosenItem, amount);
        }
        else {
            payGUI.GetComponent<PayScript>().SetUpPayGUI(base.GetChosenItemValue(), base.chosenItem, 1);
        }

		
		gameObject.SetActive(false);

	}

    private int BankExchange() {
        string bankName = transform.parent.name;
        int bankType = int.Parse(bankName.Remove(0, 4));
        Debug.Log("banktype:  " + bankType);
        int chosenType = PayScript.CoinsToValues[base.chosenItem];

        int result = bankType / chosenType;
        return result;

    }

    public void CancelShop()
    {
        gameObject.SetActive(false);
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {


			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUpPayGUI(int price){
		Transform val = transform.FindChild ("PayPanel").FindChild ("Values");
		foreach(Transform type in val){
			Debug.Log("type: " + type.name);
			Text t = type.FindChild ("Text").GetComponent<Text> ();
			t.text = "" + 0;
		}

		SetItemPrice (price);
	}

	public void SetItemPrice(int price){
		Text t = transform.FindChild ("PayPanel").FindChild ("PriceText").GetComponent<Text>();
		t.text = "You need to pay: " + price;
	}

	public void UpdateAmountText(string value, int i){
		Text t = transform.FindChild ("PayPanel").FindChild ("Values").
			FindChild (value).FindChild ("Text").GetComponent<Text>();
		int amount = int.Parse (t.text);
		amount = amount + i;
		t.text = "" + amount;
	}

}

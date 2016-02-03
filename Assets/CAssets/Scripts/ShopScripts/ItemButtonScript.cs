using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pushed(){
		Text t = gameObject.GetComponentInChildren<Text>();
		string s = t.text;
		//Debug.Log("parent object: " + transform.parent.parent.parent.parent.name + " button name: " + s);
		transform.parent.parent.parent.parent.GetComponent<ShopController> ().clickButton (s);
		//transform.FindChild("Panel").FindChild("ButtonPanel").FindChild("ItemButtons");
	}
}

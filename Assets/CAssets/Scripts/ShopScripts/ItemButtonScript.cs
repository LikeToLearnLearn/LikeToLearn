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
		//Call selectItem(s) in ItemDisplayAbstract (or actually one of its instantiated subclasses)
		transform.parent.parent.parent.parent.SendMessage("selectItem", s);

	}
}

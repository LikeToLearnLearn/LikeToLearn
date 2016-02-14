using UnityEngine;
using System.Collections;

public class ShopTrigger : MonoBehaviour {

	bool toggleShopGUI;
	public Canvas shopGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	void OnTriggerEnter(Collider c)
	{
		if (c.tag.Equals("Player"))
		{
			shopGUI.gameObject.SetActive (true);
		}

	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag.Equals("Player"))
		{
			shopGUI.gameObject.SetActive (false);
		}

	}
		



}

using UnityEngine;
using System.Collections;

public class ShopTrigger : MonoBehaviour {

	bool toggleShopGUI;
	public Rect windowRect = new Rect(20, 20, 120, 50);
	public Canvas shopGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	void OnTriggerEnter(Collider c)
	{
		//Debug.Log("shop: collision det");
		if (c.tag.Equals("Player"))
		{

			//Debug.Log("shop: collision det with player");
			shopGUI.gameObject.SetActive (true);

		}

	}

	void OnTriggerExit(Collider c)
	{
		//Debug.Log("shop: collision det");
		if (c.tag.Equals("Player"))
		{

			//Debug.Log("shop: collision det with player");
			shopGUI.gameObject.SetActive (false);
		}

	}
		



}

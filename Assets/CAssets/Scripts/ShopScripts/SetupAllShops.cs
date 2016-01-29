using UnityEngine;
using System.Collections;

public class SetupAllShops : MonoBehaviour {

	public string[] shopItems;
	public Transform newButton;
	public Transform[] cityShops;


	// Use this for initialization
	//Sets up all shops in the city. Add more shops to the game object
	//to set them up as well
	void Start () {

		foreach (Transform shop in cityShops) {
			Transform shopGUI = shop.FindChild("ShopGUI");
			shopGUI.GetComponent<ShopController> ().setUpShop();
			shopGUI.gameObject.SetActive (false);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}

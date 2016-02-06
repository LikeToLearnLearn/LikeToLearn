using UnityEngine;
using System.Collections;


public class GUIMenuControl : MonoBehaviour {

	public Transform menuPanel;
	public Transform buildHandler;
	public Transform closeMenuButton;
	public Transform openMenuButton;
	public Transform inventoryPanel;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void openMenu()
	{
		menuPanel.gameObject.SetActive (true);
		openMenuButton.gameObject.SetActive (false);
		closeMenuButton.gameObject.SetActive (true);
	}

	public void closeMenu()
	{
		menuPanel.gameObject.SetActive (false);
		openMenuButton.gameObject.SetActive (true);
		closeMenuButton.gameObject.SetActive (false);
	}

	public void openInventory(){
		closeMenu ();
		inventoryPanel.gameObject.SetActive (true);
		openMenuButton.gameObject.SetActive (false);
		closeMenuButton.gameObject.SetActive (false);
		buildHandler.gameObject.SetActive (false);
	}

	public void closeInventory(){
		inventoryPanel.gameObject.SetActive (false);
		openMenuButton.gameObject.SetActive (true);
		buildHandler.gameObject.SetActive (true);
	}

}

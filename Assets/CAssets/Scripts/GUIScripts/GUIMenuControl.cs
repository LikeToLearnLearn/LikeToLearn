using UnityEngine;
using System.Collections;


public class GUIMenuControl : MonoBehaviour {

	public Transform menuPanel;
	public Transform closeMenu;
	public Transform openMenu;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OpenMenu()
	{
		menuPanel.gameObject.SetActive (true);
		openMenu.gameObject.SetActive (false);
		closeMenu.gameObject.SetActive (true);
	}

	public void CloseMenu()
	{
		menuPanel.gameObject.SetActive (false);
		openMenu.gameObject.SetActive (true);
		closeMenu.gameObject.SetActive (false);
	}
}

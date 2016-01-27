using UnityEngine;
using System.Collections;

public class HUDHandler : MonoBehaviour {

    public GameObject inventoryCanvas, HUDCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenInvnetory()
    {
        inventoryCanvas.SetActive(true);
        HUDCanvas.SetActive(false);
    }

    public void CloseInventor()
    {
        inventoryCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
    }
}

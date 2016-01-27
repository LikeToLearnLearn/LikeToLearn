using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    GameObject sco, iho, sho;
    SceneHandler sc;

	// Use this for initialization
	void Start () {
        sco = GameObject.Find("SceneHandlerO");
        sc = sco.GetComponent<SceneHandler>();
        iho = GameObject.Find("InventoryHandlerO");
        
        sho = GameObject.Find("SaveHandlerO");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGameButtonEvent() {
        sc.changeScene("new","city_centralisland");
	}

    public void LoadGameButtonEvent()
    {
        //sc.changeScene("new", "city_centralisland");
        //SceneManager.LoadScene("city_centralisland");
    }

}

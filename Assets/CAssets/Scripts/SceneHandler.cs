using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    //TODO implement changing scenes, changeScene(from, to)

    GameObject gamec;
    GameObject player;

	// Use this for initialization
	void Start () {
        Debug.Log("START");
        gamec = GameObject.Find("GameController");
        DontDestroyOnLoad(gamec);

        player = GameObject.Find("PlayerController");
        Debug.Log(player);
        player.SetActive(false);
        DontDestroyOnLoad(player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeScene(string from, string to)
    {
        player.SetActive(true);
        Debug.Log("NEW TO CITY");
        Debug.Log(from +"::"+ to);
        if (from.Equals("new") && to.Equals("city_centralisland"))
        {
            //GameObject.Instantiate("FPSController",new Vector3(0f,0f,0f),Quaternion.identity);

            SceneManager.LoadScene("city_centralisland");
        }

    }
}

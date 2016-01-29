using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    //TODO implement changing scenes, changeScene(from, to)

    GameObject gamec;
    GameObject player;

	// Use this for initialization
	void Start () {
        gamec = GameObject.Find("GameController");
        DontDestroyOnLoad(gamec);

        //Disable player.
        player = GameObject.Find("PlayerController");
        player.SetActive(false);
        DontDestroyOnLoad(player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeScene(string from, string to)
    {
        player.SetActive(true);
        if (from.Equals("new"))
        {
            //Do stuff on starting a new game.
            //GameObject.Instantiate("FPSController",new Vector3(0f,0f,0f),Quaternion.identity);


        }

        //Scene-specific should go here...
        if (to.Equals("city_centralisland"))
        {
            //....
            SceneManager.LoadScene("city_centralisland");
        }
        else if (to.Equals("something"))
        {
            //....

        }
        else
        {
            //Put this in for now for all scenes. Should probably not be here later.
            SceneManager.LoadScene(to);
        }
    }

        public void OnLevelWasLoaded()
        {
            player = GameObject.Find("PlayerController");
            GameObject spawnpoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            player.transform.position = spawnpoint.transform.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<Rigidbody>().Sleep();
        }

}

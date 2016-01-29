using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
       // Debug.Log("collision det");
        if (c.tag.Equals("Player"))
        {
            car.SetActive(true);
            player.SetActive(false);
            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }
}

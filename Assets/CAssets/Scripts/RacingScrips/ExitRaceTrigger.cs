using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitRaceTrigger : MonoBehaviour {

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
        car = GameObject.Find("Car");
        Debug.Log("collision det with" + c.name);
        if (c.tag.Equals("PlayerCar"))
        //if (c.Equals(GameObject.Find("Car")))
        {
            car.SetActive(false);
            player.SetActive(true);
            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }
}

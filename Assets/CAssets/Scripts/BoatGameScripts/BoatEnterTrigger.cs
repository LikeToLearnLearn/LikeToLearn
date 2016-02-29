using UnityEngine;
using System.Collections;

public class BoatEnterTrigger : MonoBehaviour {

    public GameObject boatCamera;
    public GameObject boat;
    public GameObject exit;
    private BoatExitTrigger exitTrigger;

    // Use this for initialization
    void Start () {
        exitTrigger = exit.GetComponent<BoatExitTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            EnterBoatGame();
        }
    }

    private void EnterBoatGame()
    {
        GameObject.Find("BoatGame").GetComponent<BoatGame>().StartGame(); // start game in boatgame script
    }
             
}

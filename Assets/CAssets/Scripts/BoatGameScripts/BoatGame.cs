using UnityEngine;
using System.Collections;
using System;

public class BoatGame : MiniGameAbstract
{
    //public Transform entranceTrigger; // entrance of game
    //public Transform exitTrigger; // exit of game
    public QuestionSystem questionSystem; // question point controller gameobject
    public GameObject boatCam; // camera used to follow boat
    public GameObject minigameHUDCanvas; // minigame canvas
    public GameObject boat; // the boat to control

    private Vector3 boatStartPosition;
    private Quaternion boatStartRotation;


    // Use this for initialization
    public override void Start()
    {
        boatStartPosition = boat.transform.position;
        boatStartRotation = boat.transform.rotation;
            
        
    }

    public override void StartGame()
    {
        minigameHUDCanvas.GetComponent<MinigameHUDController>().GameStart();

        // Disable player components and camera
        GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject boatExitTrigger = GameObject.FindGameObjectWithTag("BoatExitTrigger");
        Component[] components = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in components)
        {
            comp.enabled = false;
        }
        Camera pcam = player.GetComponentInChildren<Camera>();
        pcam.enabled = false;

        // Activate boat and camera
        GameObject boat = GameObject.FindGameObjectWithTag("PlayerBoat");
        boat.GetComponent<BoatController>().StartBoat();
        Rigidbody boatrb = boat.GetComponent<Rigidbody>();
        boatrb.constraints = RigidbodyConstraints.FreezePositionY; //| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        boatCam.SetActive(true);
        //exitTrigger.StartTimer(); // timer for minimum time before exiting (prevents boat stuck in exit trigger)
		boatExitTrigger.SetActive(true);
        base.StartGame();
    }

    public override void StopGame()
    {
        StartCoroutine("ShowGameEnd", true);

        //startTime = Time.timeSinceLevelLoad;
        
        //GameController.control.AddBalance(GetCurrentScore()); // Default reward
        //GameController.control.AddExp(GetCurrentScore() / 2);

        base.StopGame();
    }
    public void StopGameNoDelay()
    {
        StartCoroutine("ShowGameEnd", false);
        base.StopGame();
    }


    private IEnumerator ShowGameEnd(bool delay)
    {
        minigameHUDCanvas.GetComponent<MinigameHUDController>().SetEndText("You got a score of " + GetCurrentScore());
        minigameHUDCanvas.GetComponent<MinigameHUDController>().GameOver();

        if (delay) yield return new WaitForSeconds(5);

        // Disable boat, freeze
        GameObject boat = GameObject.Find("Speedboat");
        boat.GetComponent<BoatController>().StopBoat();
        Rigidbody boatrb = boat.GetComponent<Rigidbody>();
        boatrb.constraints = RigidbodyConstraints.FreezeAll;

        // Enable player's components again (all of them)
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Component[] components = p.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in components)
        {
            comp.enabled = true;
        }

        // Move player to spawnpoint
        p.GetComponentInChildren<Camera>().enabled = true;
        p.GetComponent<Rigidbody>().velocity = Vector3.zero;
        p.GetComponent<Rigidbody>().Sleep();
        p.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;

        // Move boat to original position
        boat.transform.position = boatStartPosition;
        boat.transform.rotation = boatStartRotation;


    }

}
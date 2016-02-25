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
            GameObject.Find("BoatGame").GetComponent<BoatGame>().StartGame(); // start game in boatgame script

            // Disable player components and camera
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Component[] components = player.GetComponents<MonoBehaviour>();
            foreach ( MonoBehaviour comp in components)
            {
                comp.enabled = false;
            }
            Camera pcam = player.GetComponentInChildren<Camera>();
            pcam.enabled = false;

            // Activate boat and camera
            GameObject boat = GameObject.Find("Speedboat");
            boat.GetComponent<BoatController>().StartBoat();
            Rigidbody boatrb = boat.GetComponent<Rigidbody>();
            boatrb.constraints = RigidbodyConstraints.FreezePositionY; //| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            boatCamera.SetActive(true);
            exitTrigger.StartTimer(); // timer for minimum time before exiting (prevents boat stuck in exit trigger)


        }
    }
             
}

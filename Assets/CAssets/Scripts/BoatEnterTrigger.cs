using UnityEngine;
using System.Collections;

public class BoatEnterTrigger : MonoBehaviour {

    public GameObject boatCamera;
    public GameObject boat;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Component[] components = player.GetComponents<MonoBehaviour>();
            foreach ( MonoBehaviour comp in components)
            {
                comp.enabled = false;
            }
            Camera pcam = player.GetComponentInChildren<Camera>();
            pcam.enabled = false;

            GameObject boat = GameObject.Find("Speedboat");
            //boat.SetActive(true);
            boat.GetComponent<BoatController>().StartBoat();
            Rigidbody boatrb = boat.GetComponent<Rigidbody>();
            boatrb.constraints = RigidbodyConstraints.FreezePositionY | 
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; 

            //boatCamera = GameObject.Find("BoatDrivingCamera");
            boatCamera.SetActive(true);


        }
    }
             
}

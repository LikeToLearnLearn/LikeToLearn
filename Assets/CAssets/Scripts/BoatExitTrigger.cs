using UnityEngine;
using System.Collections;

public class BoatExitTrigger : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        print(c.tag);
        if (c.tag.Equals("PlayerBoat"))
        {
            //GameObject.FindGameObjectWithTag("PlayerBoat").GetComponent<BoatEnterTrigger>().enabled = false;
            GameObject boat = GameObject.Find("Speedboat");
            boat.GetComponent<BoatController>().enabled = false;
            Rigidbody boatrb = boat.GetComponent<Rigidbody>();
            boatrb.constraints = RigidbodyConstraints.FreezeAll;

            GameObject p = GameObject.FindGameObjectWithTag("Player");
            Component[] components = p.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour comp in components)
            {
                comp.enabled = true;
            }
            p.GetComponentInChildren<Camera>().enabled = true;
            p.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            //p.SetActive(true);
            //p.transform.position = new Vector3(289.1f, 92.29f, 345.53f);
            //p.GetComponent<Rigidbody>.c
        }
    }
}

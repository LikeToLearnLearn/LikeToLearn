using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddRelativeTorque(Vector3.up * 1000);
    }


    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * 1, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

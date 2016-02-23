using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

    Rigidbody rb;
    Transform tf;
    float startY;
    float newY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        //rb.AddRelativeTorque(Vector3.up * 1000);
        startY = rb.position.y;
    }


    void FixedUpdate()
    {
        if (rb.position.x > 5)
        {
            //startY = rb.rotation.y;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x, startY-180, rb.rotation.z) * Time.deltaTime);
            Debug.Log("fish rotation y: " + tf.rotation.y);
            //newY = Mathf.Lerp(startY, startY - 180, 5);
            if (tf.rotation.y < 278)//Mathf.FloorToInt(startY + 180)
                rb.MoveRotation(rb.rotation * deltaRotation);
            if(Mathf.FloorToInt(startY) == Mathf.FloorToInt(startY + 180))
                rb.AddRelativeForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        }
            
        
        else if(transform.position.x < -5.0)
            rb.AddRelativeForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        else
            rb.AddRelativeForce(Vector3.forward * 1, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

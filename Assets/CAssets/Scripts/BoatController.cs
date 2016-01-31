using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class BoatController : MonoBehaviour {

	public float turnSpeed = 3f;
	public float forwardSpeed = 5f;
    public float maxTurnTorque = 0.5f;

	private Rigidbody rb;

    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody>();
        turnSpeed = turnSpeed * 1000f;
        forwardSpeed = forwardSpeed * 1000f;

    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        if (v > 0)
        {
            rb.AddForce( transform.forward * v * forwardSpeed * Time.deltaTime, ForceMode.Force);
        }
        else if (v < 0) 
        {
            rb.AddForce(transform.forward * v * 0.6f * forwardSpeed * Time.deltaTime); // Moving in reverse is slower
        }
        
        //Limit torque. Conditions attempt to simulate reality a little
        if (!(h == 0f || rb.angularVelocity.y > maxTurnTorque || rb.velocity.sqrMagnitude < 3.5f))
        {
            //Add "horizontal" torque
            rb.AddRelativeTorque(0f, h * turnSpeed * Time.deltaTime, 0f);
        }

        /* An attempt at keeping boat upright
        Quaternion localrot = rb.transform.localRotation;
        Vector3 localup = new Vector3(0f, localrot.y, 0f);
        Quaternion vert = Quaternion.FromToRotation(localup,Vector3.up);

        float angleYDiff = Quaternion.Angle(rb.rotation, vert);
        if (angleYDiff > 30f)
        {
            print("yup");
            rb.MoveRotation(vert * Time.deltaTime);
        }
        */

        
        //Keep rotation close to 0. TODO 
        Quaternion q = new Quaternion(0f, 0f, 0f, 0f);
        this.gameObject.transform.rotation = Quaternion.LerpUnclamped(rb.rotation, q, Time.deltaTime * 10f);

        Quaternion nq = this.gameObject.transform.rotation;
        if (nq.x > 10 || nq.z > 10)
        {
            //Never comes in here atm
            print("x:" + rb.transform.rotation.x + "  z:" + rb.transform.rotation.z);
            rb.transform.rotation = new Quaternion(1f, rb.transform.rotation.y, 1f, 0f);
        }
        
    }
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class BoatController : MonoBehaviour {

	public float turnSpeed = 3f;
	public float forwardSpeed = 5f;
    public float maxTurnTorque = 0.5f;

	private Rigidbody rb;
    private ParticleSystem ps;
    private bool boatOn;


    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();

        turnSpeed = turnSpeed * 1000f;
        forwardSpeed = forwardSpeed * 1000f;
        boatOn = false;

    }

    public void StartBoat()
    {
        boatOn = true;
        //this.GetComponentInChildren<ParticleSystem>().Play();
        ps.startSpeed = 1;
    }
    public void StopBoat()
    {
        boatOn = false;
        //this.GetComponentInChildren<ParticleSystem>().Stop();
        ps.startSpeed = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        if (boatOn)
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            ps.startSpeed = h*10;
            if (v > 0)
            {
                rb.AddForce(transform.forward * v * forwardSpeed * Time.fixedDeltaTime / rb.mass, ForceMode.Acceleration);
            }
            else if (v < 0)
            {
                rb.AddForce(transform.forward * v * 0.6f * forwardSpeed * Time.fixedDeltaTime); // Moving in reverse is slower
            }

            //Limit torque. Conditions attempt to simulate reality a little
            if (!(h == 0f || rb.angularVelocity.y > maxTurnTorque))
            {
                //Add "horizontal" torque   
                rb.AddTorque(0f, h * turnSpeed * Time.fixedDeltaTime, 0f);
            }


            //Keep boat upright
            Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
            float buoyantTorque = 50f;
            rb.AddTorque(q.x * buoyantTorque * 1, q.y * buoyantTorque * 10, q.z * buoyantTorque * 10);
           // Quaternion q = new Quaternion(0f, 0f, 0f, 0f);
           // this.gameObject.transform.rotation = Quaternion.LerpUnclamped(rb.rotation, q, Time.fixedDeltaTime * 1000f);

            //Quaternion nq = this.gameObject.transform.rotation;
            //if (nq.x > 10 || nq.z > 10)
            //{
                //Never comes in here atm
            //    print("x:" + rb.transform.rotation.x + "  z:" + rb.transform.rotation.z);
            //    rb.transform.rotation = new Quaternion(1f, rb.transform.rotation.y, 1f, 0f);
            //}

        }
    }
}

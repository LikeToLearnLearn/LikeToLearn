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
            else if (v == 0)
            {
                rb.velocity = new Vector3(rb.velocity.x * 0.94f, rb.velocity.y * 0.94f, rb.velocity.z *0.94f);
                    

            }
            else if (v < 0)
            {
                rb.AddForce(transform.forward * v * 0.6f * forwardSpeed * Time.fixedDeltaTime); // Moving in reverse is slower
            }

            //Limit torque. Conditions attempt to simulate reality a little
            if (!(h == 0f || Mathf.Abs(rb.angularVelocity.y) > maxTurnTorque))
            {
                //Add "horizontal" torque   
                rb.AddTorque(0f, h * turnSpeed * Time.fixedDeltaTime, 0f);
            }

            //Keep boat upright
            Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
            float buoyantTorque = 120f;
            rb.AddTorque(q.x * buoyantTorque * 10, q.y * buoyantTorque * 10, q.z * buoyantTorque * 10);

        }
    }
}

using UnityEngine;
using System.Collections;

// (R)andom (M)ovement experiments
public class RMScript : MonoBehaviour
{

    private Rigidbody rb;
    private float currentSpeed;
    private float maxVelocity;
    
    void Start()
    {
        currentSpeed = 0f;
        maxVelocity = 6f;
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {

        rb.AddExplosionForce(255,transform.position,25525);
        /*
        if(rb.velocity.x < maxVelocity && rb.velocity.y < maxVelocity && rb.velocity.z < maxVelocity)
        {
            //print(rb.velocity);
            currentSpeed = Mathf.Lerp(currentSpeed, maxVelocity, 0.2f);
            rb.AddRelativeForce(Vector3.forward * currentSpeed);
        }
        
        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
        float buoyantTorque = 100f;
        rb.AddTorque(q.x * buoyantTorque, q.y * buoyantTorque, q.z * buoyantTorque);
        
        if (Random.Range(0, 100) == 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + Random.Range(-4, 5), rb.rotation.y + Random.Range(-5, 5), rb.rotation.z + Random.Range(-4, 5)));
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

         
        if (Physics.Raycast(transform.position, Vector3.forward, 5))
        {
            //print("something in front");
            //RotateToAvoid();
        }
        */
    }
    /*
    void OnCollisionStay()
    {
        if (Random.Range(0, 10000) == 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + 180f, rb.rotation.y + 180f, rb.rotation.z + 180f));
            rb.MoveRotation(rb.rotation * deltaRotation);
            currentSpeed = maxVelocity;
        }
        else
        {
            //print(this.gameObject + " collided with " + collisionInfo.collider.gameObject.name);
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x - 30f, rb.rotation.y + 120f * Random.Range(-1,2), rb.rotation.z + 90f * Random.Range(-1, 2)) * 3 * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }


    }


    private void RotateToAvoid()
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + 15f, rb.rotation.y + Random.Range(120, 180) * Random.Range(0, 2), rb.rotation.z + Random.Range(120, 180) * Random.Range(0, 2)) * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    */
}

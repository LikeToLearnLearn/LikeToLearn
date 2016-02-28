using UnityEngine;
using System.Collections;

// Expects object to be contained in a shape of colliders to limit movement
public class RandomMovementScript : MonoBehaviour {

    private Rigidbody rb;
    private float currentSpeed;
    private float maxSpeed;


    void Start()
    {
        currentSpeed = 0f;
        maxSpeed = 400f;
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {

        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 0.2f);
        rb.AddRelativeForce(Vector3.forward * currentSpeed);

        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
        float buoyantTorque = 10f;
        rb.AddTorque(q.x * 1 * buoyantTorque, 0, q.y * buoyantTorque);
        
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + Random.Range(-4,5), rb.rotation.y + Random.Range(-5,5), rb.rotation.z + Random.Range(-4, 5) * Time.fixedDeltaTime));
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        if (Physics.Raycast(transform.position, Vector3.forward, 5))
        {
            //print("something in front");
            RotateToAvoid();
        }
    }

    void OnCollisionEnter()
    {

        if (Random.Range(0, 100) == 0)
        {
            currentSpeed = Random.Range(20, maxSpeed);
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }

    void OnCollisionStay()
    {
        if (Random.Range(0, 1000) == 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + 180f, rb.rotation.y + 180f, rb.rotation.z + 180f) * 5 * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            currentSpeed = maxSpeed;
        }
        else
        {
            //print(this.gameObject + " collided with " + collisionInfo.collider.gameObject.name);
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x - 30f, rb.rotation.y + 120f, rb.rotation.z + 90f) * 3 * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        
    }

    private void RotateToAvoid()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + 15f, rb.rotation.y + Random.Range(120, 180) * Random.Range(-1, 2), rb.rotation.z + Random.Range(120, 180) * Random.Range(-1, 2)) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

}

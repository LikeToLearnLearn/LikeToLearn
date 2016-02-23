using UnityEngine;
using System.Collections;

// Moves fish 
// Expects fish to be contained in a shape of colliders to limit movement (FishContainer prefab can be used for this)
public class FishController : MonoBehaviour {

    private Rigidbody rb;
    private float currentSpeed;
    private float maxSpeed;


    void Start()
    {
        currentSpeed = 0f;
        maxSpeed = 500f;
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {

        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 0.1f);
        rb.AddRelativeForce(Vector3.forward * currentSpeed);

        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
        float buoyantTorque = 100f;
        rb.AddTorque(q.x * 1 * buoyantTorque, 0, q.y * buoyantTorque);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + Random.Range(-1, 1), rb.rotation.y + Random.Range(-1, 1), rb.rotation.z + Random.Range(-1, 1) * Time.fixedDeltaTime));
        rb.MoveRotation(rb.rotation * deltaRotation);

    }

    void OnCollisionEnter()
    {
        currentSpeed = maxSpeed;
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        //print(collisionInfo.collider.gameObject.name);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(rb.rotation.x + Random.Range(-15, 15), rb.rotation.y + 150f, rb.rotation.z + Random.Range(-15, 15)) * 5 *Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}

using UnityEngine;
using System.Collections;

public class WaterBuoyancy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay (Collision collisionInfo)
    {

        // Very expensive...
        Vector3 uplift = new Vector3(0, 1, 0);
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            print("touching:" + contact.otherCollider.name);
            contact.otherCollider.GetComponentInParent<Rigidbody>().AddForce(uplift);
        }
        
    }

}

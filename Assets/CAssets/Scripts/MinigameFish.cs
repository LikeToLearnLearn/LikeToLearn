using UnityEngine;
using System.Collections;

public class MinigameFish : MonoBehaviour {
    public bool play = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            //spela minispel: fånga fisk
            play = true;
        }

    }
}

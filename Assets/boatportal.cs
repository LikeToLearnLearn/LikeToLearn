using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class boatportal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("collision det");
        if (c.tag.Equals("Player"))
        {

            Debug.Log("collision det with player");
            SceneManager.LoadScene("foundation");
        }

    }
}

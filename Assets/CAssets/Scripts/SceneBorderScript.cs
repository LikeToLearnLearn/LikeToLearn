using UnityEngine;
using System.Collections;

public class SceneBorderScript : MonoBehaviour {

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag.Equals("Player"))
        {
            c.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
}

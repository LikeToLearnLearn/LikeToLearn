using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransitionAreaTrigger : MonoBehaviour {

    GameObject sco;
    SceneHandler sc;

    string leadsTo;

	// Use this for initialization
	void Start ()
    {
        sco = GameObject.Find("SceneHandlerO");
        sc = sco.GetComponent<SceneHandler>();

        //The name of the object this script is attached to is expected to have the to-scene (leadsTo) seperated by space.
        //Like so: "[anything] [mapname]". E.g: "SceneTransitionArea build_constructionisland".
        string[] splits = this.gameObject.name.Split(' ');
        leadsTo = splits[1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("collision det");
        if (c.tag.Equals("Player"))
        {
            //Debug.Log("collision det with player");

            //Call SceneHandler to change scene, pass on current scene and target scene.
            sc.changeScene(SceneManager.GetActiveScene().name, leadsTo);
        }

    }

}

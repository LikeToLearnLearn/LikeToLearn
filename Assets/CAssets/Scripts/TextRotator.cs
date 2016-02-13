using UnityEngine;
using System.Collections;

public class TextRotator : MonoBehaviour {

    private GameObject playerBoat;
	// Use this for initialization
	void Start () {
        playerBoat = GameObject.FindGameObjectWithTag("PlayerBoat");
        InvokeRepeating("LookAtCamera", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        /*if(!this.name.Equals("QuestionText"))
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        if (playerBoat.GetComponent<BoatController>().enabled == true)
        {
            if (Vector3.Distance(playerBoat.transform.position, this.gameObject.transform.position) > 110)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }*/
    }

    private void LookAtCamera()
    {
        this.gameObject.transform.rotation = playerBoat.transform.rotation;
    }

}

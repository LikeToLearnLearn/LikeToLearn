using UnityEngine;
using System.Collections;

public class TextRotatorFish : MonoBehaviour
{

    private GameObject playerBoat;
    // Use this for initialization
    void Start()
    {
        playerBoat = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("LookAtCamera", 0, 1);
    }

    void Update()
    {
        this.gameObject.transform.rotation = playerBoat.transform.rotation;
    }

    private void LookAtCamera()
    {
        this.gameObject.transform.rotation = playerBoat.transform.rotation;
    }

}


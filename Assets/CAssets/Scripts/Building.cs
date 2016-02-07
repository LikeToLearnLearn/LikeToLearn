using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Building : MonoBehaviour {

    public BuildGUIScript buildgui;
    public Object brick;
    public GameObject transparentbrick;

    // Use this for initialization
    void Start () {

        buildgui = GameObject.Find("BuildGUI").GetComponent<BuildGUIScript>();
        //TODO Get item counts or current item properly!

        Instantiate(transparentbrick, new Vector3(), Quaternion.identity); //TODO On chosen item, make a transparent version of it to show!
    }

    // Update is called once per frame
    void Update()
    {

        // Move transparent version of current chosen item to where camera is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transparentbrick.transform.position = hit.point;
        }

        // On Fire1 (ctrl/lmb/touch) place the real item where camera is pointing
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2))
            {
                //if (hit.collider.name.Equals("Terrain"))
                //{   }
                Instantiate(brick, hit2.point, Quaternion.identity);


            }

        }

        if (CrossPlatformInputManager.GetButton("Fire2"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                //if (hit.collider.name.Equals("Terrain"))
                //{   }
                Instantiate(brick, hit.point, Quaternion.identity);


            }
        }
    }
}

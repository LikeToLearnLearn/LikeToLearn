using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Building : MonoBehaviour {

    public BuildGUIScript buildgui;
    public GameObject currentItem;
    public GameObject currentItemMarker;
    public GameObject player;
    private Camera camera;

    private Ray ray;
    private RaycastHit hit;

    private bool snapMode = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
        buildgui = GameObject.Find("BuildGUI").GetComponent<BuildGUIScript>();

        //TODO Get item counts and current item properly!
        currentItem = GameObject.Find("Brick");
        currentItemMarker = GameObject.Find("TransparentBrick");

        //Instantiate(currentItem, new Vector3(), Quaternion.identity); //TODO On chosen item, make a transparent version of it to show!
    }

    public void SetCurrentItem()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(currentItem != null)
        {
            // Move transparent version of current chosen item to where camera is pointing
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            float dist = Vector3.Distance(hit.point, player.transform.position);
            if (dist < 1)
            {
                return;
            }
            else if (dist < 5)
            {
                currentItemMarker.transform.position = hit.point;
            }   
            else
            {
                currentItemMarker.transform.position = ray.GetPoint(5);
            }

            currentItemMarker.transform.rotation = Quaternion.identity; 
                                                //player.transform.rotation; to make item turn with player
                                                //Quaternion.identity; to make item never turn, all placed items align

            // On Fire1 (ctrl/lmb/touch) place the real item where camera is pointing
            if (CrossPlatformInputManager.GetButtonDown("Fire1") && !snapMode)
            {
                //RaycastHit hit2;
                //if (Physics.Raycast(ray, out hit2))
                //{

                Instantiate(currentItem, currentItemMarker.transform.position, currentItemMarker.transform.rotation);


                //}

            }
            else if(CrossPlatformInputManager.GetButtonDown("Fire1") && snapMode)
            {
                //TODO some sort of hold and drag mode that will automatically align several items within origin and another point...
                // will need some sort of ITEM SIZE, and count multiples of this size that fit in the box...
            }


            if (CrossPlatformInputManager.GetButton("Fire2"))
            {
                //if (Physics.Raycast(ray, out hit))
                //{
                    Instantiate(currentItem, currentItemMarker.transform.position, currentItemMarker.transform.rotation);
                //}
            }

        }



    }
}

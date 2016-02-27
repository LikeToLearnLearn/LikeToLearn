using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Building : MonoBehaviour {

    public BuildGUIScript buildgui;
    public Material transparentMat;

    public GameObject currentItem;
    public GameObject currentItemMarker;
    private float maxMarkerDistance;
    private Bounds currentItemBounds;

	private string currentItemString;

    public GameObject player;
    private GameObject cam;
    private Vector3 currentMousePosition;

    private bool markerSnapped = false;

    private Ray ray;
    private RaycastHit hit;

    private GameObject latestPlaced;
    private bool snapMode = false;
    private Vector3 snapStartPosition;
    private Vector3 snapCurrentPosition;
    private GameObject[] snapItemMarkers;

    public bool add99999ofeveryitem = true;

    // Use this for initialization
    void Start() {
        maxMarkerDistance = 4;

        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        buildgui = GameObject.Find("BuildGUI").GetComponent<BuildGUIScript>();

        //ITEMS CHEAT FOR TESTING!
        if (add99999ofeveryitem)
        {
            string[] names = System.Enum.GetNames(typeof(GameController.Item));
            GameController.Item[] items = (GameController.Item[])System.Enum.GetValues(typeof(GameController.Item));
            foreach (GameController.Item i in items)
            {
                GameController.control.AddItems(i, 99999);
            }
        }
        //ITEMS CHEAT!
    }

    // Update is called once per frame
    void Update()
    {
		if (currentItemString != buildgui.chosenItem && buildgui.chosenItem != "")
		{
			currentItemString = buildgui.chosenItem;
			currentItem = (GameObject) Instantiate(Resources.Load("Build" + currentItemString));
			currentItemBounds = currentItem.GetComponent<Renderer>().bounds;

            if (currentItemMarker != null) Destroy(currentItemMarker);
			currentItemMarker = (GameObject) Instantiate(currentItem, new Vector3(), Quaternion.identity);
			currentItemMarker.name = "Transparent" + currentItemString;
            currentItemMarker.GetComponent<Renderer>().material = transparentMat;
            currentItemMarker.GetComponent<Collider>().enabled = false;
		}

        if(currentItem != null)
        {
            if (currentMousePosition != Input.mousePosition)
            {
                markerSnapped = false;
                MoveCurrentItemMarker();
            }

                // On Fire1 (ctrl/lmb) place an item where camera is pointing, initiate snapmode
                if (CrossPlatformInputManager.GetButtonDown("Fire1") && !snapMode)
                {
    	            print("fire1 no snap");
					if (GameController.control.RemoveItem(currentItemString) > 0) {
                    	Instantiate(currentItem, currentItemMarker.transform.position, currentItemMarker.transform.rotation);
                        snapStartPosition = currentItemMarker.transform.position;
                        snapMode = true;
                    }

                }
                // While mouse is held down, move the marker around
                //else if (CrossPlatformInputManager.GetButton("Fire1") && snapMode)
                //{
                    // Some sort of hold and drag mode that will automatically align several items within origin and another point...
                    // will need some sort of ITEM SIZE, and count multiples of this size that fit in the box...
                     

                    //print("holding");
                //}
                // On second click, calculate and place blocks
                else if (CrossPlatformInputManager.GetButtonDown("Fire1") && snapMode)
                {
                    snapCurrentPosition = currentItemMarker.transform.position;

                    StartCoroutine(PlaceItemsAsHollowBox());

                    // Place X-length blocks
                    /*if (distanceX < 0)
                    {
                        for (int i = 0; i < blocksX; i++)
                        {
                            Instantiate(currentItem, snapStartPosition - new Vector3(i * currentItemBounds.size.x, 0, 0),
                                currentItemMarker.transform.rotation);
                        }
                    }
                    else if (distanceX >= 0)
                    {
                        for (int i = 0; i < blocksX; i++)
                        {
                            Instantiate(currentItem, snapStartPosition + new Vector3(i * currentItemBounds.size.x, 0, 0),
                                currentItemMarker.transform.rotation);
                        }
                    }*/

                    // Place Y-length blocks
                    /*if (distanceY < 0)
                    {
                        for (int i = 0; i < blocksY; i++)
                        {
                            Instantiate(currentItem, snapStartPosition - new Vector3(0, i * currentItemBounds.size.y, 0),
                                currentItemMarker.transform.rotation);
                        }
                    }
                    else if (distanceY >= 0)
                    {
                        for (int i = 0; i < blocksY; i++)
                        {
                            Instantiate(currentItem, snapStartPosition + new Vector3(0, i * currentItemBounds.size.y, 0),
                                currentItemMarker.transform.rotation);
                        }
                    }

                    // Place Z-length blocks
                    if (distanceZ < 0)
                    {
                        for (int i = 0; i < blocksZ; i++)
                        {
                            Instantiate(currentItem, snapStartPosition - new Vector3(0, 0, i * currentItemBounds.size.z),
                                currentItemMarker.transform.rotation);
                        }
                    }
                    else if (distanceZ >= 0)
                    {
                        for (int i = 0; i < blocksZ; i++)
                        {
                            Instantiate(currentItem, snapStartPosition + new Vector3(0, 0, i * currentItemBounds.size.z),
                                currentItemMarker.transform.rotation);
                        }

                    }*/

                    snapMode = false;
                    print("snap off");
                }


                // Mobile delete block on touch. UNTESTED with mousePosition
                if(Input.touches.Length != 0)
                {
                    Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit touchHit;
                    if (Physics.Raycast(touchRay, out touchHit))
                    {
                        if (hit.collider != null)
                        {
                            if (hit.collider.name.Contains("Clone"))
                            {
                                Destroy(hit.collider.gameObject);
                                snapMode = false;
                            }
                        }
                    }
                }
                
          
            // Second button to remove a block. This button doens't exist on mobile.
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.name.Contains("Clone"))
                        {

                            Physics.Raycast(ray, out hit);
                            Destroy(hit.collider.gameObject);
                            snapMode = false;
                        }
                    }
                }
           // }

        }

    }



    private IEnumerator PlaceItemsAsHollowBox()
    {
        //Vector3 difference = snapCurrentPosition - snapStartPosition;
        //print(difference);
        //int blocksX = Mathf.Min(100, 1 + Mathf.FloorToInt(difference.x / currentItemBounds.size.x));
        //int blocksY = Mathf.Min(100, 1 + Mathf.FloorToInt(difference.y / currentItemBounds.size.y));
        //int blocksZ = Mathf.Min(100, 1 + Mathf.FloorToInt(difference.z / currentItemBounds.size.z));
        //int dirX = difference.x >= 0 ? 1 : -1;
        //int dirY = difference.y >= 0 ? 1 : -1;
        //int dirZ = difference.z >= 0 ? 1 : -1;

        // Calculate length of box in every dimension
        float distanceX = snapCurrentPosition.x - snapStartPosition.x;
        float distanceY = snapCurrentPosition.y - snapStartPosition.y;
        float distanceZ = snapCurrentPosition.z - snapStartPosition.z;
        
        // Determine directions box is to be offset toward
        int dirX = distanceX >= 0 ? 1 : -1;
        int dirY = distanceY >= 0 ? 1 : -1;
        int dirZ = distanceZ >= 0 ? 1 : -1;
        
        // Calculate how many blocks fit in every dimension, with a max for performance reasons
        int blocksX = Mathf.Min(40, 1 + Mathf.FloorToInt(Mathf.Abs(distanceX) / currentItemBounds.size.x));
        int blocksY = Mathf.Min(40, 1 + Mathf.FloorToInt(Mathf.Abs(distanceY) / currentItemBounds.size.y));
        int blocksZ = Mathf.Min(40, 1 + Mathf.FloorToInt(Mathf.Abs(distanceZ) / currentItemBounds.size.z));
        
        // Place all blocks in a hollow box form
        for (int i = 0; i < Mathf.Abs(blocksX); i++)
        {
            for (int j = 0; j < Mathf.Abs(blocksY); j++)
            {
                for (int k = 0; k < Mathf.Abs(blocksZ); k++)
                {
                    if (i == 0 || j == 0 || k == 0 ||
                       i == blocksX - 1 || j == blocksY - 1 || k == blocksZ - 1)
                    {
                        if (GameController.control.RemoveItem(currentItemString) > 0)
                        {
                            latestPlaced = (GameObject)Instantiate(currentItem, snapStartPosition + new Vector3(
                               dirX * i * currentItemBounds.size.x,
                               dirY * j * currentItemBounds.size.y,
                               dirZ * k * currentItemBounds.size.z),
                               currentItemMarker.transform.rotation);
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0);
    }

    private void MoveCurrentItemMarker()
    {
        // Move transparent version of current chosen item to where camera is pointing
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        Physics.Raycast(ray, out hit);
        float dist = Vector3.Distance(hit.point, player.transform.position);
        if (dist < 1)
        {
            return;
        }
        else if (dist < maxMarkerDistance)
        {
            if (hit.collider != null && !hit.collider.gameObject.name.Equals("Terrain") && !markerSnapped)
            {
                currentItemMarker.transform.rotation = hit.collider.transform.rotation;
                currentItemMarker.transform.position = hit.collider.bounds.center + Vector3.Scale(hit.collider.gameObject.GetComponent<Renderer>().bounds.size, hit.normal);
                // Much smurt.
            }
        }
        else
        {
            currentItemMarker.transform.position = ray.GetPoint(maxMarkerDistance);
            currentItemMarker.transform.rotation = Quaternion.identity; //Quaternion.identity;
                                                                              //player.transform.rotation; to make item turn with player
                                                                              //Quaternion.identity; to make item never turn, all placed items align (not to grid)
        }


    }
}

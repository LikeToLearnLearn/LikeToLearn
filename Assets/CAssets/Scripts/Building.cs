using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Building : MonoBehaviour {

    public BuildGUIScript buildgui;

    public GameObject currentItem;
    public GameObject currentItemMarker;
    private Bounds currentItemBounds;

    public GameObject player;
    private GameObject camera;
    private Vector3 currentMousePosition;

    private bool markerSnapped = false;

    private Ray ray;
    private RaycastHit hit;

    private GameObject latestPlaced;
    private bool snapMode = false;
    private Vector3 snapStartPosition;
    private Vector3 snapCurrentPosition;
    private GameObject[] snapItemMarkers;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        buildgui = GameObject.Find("BuildGUI").GetComponent<BuildGUIScript>();

        //TODO Get item counts and current item properly!
        currentItem = GameObject.Find("Brick"); //alt "ShortStair"
        currentItemMarker = GameObject.Find("TransparentBrick");
        currentItemBounds = currentItem.GetComponent<Renderer>().bounds;

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
            //if (currentMousePosition != Input.mousePosition)
            //{
            MoveCurrentItemMarker();
                //}

                // On Fire1 (ctrl/lmb) place an item where camera is pointing, initiate snapmode
                if (CrossPlatformInputManager.GetButtonDown("Fire1") && !snapMode)
                {
                print("fire1 no snap");
                    Instantiate(currentItem, currentItemMarker.transform.position, currentItemMarker.transform.rotation);
                    snapStartPosition = currentItemMarker.transform.position;
                    snapMode = true;

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

                    // Calculate length of box in every dimension
                    float distanceX = snapCurrentPosition.x - snapStartPosition.x;
                    float distanceY = snapCurrentPosition.y - snapStartPosition.y;
                    float distanceZ = snapCurrentPosition.z - snapStartPosition.z;

                    // Determine directions box is to be offset toward
                    int dirX = distanceX >= 0 ? 1 : -1;
                    int dirY = distanceY >= 0 ? 1 : -1;
                    int dirZ = distanceZ >= 0 ? 1 : -1;

                    // Attempt at removing smallest of the 3 to only form a plane instead
                    /*ArrayList list = new ArrayList();
                    list.Add(distanceX);
                    list.Add(distanceY);
                    list.Add(distanceZ);
                    list.Remove(Mathf.Min(distanceX,distanceY,distanceZ));
                    list.Sort();*/

                    // Calculate how many blocks fit in every dimension, with a max for performance reasons
                    int blocksX = Mathf.Min(40, Mathf.CeilToInt(Mathf.Abs(distanceX) / currentItemBounds.size.x));
                    int blocksY = Mathf.Min(40, Mathf.CeilToInt(Mathf.Abs(distanceY) / currentItemBounds.size.y));
                    int blocksZ = Mathf.Min(40, Mathf.CeilToInt(Mathf.Abs(distanceZ) / currentItemBounds.size.z));

                    // Place all blocks in a hollow box form
                    for (int i = 0; i < blocksX; i++)
                    {
                        for (int j = 0; j < blocksY; j++)
                        {
                            for (int k = 0; k < blocksZ; k++)
                            {
                                if(i == 0 || j == 0 || k == 0 || 
                                   i == blocksX-1|| j == blocksY-1 || k == blocksZ-1)
                            {
                                 latestPlaced = (GameObject) Instantiate(currentItem, snapStartPosition + new Vector3(
                                    dirX * i * currentItemBounds.size.x,
                                    dirY * j * currentItemBounds.size.y,
                                    dirZ * k * currentItemBounds.size.z),
                                    currentItemMarker.transform.rotation);
                            }
                            }
                        }
                    }

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


                // Mobile delete block on touch. UNTESTED
                if(Input.touches.Length != 0)
                {
                    Touch t = Input.GetTouch(0);
                    Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit touchHit;
                    if (Physics.Raycast(touchRay, out touchHit))
                    {
                        if (hit.collider != null)
                        {
                            if (hit.collider.name.Contains("Clone"))
                            {
                                Destroy(hit.collider.gameObject);
                            }
                        }
                    }
                }
                
          
            // Second button to remove a block. NO SECOND BUTTON ON MOBILE
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.name.Contains("Clone"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
           // }

        }

    }



    private IEnumerator PlaceItemsAsHollowBox()
    {

        yield return new WaitForSeconds(0);
    }

    private void MoveCurrentItemMarker()
    {
        // Move transparent version of current chosen item to where camera is pointing
        print("move");
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        Physics.Raycast(ray, out hit);
        float dist = Vector3.Distance(hit.point, player.transform.position);
        if (dist < 1)
        {
            return;
        }
        else if (dist < 4)
        {
            if (hit.collider != null && !hit.collider.gameObject.name.Equals("Terrain")) // TODO fix this check to prevent exception
            {
                // WIP MAKE IT ACTUALLY SNAP NEXT TO BLOCK
                if (hit.normal.x != 0 && !markerSnapped)
                {
                    float add = hit.collider.gameObject.GetComponent<Renderer>().bounds.extents.x;
                    Vector3 a = new Vector3(add, 0, 0);
                    currentItemMarker.transform.position = hit.collider.bounds.center + a;
                    markerSnapped = true;
                }
                if (hit.normal.y != 0 && !markerSnapped)
                {
                    float add = hit.collider.gameObject.GetComponent<Renderer>().bounds.extents.y;
                    Vector3 a = new Vector3(0, add, 0);
                    currentItemMarker.transform.position = hit.collider.bounds.center + a;
                    markerSnapped = true;
                }
                if (hit.normal.z != 0 && !markerSnapped)
                {
                    float add = hit.collider.gameObject.GetComponent<Renderer>().bounds.extents.z;
                    Vector3 a = new Vector3(0, 0, add);
                    currentItemMarker.transform.position = hit.collider.bounds.center + a;
                    markerSnapped = true;
                }
            }

            //currentItemMarker.transform.position = hit.point;
        }
        else
        {
            currentItemMarker.transform.position = ray.GetPoint(4);
        }

        currentItemMarker.transform.rotation = Quaternion.identity;
        //player.transform.rotation; to make item turn with player
        //Quaternion.identity; to make item never turn, all placed items align (not to grid)

    }
}

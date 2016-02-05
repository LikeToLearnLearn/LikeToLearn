using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MinigameFish : MonoBehaviour {
    private FishingLogic fishingLogic;

    public Transform prefabWrong;
    public Transform prefabRight;

    public Texture image;
    private bool play = false;

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            fishingLogic = racingLogicObject.GetComponent<FishingLogic>();
        }
        if (fishingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        CreatePickups();
        
    }
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.name == "Cruscarp_right(Clone)")
            {
                //player hit the right fish
                print("you hit the right fish");
                GameObject rightFish = hit.transform.gameObject;
                //rightFish.GetComponent<PickUpRightAnswereFish>().enabledText = true;

            }
            if(hit.collider.name == "Cruscarp_wrong(Clone)")
            {
                //player hit the wrong fish
                print("you hit the wrong fish");
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            //spela minispel: fånga fisk

            // Demo creating guiText

            // sätta crosshair
            play = true;

            //GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().enabled = false;
            //GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().enabled = false;
            //print("Stop player movement");
            fishingLogic.CreateMultiplication(4, null);
        }

    }

    void CreatePickups()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("CreatePickUps");
            //Instantiate(prefabWrong, new Vector3((SetValue(5)), 121, (SetValue(10)) + 324), Quaternion.identity);
            Instantiate(prefabWrong, new Vector3(SetValue(5) - 5, 0, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));
        }
        Debug.Log("Create rigth pickUp");
        Instantiate(prefabRight, new Vector3(SetValue(5) - 5, 0, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));
    }

    float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }

    void OnGUI()
    {
        if (play == true)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, image.width, image.height), image);
        }
    }
}

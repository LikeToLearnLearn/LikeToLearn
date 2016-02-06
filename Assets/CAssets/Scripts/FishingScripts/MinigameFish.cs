using UnityEngine;

public class MinigameFish : MonoBehaviour {
    private FishingLogic fishingLogic;
    private static int wrongfish = 10;

    public Transform prefabWrong;
    public Transform prefabRight;

    public Texture image;
    private bool play = false;

    private GameObject[] fishes;
    private PickUpWrongAnswereFish wrongAnswere;

    private float myFieldOfView;

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        GameObject racingLogicObject = GameObject.FindWithTag("FishingController");
        if (racingLogicObject != null)
        {
            fishingLogic = racingLogicObject.GetComponent<FishingLogic>();
        }
        if (fishingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        myFieldOfView = Camera.main.fieldOfView;

    }
	
	// Update is called once per frame
	void Update () {
        if (play)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject fish = hit.transform.gameObject;
                if (hit.collider.name == "Cruscarp_right(Clone)")
                {
                    //player hit the right fish
                    fish.GetComponent<RightAnswereFish>().displayValue = true;

                    //zoom in camera script
                    if (Camera.main.fieldOfView > 50)
                        Camera.main.fieldOfView -= 1;
                }
                if (hit.collider.name == "cruscarp_wrong(Clone)")
                {
                    //player hit the wrong fish
                    fish.GetComponent<PickUpWrongAnswereFish>().displayValue = true;

                    //zoom in camera script
                    if (Camera.main.fieldOfView > 50)
                        Camera.main.fieldOfView -= 1;
                }


            }
            else
            {
                //zoom out camera script
                if (Camera.main.fieldOfView <= myFieldOfView)
                    Camera.main.fieldOfView += 1;
            }

            //Refresh wrong answers when player clicks on the right fish
            //Create a new fish with right answer
            if (fishingLogic.GetAnswered())
            {
                foreach (GameObject i in fishes)
                {
                    wrongAnswere = i.GetComponent<PickUpWrongAnswereFish>();
                    wrongAnswere.SetValue();
                }
                Instantiate(prefabRight, new Vector3(SetValue(5) - 5, -0.85f, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));
                fishingLogic.CreateMultiplication(4, null);
            }
            fishingLogic.SetAnswered(false);
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            //spela minispel: fånga fisk

            //Creating guiText


            //Create gui: display crosshair, question, score, timer...
            play = true;
            fishingLogic.ActiveQuestion();
            fishingLogic.ActivateSign();
            fishingLogic.UpdateScore();
            
            //Create 4*n math question
            fishingLogic.CreateMultiplication(4, null);
            CreatePickups();
        }

    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            play = false;
            fishingLogic.DeactivateQuestion();
            fishingLogic.DeactivateSign();
            DestroyAllPickups();
            Destroy(GameObject.Find("Cruscarp_right(Clone)"));
            fishingLogic.cleanPoints();
        }
    }

    void CreatePickups()
    {
        for (int i = 0; i < wrongfish; i++)
        {
            Instantiate(prefabWrong, new Vector3(SetValue(5) - 5, 0, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));
        }
        Instantiate(prefabRight, new Vector3(SetValue(5) - 5, 0, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));

        fishes = GameObject.FindGameObjectsWithTag("Fish");
    }

    float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }

    //Display crosshair
    void OnGUI()
    {
        if (play)
        {
            GUI.DrawTexture(new Rect(Screen.width/2 - image.width/2, Screen.height/2 - image.height/2, image.width, image.height), image);
        }
    }

    void DestroyAllPickups()
    {
        foreach (GameObject o in fishes)
        {
            Destroy(o, 0);
        }
    }
    
}

using System.Collections.Generic;
using UnityEngine;

public class MinigameFish : MonoBehaviour {
    //ärva minigameabstract

    private FishingLogic fishingLogic;
    private int answers = 5;

    public Transform fish;

    public Texture image;
    private bool play = false;

    private GameObject[] fishes;
    private Fish f;

    private float myFieldOfView;

    Ray ray;
    RaycastHit hit;
    private GameObject mhc;

    private bool gameOver = false;

    private GameObject ihc;
    private bool gameStart = false;

    // Use this for initialization
    void Start () {
        mhc = GameObject.FindWithTag("MinigameHud");
        mhc.SetActive(false);

        ihc = GameObject.FindWithTag("InstructionHud");
        ihc.SetActive(false);

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
        if (fishingLogic.GetRemainingTime() < 0)
        {
            gameOver = true;
            mhc.GetComponent<MinigameHUDController>().SetEndText("You have catched " + fishingLogic.GetCurrentScore().ToString() + " fish!");
            EndGame();
        }
        if (play)
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject fish = hit.transform.gameObject;
                if (hit.collider.name == "cruscarp(Clone)")
                {
                    //player hit the fish
                    fish.GetComponent<Fish>().displayValue = true;

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
                fishes = GameObject.FindGameObjectsWithTag("Fish");
                fishingLogic.CreateQuestion(answers);
                foreach (GameObject i in fishes)
                {
                    f = i.GetComponent<Fish>();
                    if(!f.IsRightFish())
                        f.SetAnswer();
                }
                Instantiate(fish, new Vector3(SetValue(5) - 5, -0.95f, SetValue(5) - 10), Quaternion.Euler(0, 90, 0));
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
            ihc.SetActive(true);
        }

    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            ihc.SetActive(false);
            gameOver = false;
            EndGame();
        }
    }

    void CreatePickups()
    {
        for (int i = 0; i < answers; i++)
        {
            Instantiate(fish, GeneradPosition(), Quaternion.Euler(0, 90, 0));
        }

        fishes = GameObject.FindGameObjectsWithTag("Fish");
    }

    Vector3 GeneradPosition()
    {
        Vector3 position = new Vector3(SetValue(5) - 5, -0.95f, SetValue(5) - 14);

        fishes = GameObject.FindGameObjectsWithTag("Fish");

        foreach (GameObject go in fishes)
        {
            while (position == go.transform.position)
                position.x = SetValue(5) - 5;
                position.z = SetValue(5) - 14;
        }
        return position;
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
        fishes = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject o in fishes)
        {
            Destroy(o);
        }
    }

    void EndGame()
    {
        if (gameOver)
            mhc.GetComponent<MinigameHUDController>().GameOver();
        else
        {
            mhc.SetActive(false);
            fishingLogic.ClearScore();
        }
        play = false;
        fishingLogic.StopGame();
        DestroyAllPickups();
    }

    public void GameStart()
    {
        ihc.SetActive(false);
        play = true;
        mhc.SetActive(true);
        mhc.GetComponent<MinigameHUDController>().GameStart();

        fishingLogic.StartGame();
        fishingLogic.CreateQuestion(answers);
        CreatePickups();
    }
}

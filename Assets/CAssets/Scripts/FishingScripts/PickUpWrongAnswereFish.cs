using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpWrongAnswereFish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic fishingLogic;

    private float points;
    private float f;
    private GameObject player;
    public bool displayValue = false;

    // Use this for initialization
    void Start()
    {
        GameObject fishingLogicObject = GameObject.FindWithTag("FishingController");
        if (fishingLogicObject != null)
        {
            fishingLogic = fishingLogicObject.GetComponent<FishingLogic>();
        }
        if (fishingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        points = 0;
        player = fishingLogic.GetPlayer();
        SetValue();


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + player.transform.position;       
        if (!displayValue)
        {
            text.SetActive(false);
        }
        if (displayValue)
        {
            text.SetActive(true);
        }
        displayValue = false;
        /*
        if (fishingLogic.GetUpdateWrongAnswere())
        {
            SetValue();
        }
        fishingLogic.SetUpdateWrongAnswere(false);*/
        
    }

    public void SetValue()
    {
        f = Mathf.Floor(Random.value * 100f);
        while(f == fishingLogic.GetMultiplicationAnswere())
            f = Mathf.Floor(Random.value * 100f);
        Debug.Log("New wrong answer: " + f);
        text.GetComponent<TextMesh>().text = "" + f;
    }

    public void empty() { }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class RightAnswereFish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic fishingLogic;

    private float points;
    private float f;

    // Use this for initialization
    void Start()
    {

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            fishingLogic = racingLogicObject.GetComponent<FishingLogic>();
        }
        if (fishingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        points = 0;
        SetValue();

    }

    // Update is called once per frame
    void Update()
    {



    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {
            gameObject.SetActive(false);

            fishingLogic.AddScore(5);

            points = points + f;
            //Debug.Log(points);

        }
    }


    void SetValue()
    {
        f = fishingLogic.GetMultiplicationAnswere();
        text.GetComponent<TextMesh>().text = "" + f;

    }



}

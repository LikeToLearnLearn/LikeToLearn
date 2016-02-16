using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Fish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic fishingLogic;
    private bool rightFish = false;
    private Question q;
    private String answer;

    //Point to right fish
    private static int point = 1;

    private float f;
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

        q = fishingLogic.GetQuestion();
        SetValue();

    }

    // Update is called once per frame
    void Update()
    {
        if (!displayValue)
        {
            text.SetActive(false);
        }
        if (displayValue)
        {
            text.SetActive(true);
        }
        displayValue = false;
    }

    public void SetValue()
    {
        //q = fishingLogic.GetQuestion();
        answer = q.Alternative();
        text.GetComponent<TextMesh>().text = answer;
        Debug.Log("Generated questions: " + q.Alternative());
    }

    void OnMouseDown()
    {
        q.Answer(answer);
        if (q.Correct())
        {
            fishingLogic.AddScore(point);
            fishingLogic.UpdateScore();
            //add fish
            GameController.control.AddItem(GameController.Item.Fish);
            fishingLogic.SetAnswered(true);
            Destroy(gameObject);
        }
    }

}
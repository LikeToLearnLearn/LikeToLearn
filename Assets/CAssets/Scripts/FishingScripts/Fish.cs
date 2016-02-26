using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Fish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic fishingLogic;
    private Question q;
    private String answer;
    private bool rightFish = false;

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
        SetAnswer();

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

    public void SetAnswer()
    {
        q = fishingLogic.GetQuestion();
        answer = q.GetAlternative();
        text.GetComponent<TextMesh>().text = answer;
        Debug.Log("Generated answer: " + answer);
    }

    void OnMouseDown()
    {
        q.Answer(answer);
        if (q.IsCorrect())
        {
            fishingLogic.AddScore(point);
            fishingLogic.UpdateScore();
            //add fish
            GameController.control.AddItem(GameController.Item.Fish);
            rightFish = true;
            fishingLogic.SetAnswered(true);
            Destroy(gameObject);
        }
    }

    public bool IsRightFish()
    {
        return rightFish;
    }

}
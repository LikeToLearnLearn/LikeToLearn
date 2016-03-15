﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRacing : MonoBehaviour {
    public GameObject text;
    private RacingLogic racingLogic;
    public Vector3 pointA, pointB, pointC;
    public GameObject me;

    private float points;
    private float f;
    private GameObject player;
    private string value;
    
    void Start()
    {
        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        player = racingLogic.GetPlayer();
        SetValue();

        racingLogic.AddPickUp(me);
        Debug.Log("Added" + me);
         
    }

    void Update()
    {     
        transform.Rotate(0,99 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {
            gameObject.SetActive(false);     
            racingLogic.GetQuestion().Answer(value);

            if (racingLogic.GetQuestion().IsCorrect())
            {
                racingLogic.AddTime(25);
                racingLogic.AddScore(5f);
                racingLogic.CreateQuestion(4);
                GameController.control.AddItem(GameController.Item.RedBalloon);
                racingLogic.UpdateAllPickUps();
            }
            else {
                racingLogic.AddScore(-2);
                racingLogic.AddTime(-5);
            }
        }
    }


     public void SetValue()
    {
        value = racingLogic.GetQuestion().GetAlternative();
        text.GetComponent<TextMesh>().text = value;
    }

}
  
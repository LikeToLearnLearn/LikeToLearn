﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DriveIntoWaterTrigger : MonoBehaviour {

    private RacingLogic racingLogic;

	void Start () {

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

    }

    void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {       
        if (c.tag.Equals("PlayerCar") || c.tag.Equals("Player" ))
        {
            racingLogic.StopRacing();
         }
     } 
}

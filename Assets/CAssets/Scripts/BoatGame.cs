﻿using UnityEngine;
using System.Collections;
using System;

public class BoatGame : MiniGameAbstract
{


    // Use this for initialization
    public override void Start()
    {
       
        //answerPoints = GameObject.Find("AnswerPoints").GetComponentsInChildren<Transform>();

        //SetUpQuestionAndAnswers();
    }

    /*
    private void SetUpQuestionAndAnswers()
    {
        CreateQuestion(answerPoints.Length);
        foreach (Transform t in answerPoints)
        {
            t.GetComponentInChildren<TextMesh>().text = GetQuestion().Alternative();

        }
    }
    */

    public void AnsweredCorrect(int difficulty)
    {
        AddScore(UnityEngine.Random.Range(0, 5) + difficulty * 5);
        AddTime(10 * difficulty);
    }

    public void AnsweredFalse(int difficulty)
    {
        AddScore(-(UnityEngine.Random.Range(0, 5) + difficulty * 1));

    }


}
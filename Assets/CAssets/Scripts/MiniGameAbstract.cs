﻿using UnityEngine;
using System.Collections;

public abstract class MiniGameAbstract : MonoBehaviour
{

    private bool playing = false;

    private float startTime; // Time since level load when minigame starts
    private float remainingTime; // Time left of current round
    //private float takenTime = 0;
    private int currentScore; // Score gained so far this round

    private Question currentQuestion; // Current question to answer

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public virtual void Update()
    {
        if (GetPlaying())
        {
            AddTime(-Time.deltaTime);
            //print("playing:" + remainingTime + " score: " + currentScore);

            if (GameController.control.name != "testmode")
            {
                //GameController.control.data.currentCourse.takenTime = GameController.control.data.currentCourse.takenTime + Time.deltaTime;
                GameController.control.addTakenMomentTime(Time.deltaTime);
                //Debug.Log(" takenTime = " + GameController.control.data.currentCourse.takenTime);
            }   

            if (GetRemainingTime() < 0)
            {
                StopGame();
            }                
        }
    }
    

    public virtual void StartGame()
    {
        ClearScore();
        ClearRemainingTime();
        startTime = Time.timeSinceLevelLoad;
        AddTime(90f); // 90s default remaining time
        GameController.control.AskForNewQuestions();

        playing = true;
    }

    public virtual void StopGame()
    {
        //TODO save highscore and whatnot
        //convert score to prize
        GameController.control.AddBalance(currentScore); // Default reward
        GameController.control.AddExp(currentScore/2);

        playing = false;
    }   


    public virtual float GetPlayedTime()
    {
        return Time.timeSinceLevelLoad - startTime;
    }
    public virtual bool GetPlaying()
    {
        return playing;
    }
    public virtual int GetCurrentScore()
    {
        return currentScore;
    }
    public virtual float GetRemainingTime()
    {
        return remainingTime;
    }


    public virtual void AddScore(int score)
    {
        currentScore += score;
    }

    public virtual void ClearScore()
    {
        currentScore = 0;
    }

    public virtual void AddTime(float time)
    {
        remainingTime += time;
    }

    public virtual void ClearRemainingTime()
    {
        remainingTime = 0;
    }


    public void CreateQuestion(int choices)
    {
        currentQuestion = GameController.control.GetQuestion(choices);
		//Debug.Log("question i MinigameAbstract: " + currentQuestion.GetQuestion());
    }

    public Question GetQuestion()
    {
        return currentQuestion;
    }
}

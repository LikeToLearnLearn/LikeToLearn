using UnityEngine;
using System.Collections;

public class BoatGame : MonoBehaviour {

    private bool playing;

    private float startTime;
    private float remainingTime;
    private int currentScore;


	// Use this for initialization
	void Start () {
        playing = false;
        remainingTime = 90f;
	}
	
	// Update is called once per frame
	void Update () {
        if(playing)
        {
            remainingTime -= Time.deltaTime;
            print("playing:" + remainingTime + " score: " + currentScore);
            if(remainingTime < 0)
            {
                StopGame();
            }
        }
	}

    public void StartGame()
    {
        startTime = Time.timeSinceLevelLoad;
        playing = true;

    }

    public void StopGame()
    {
        //TODO save highscore and whatnot
        //convert score to prize
        playing = false;
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }

    public void AddTime(float time)
    {
        remainingTime += time;
    }


}

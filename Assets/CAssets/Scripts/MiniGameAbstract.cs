using UnityEngine;
using System.Collections;

public abstract class MiniGameAbstract : MonoBehaviour
{

    private bool playing;

    private float startTime;
    private float remainingTime;
    private int currentScore;


    // Use this for initialization
    public virtual void Start()
    {
        playing = false;
        remainingTime = 90f; // 90s default remaining time
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (playing)
        {
            remainingTime -= Time.deltaTime;
            print("playing:" + remainingTime + " score: " + currentScore);
            if (remainingTime < 0)
            {
                StopGame();
            }
        }
    }

    public virtual void StartGame()
    {
        startTime = Time.timeSinceLevelLoad;
        playing = true;

    }

    public virtual void StopGame()
    {
        //TODO save highscore and whatnot
        //convert score to prize
        playing = false;
    }

    public virtual float GetPlayTime()
    {
        return Time.timeSinceLevelLoad - startTime;
    }

    public virtual void AddScore(int score)
    {
        currentScore += score;
    }

    public virtual void AddTime(float time)
    {
        remainingTime += time;
    }


}

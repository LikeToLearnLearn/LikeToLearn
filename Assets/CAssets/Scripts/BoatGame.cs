using UnityEngine;
using System.Collections;

public class BoatGame : MiniGameAbstract
{
    /*
    BoatGameHUDController hud;

    // Use this for initialization
    public override void Start()
    {
        playing = false;
    }*/

    public void AnsweredCorrect(int difficulty)
    {
        AddScore(Random.Range(0, 5) + difficulty * 5);
        AddTime(10 * difficulty);
    }

    public void AnsweredFalse(int difficulty)
    {
        AddScore(-(Random.Range(0, 5) + difficulty * 1));
    }

}

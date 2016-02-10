using UnityEngine;
using System.Collections;

public class BoatGame : MiniGameAbstract
{
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

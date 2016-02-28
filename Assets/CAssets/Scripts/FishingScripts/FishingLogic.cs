using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FishingLogic : MiniGameAbstract
{
    private float f;
    private float answere;

    //public GUIText scoreText;
    public GameObject text;
    public GameObject score;
    private GameObject player;
    private int TimeRemaining;
    private bool answered;

    // Use this for initialization
    public override void Start()
    {
        answered = false;
        player = null;
    }

    public bool GetAnswered()
    {
        return answered;
    }

    public void SetAnswered(bool s)
    {
        answered = s;
    }

    public float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }
}
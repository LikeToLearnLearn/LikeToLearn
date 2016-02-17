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

    public void SetSign(GameObject t)
    {
        score = t;
    }

    public void ActiveQuestion()
    {
        text.SetActive(true);
    }

    public void ActivateSign()
    {
        score.SetActive(true);
    }

    public void DeactivateSign()
    {
        score.SetActive(false);
    }

    public void DeactivateQuestion()
    {
        text.SetActive(false);
    }

    public float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }

    public void UpdateScore()
    {
        score.GetComponent<TextMesh>().text = "Score: " + GetCurrentScore();
    }

    public void SetQuestion()
    {
        text.GetComponent<TextMesh>().text = GetQuestion().question;
    }
}
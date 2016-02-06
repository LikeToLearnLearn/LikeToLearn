using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FishingLogic : MonoBehaviour
{
    private float points = 0;
    private float f;
    private float answere;

    //public GUIText scoreText;
    public GameObject text;
    public GameObject score;
    private GameObject player;
    private int TimeRemaining;
    private bool answered;


    // Use this for initialization
    void Start()
    {
        answered = false;
        player = null;
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetAnswered()
    {
        return answered;
    }

    public void SetAnswered(bool s)
    {
        answered = s;
    }

    public float GetMultiplicationAnswere()
    {
        return answere;

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

    public float SetPickUpPosition(float i)
    {
        return Mathf.Floor(Random.value * i) + 5;
    }

    public void SetPlayer(GameObject P)
    {
        player = P;

    }

    public GameObject GetPlayer()
    {
        return player;
    }


    void SetPoints(float f)
    {
        points = points + f;
    }

    public void AddScore(float newScoreValue)
    {
        points += newScoreValue;
        UpdateScore();
        Debug.Log("Present points: " + points);
    }

    public void UpdateScore()
    {
        score.GetComponent<TextMesh>().text = "Score: " + points;
    }

    public void CreateMultiplication(float n, GameObject t)
    {
        if (t == null) t = text;
        float a = n;
        float b = SetValue(10);
        answere = a*b;
        t.GetComponent<TextMesh>().text = a + " * " + b;
    }

    public void cleanPoints()
    {
        points = 0;
    }
}
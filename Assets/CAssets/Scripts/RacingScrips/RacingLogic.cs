using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RacingLogic : MonoBehaviour
{
    private float points;
    private float f;
    private float answere;

    public GUIText scoreText;
    public GameObject text;

    // Use this for initialization
    void Start()
    {
       
       points = 0;
       //UpdateScore();
       
    }

    void SetMultiplicationAnswere(float i)
    {
        answere = i;

    }

    public float GetMultiplicationAnswere()
    {
        return answere;

    }

    float SetValue(float i)
    {

        return Mathf.Floor(Random.value * i);


    }

    // Update is called once per frame
    void Update()
    {

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

    void UpdateScore()
    {
        //scoreText.text = "Score: " + points;
        text.GetComponent<TextMesh>().text = "Score: " + points;
    }

    public void CreateMultiplication(float n)
    {
        float a = n;
        float b = SetValue(10);
        SetMultiplicationAnswere(a * b);
        text.GetComponent<TextMesh>().text = a + " * " + b;
        
    }

   

   
}





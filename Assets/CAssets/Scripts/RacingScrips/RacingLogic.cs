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

    float SetPickUpPosition(float i)
    {

        return Mathf.Floor(Random.value * i) + 5;


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

    public void CreateMultiplication(float n, GameObject t  )
    {
        if (t == null) t = text;
        float a = n;
        float b = SetValue(10);
        SetMultiplicationAnswere(a * b);
        t.GetComponent<TextMesh>().text = a + " * " + b;
        
    }

    public void CreatePickups(Transform prefabWrong, Transform prefabRigth, float x, float y, float z)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("CreatePickUps");
            Instantiate(prefabWrong, new Vector3((SetPickUpPosition(5))+ i + x, y, (SetPickUpPosition(10))- i + z), Quaternion.identity);

        }

        Debug.Log("Create rigth pickUp");
        Instantiate(prefabRigth, new Vector3((SetValue(5)) + 2 + x, y, (SetValue(10)) - 2 + z), Quaternion.identity);
    }



}





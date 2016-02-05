using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FishingLogic : MonoBehaviour
{
    private float points;
    private float f;
    private float answere;

    public GUIText scoreText;
    public GameObject text;
    private GameObject sign;
    private GameObject player;
    private ArrayList pickUps;
    private int TimeRemaining;


    // Use this for initialization
    void Start()
    {
        points = 0;
        sign = null;
        player = null;
        pickUps = new ArrayList();

        //UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SetMultiplicationAnswere(float i)
    {
        answere = i;

    }

    public float GetMultiplicationAnswere()
    {
        return answere;

    }

    public void SetSign(GameObject t)
    {
        sign = t;

    }

    public void DeactivateSign()
    {
        sign.SetActive(false);


    }

    public GameObject GetSign()
    {
        if (sign == null) sign = text;
        return sign;
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
        if (sign == null) sign = text;
        points += newScoreValue;
        UpdateScore();
        Debug.Log("Present points: " + points);
    }

    void UpdateScore()
    {
        if (sign == null) sign = text;
        //scoreText.text = "Score: " + points;
        sign.GetComponent<TextMesh>().text = "Score: " + points;
    }

    public void CreateMultiplication(float n, GameObject t)
    {
        if (t == null) t = text;
        float a = n;
        float b = SetValue(10);
        SetMultiplicationAnswere(a * b);
        t.GetComponent<TextMesh>().text = a + " * " + b;

    }
    public void AddPickUp(GameObject p)
    {
        pickUps.Add(p);
        Debug.Log("Addeded: " + p);
    }

    public void DestroyAllPickUps()
    {
        foreach (GameObject o in pickUps)
        {
            Debug.Log("Destroyed: " + o);
            Destroy(o, 0);



        }
    }

}
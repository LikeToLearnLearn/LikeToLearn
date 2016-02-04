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
    private GameObject sign;
    private GameObject player;
    private ArrayList pickUps;

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

    public void AddPickUp(Object p)
    {
        pickUps.Add(p);
       
    }

    public void DestroyAllPickUps()
    {
        foreach (Object o in pickUps)
        {
            Destroy(o, 0);

        }
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

    public void CreatePickups(Transform prefabWrong, Transform prefabRigth, float x, float y, float z)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("CreatePickUps");
            AddPickUp(Instantiate(prefabWrong, new Vector3((SetPickUpPosition(5)) + i + x, y, (SetPickUpPosition(10)) - i + z), Quaternion.identity));
            

        }
        //MoveObject(prefabWrong, new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), 1);

        Debug.Log("Create rigth pickUp");
        AddPickUp(Instantiate(prefabRigth, new Vector3((SetPickUpPosition(5)) + x, y, (SetPickUpPosition(10)) + z), Quaternion.identity));
    }

    public IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        
    }

}
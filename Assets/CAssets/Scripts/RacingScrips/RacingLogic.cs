using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RacingLogic :MiniGameAbstract
{
    public GameObject IntroductionCanvas;

    private float points;
    private float f;
    private float answere;
    public GameObject text;

    private GameObject sign;
    private GameObject player;
    private ArrayList pickUps;

    private Vector3 pointA, pointB, pointC;
    private int numbersLeft;
    private Transform prefabWrong;
   
    private string multiplication;
    private float direction;
    
    private Question q;
    private bool message;
    private string MessageString;
    private bool GotRight;
    private bool GameStarted;
    private GameObject car;
    private GameObject Hud;
    
    public override void Start()
    {
        points = 0;
        pickUps = new ArrayList();

        sign = null;
        player = null;
        Hud = null;
        car = null;
        message = false;
        GotRight = true;
        GameStarted = false;
        
        CreateQuestion(4);
    }

   public override void Update()
    {
        base.Update();
        
        if (!GetPlaying()&& GameStarted)
        {
            StopRacing();
        }
        
        if (message == true) sign.GetComponent<TextMesh>().text = MessageString;

        if (GetRemainingTime() < 15  && GetRemainingTime() >= 3 && GetPlaying())
        {
            sign.GetComponent<TextMesh>().text =  GetRemainingTime().ToString("f0")+ " s left!!";
            message = false;
        }

        else if (GetRemainingTime() <= 2 && GetPlaying())
        {
            sign.GetComponent<TextMesh>().text = "Game Over";
            message = false;
        }
    }
    
    public void StartRaicing()
    {
        StartGame();
        SetGameStarted(true);
        
        player.SetActive(false);
        Hud.SetActive(true);
        car.SetActive(true);

        player.transform.position = new Vector3(360, 120, 340);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        DeactivateIntroductionCanvas();
        Cursor.visible = false;
    }

    public void StopRacing()
    {
            SetGameStarted(false);

            player.SetActive(true);
            car.SetActive(false);
            Hud.SetActive(false);
                        
            car.transform.position = new Vector3(318, 120, 318);
            car.transform.localEulerAngles = new Vector3(0, -43, 0);

            DeactivateSign();
            GettingMoney(GetCurrentScore());
    }

    public void DeactivateIntroductionCanvas()
    {
        IntroductionCanvas.SetActive(false);
    }

    public void ActivateIntroductionCanvas()
    {
        IntroductionCanvas.SetActive(true);
    }

    public void SetCar(GameObject c)
    {
        car = c;
    }

    public GameObject GetCar()
    {
        return car;
    }

    public void SetHud(GameObject h)
    {
        Hud = h;
    }

    public GameObject GetHud()
    {
        return Hud;
    }

    public void SetGameStarted(bool started)
    {
        GameStarted = started;
    }

    public bool GetGameStarted()
    {
        return GameStarted;
    }
    
    public void PutMessage(string s)
    {
        message = true;
        MessageString = s;
    }

    public void SetDirection(float d)
    {
        direction = d;
    }

    public float GetDirection()
    {
        return direction;
    }
    
    public void SetSign(GameObject t)
    {
        sign = t;
    }

    public void DeactivateSign()
    {
        if(sign != null) sign.SetActive(false);
        message = false;
    }

    public GameObject GetSign()
    {
        return sign;
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
        int newScore = (int) newScoreValue;
        base.AddScore(newScore);        
    }

    public float GetPoints()
    {
        return points;
    }

    public void CreateMultiplication(GameObject t)
    {
        q = GetQuestion();
		multiplication = q.GetQuestion();
    }

    public string GetMultiplication()
    {
        return multiplication;
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

    public void UpdateAllPickUps()
    {
        foreach (GameObject o in pickUps)
        {
            Debug.Log("Changed value on : " + o);
            o.GetComponent<PickUpRacing>().SetValue();
        }
    }

    public void GettingMoney(float score)
    {
        GameController.control.AddBalance((int)score);
    }

    public void CreatePickups(Transform prefab, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Instantiate(prefab, A, Quaternion.identity);
        Instantiate(prefab, B, Quaternion.identity);
        Instantiate(prefab, C, Quaternion.identity);
        Instantiate(prefab, D, Quaternion.identity);
    }
}
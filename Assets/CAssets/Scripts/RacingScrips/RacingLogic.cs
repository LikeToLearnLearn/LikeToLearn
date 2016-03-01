using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RacingLogic :/* MonoBehaviour//*/MiniGameAbstract
{
    public GameObject IntroductionCanvas;

    private float points;
    private float f;
    private float answere;

    //public GUIText scoreText;
    public GameObject text;

    private GameObject sign;
    private GameObject player;
    private ArrayList pickUps;
    //private int TimeRemaining;

    private Vector3 pointA, pointB, pointC;
    //private float timeLeft;
    //private float numbersLeft;
    private Transform prefabWrong;
    //private Transform prefabRight;
    //private Vector3 A, B, C, D;
    //private float right;
    //private bool update;
    private string multiplication;
    private float direction;

    private Question q;
    private bool message;
    private string MessageString;
    //private bool GotRight;
    private bool GameStarted;
    private Camera PlayerCamera;
    private GameObject car;
    private GameObject Hud;


    // Use this for initialization
    public override 
    void Start()
    {
      
        points = 0;
        sign = null;
        player = null;
        pickUps = new ArrayList();
        //numbersLeft = 0;
        //timeLeft = 3.0f;
        //update = false;
        message = false;
        //GotRight = false;
        GameStarted = false;
        Hud = null;
        car = null;
        


        //UpdateScore();
    }
    
    // Update is called once per frame
    public override 
        void Update()
    {
        base.Update();
        //timeLeft -= Time.deltaTime;

        //if (numbersLeft > 0 && timeLeft < 0)
        //{
        // if (!GotRight)
        // {
        //print("Putting");
        // if(numbersLeft == right) Instantiate(prefabRight, new Vector3((SetPickUpPosition(5)) + x, y, (SetPickUpPosition(10)) + z), Quaternion.identity);
        //else
        //Instantiate(prefabWrong, new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), Quaternion.identity);
        //numbersLeft--;
        //timeLeft = 1.0f;
        // }


        //}
        // else GotRight = false;

        
        if (!GetPlaying()&& GameStarted)
        {
            player.SetActive(true);
            print("Hämtade: " + player);
            car = GetCar();
            car.SetActive(false);
            Hud.SetActive(false);
            GettingMoney(GetCurrentScore());
            SetGameStarted(false);
            car.transform.position = new Vector3(318, 120, 318);
            DeactivateSign();
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

        /*else if (update == true && timeLeft > 0)
        {
            sign.GetComponent<TextMesh>().text = "Score: " + points;

        }
       
        else if (update == true)
        {
            sign.GetComponent<TextMesh>().text = GetMultiplication();
            update = false;
        }*/
    }

    public void DeactivateIntroductionCanvas()
    {
        IntroductionCanvas.SetActive(false);
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

    public void SetPlayerCamera(Camera c)
    {
        PlayerCamera = c;
    }

    public Camera GetPlayerCamera()
    {
        return PlayerCamera;
    }

    public void SetGameStarted(bool started)
    {
        GameStarted = started;
    }

    public bool GetGameStarted()
    {
        return GameStarted;
    }

   /* public void SetGotRight(bool set)
    {
        //GotRight = set;

    }*/

    public void PutMessage(string s)
    {
        message = true;
        MessageString = s;
    }

   /* public void SetQuestion(Question qu)
    {
        q = qu;

    }*/
   /* public override
     Question GetQuestion()
    {
        return q;
    }*/

    public void SetDirection(float d)
    {
        direction = d;

    }

    public float GetDirection()
    {

        return direction;
    }

    /*public void SetPointB(Vector3 b)
    {
        pointB  = b;

    }

    public void SetPointC(Vector3 c)
    {
        pointC = c;

    }

    public void SetPointA(Vector3 a)
    {
        pointA = a;

    }

    public Vector3 GetPointA()
    {
        return pointA;

    }

    public Vector3 GetPointB()
    {
        return pointB;

    }

    public Vector3 GetPointC()
    {
        return pointC;

    }*/


   /* void SetMultiplicationAnswere(float i)
    {
        answere = i;

    }

    public float GetMultiplicationAnswere()
    {
        return answere;

    }*/

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
        //if (sign == null) sign = text;
        return sign;
    }

   /*public float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }*/

    /*public float SetPickUpPosition(float i)
    {
        return Mathf.Floor(Random.value * i) + 5;
    }*/

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
        //if (sign == null) sign = text;
        points += newScoreValue;
        //Debug.Log("New score value: " + newScoreValue);
        //UpdateScore();
        int newScore = (int) newScoreValue;

        //Debug.Log("New score: " + newScore);
        base.AddScore(newScore);
        
    }

    public float GetPoints()
    {
        return points;
    }

   /* void UpdateScore()
    {
       // if (sign == null) sign = text;
        //scoreText.text = "Score: " + points;
        update = true;
        //sign.GetComponent<TextMesh>().text = "Score: " + points;
    }*/

    public void CreateMultiplication(GameObject t)
    {
        q = GetQuestion();
        //if (t == null) t = text;
        //float a = n;
        //float b = SetValue(10);
        //SetMultiplicationAnswere(a * b);
		multiplication = q.GetQuestion(); //"" + a + " * " + b;
                                     //t.GetComponent<TextMesh>().text = a + " * " + b;

        //t.GetComponent<TextMesh>().text = multiplication;  

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

    public void GettingMoney(float score)
    {
        GameController.control.AddBalance((int)score);

        /*
        int Hundredbills = 0;
        int points = Mathf.FloorToInt(score);
        if (GetCurrentScore() > 0)
        {
            for (int i = 0; i < (points / 100); i++)
            {
                GameController.control.AddItem(GameController.Item.HundredBill);
                Hundredbills++;
            }

            Debug.Log("Got : " + Hundredbills + "hundredbills");
            points = points - (Hundredbills * 100);

            int TenCoins = 0;
            for (int i = 0; i < (points / 10); i++)
            {
                GameController.control.AddItem(GameController.Item.TenCoin);
                TenCoins++;
            }
            Debug.Log("Got : " + TenCoins  + "tencoins");
            points = points - (TenCoins * 10);
            for (int i = 0; i < points; i++)
            {
                GameController.control.AddItem(GameController.Item.OneCoin);
            }
            Debug.Log("Got : " + points + "onecoins");
        }
        */
    }

    public void CreatePickups(Transform prefab, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        //numbersLeft = 4;
        //timeLeft = 1.0f;
        //prefabWrong = prefab;
        Instantiate(prefab, A, Quaternion.identity);
        Instantiate(prefab, B, Quaternion.identity);
        Instantiate(prefab, C, Quaternion.identity);
        Instantiate(prefab, D, Quaternion.identity);
        //this.A = A;
        //this.B = B;
        //this.C = C;
        //this.D = D;
        //right = SetValue(1) + 1;

        //for (int i = 0; i < 3; i++)
        //{
        // Debug.Log("CreatePickUps");
        // 
        //StartCoroutine(Example());
        //}
        //MoveObject(prefabWrong, new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), 1);

        //Debug.Log("Create rigth pickUp");
        //Instantiate(prefabRigth, new Vector3((SetPickUpPosition(5)) + x, y, (SetPickUpPosition(10)) + z), Quaternion.identity);
    }


    /*
    public void OldCreatePickups(Transform pbWrong, Transform pbRigth, float x, float y, float z)
    {
        numbersLeft =4;
        timeLeft = 3.0f;
        prefabWrong = pbWrong;
        prefabRight = pbRigth;
        this.x = x;
        this.y = y;
        this.z = z;
        right = SetValue(1) + 1;

        //for (int i = 0; i < 3; i++)
        //{
                       // Debug.Log("CreatePickUps");
           // Instantiate(prefabWrong, new Vector3((SetPickUpPosition(5)) + i + x, y, (SetPickUpPosition(10)) - i + z), Quaternion.identity);
            //StartCoroutine(Example());
        //}
        //MoveObject(prefabWrong, new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), new Vector3((SetPickUpPosition(5)) + 2 + x, y, (SetPickUpPosition(10)) - 2 + z), 1);

        //Debug.Log("Create rigth pickUp");
        //Instantiate(prefabRigth, new Vector3((SetPickUpPosition(5)) + x, y, (SetPickUpPosition(10)) + z), Quaternion.identity);
    }
    */
         
    
/*
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }*/
    /*
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
        
        
    }*/

}
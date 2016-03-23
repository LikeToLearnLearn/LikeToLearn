using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RacingLogic :MiniGameAbstract
{
    public GameObject introductionCanvas;
    public Transform balloon;

    private GameObject player;
    private GameObject car;
    private GameObject Hud;

    private ArrayList pickUps;
    private int feedbackTime;
    private bool message;
    private bool GameStarted;
    
         
    public override void Start()
    {
        pickUps = new ArrayList();

        player = GameObject.FindWithTag("Player");
        if(player == null) player = GameObject.FindWithTag("PlayerTest");
        player.transform.localEulerAngles = new Vector3(0, 278, 0);

        Hud = null;
        car = null;
        message = false;
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
        else if (message == true && feedbackTime > 0)
        {
           feedbackTime--;
        }

        else if (message == true && feedbackTime == 0)
        {
            Hud.GetComponent<MinigameHUDController>().DeactivateFeedback();
            message = false;
        }

        if (GetRemainingTime() < 15  && GetRemainingTime() >= 2 && GetPlaying())
        {
            PutMessage(GetRemainingTime().ToString("f0") + " s left!!");
            message = false;
        }

        else if (GetRemainingTime() <= 1 && GetPlaying())
        {
            PutMessage("Time's up!");
            message = false; 
        }
    }

    public void SetCar(GameObject c)
    {
        car = c;
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
          
    public GameObject GetPlayer()
    {
        return player;
    }

    public void ActivateIntroductionCanvas()
    {
        introductionCanvas.SetActive(true);
    }

    public void StartRaicing()
    {
        StartGame();
        SetGameStarted(true);
        
        player.SetActive(false);
        Hud.SetActive(true);
        car.SetActive(true);
        introductionCanvas.SetActive(false);

        player.transform.position = new Vector3(360, 120, 340);
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        
        Cursor.visible = false;
    }

    public void StopRacing()
    {
        SetGameStarted(false);

        player.SetActive(true);
        car.SetActive(false);

        PutMessage("");
        Hud.GetComponent<MinigameHUDController>().DeactivateFeedback();
        Hud.SetActive(false);
                        
        car.transform.position = new Vector3(330, 120, 318);
        car.transform.localEulerAngles = new Vector3(0, -43, 0);

        GameController.control.AddBalance(GetCurrentScore());
    }
    
    public void PutMessage(string s)
    {
        message = true;
        feedbackTime = 20;
        Hud.GetComponent<MinigameHUDController>().GiveFeedback(s);
    }
  
     public void AddPickUp(GameObject p)
     {
          pickUps.Add(p);
    }

    public void DestroyAllPickUps()
    {
        foreach (GameObject o in pickUps)
        {
            Destroy(o, 0);                
        }
    }

    public void UpdateAllPickUps()
    {
        foreach (GameObject o in pickUps)
        {
            o.GetComponent<PickUpRacing>().SetValue();
        }
    }

    public void CreatePickups(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Instantiate(balloon, A, Quaternion.identity);
        Instantiate(balloon, B, Quaternion.identity);
        Instantiate(balloon, C, Quaternion.identity);
        Instantiate(balloon, D, Quaternion.identity);
    }
}
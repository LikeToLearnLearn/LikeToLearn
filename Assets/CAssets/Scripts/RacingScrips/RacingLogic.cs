using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RacingLogic :MiniGameAbstract
{
    public GameObject IntroductionCanvas;
        
    private GameObject player;
    private ArrayList pickUps;

    private int feedbackTime;
    private Question q;

    private bool message;
    private bool GameStarted;

    private GameObject car;
    private GameObject Hud;


    
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
    
    public void StartRaicing()
    {
        StartGame();
        SetGameStarted(true);
        
        player.SetActive(false);
        Hud.SetActive(true);
        car.SetActive(true);

        player.transform.position = new Vector3(360, 120, 340);
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        DeactivateIntroductionCanvas();
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
                        
        car.transform.position = new Vector3(318, 120, 318);
        car.transform.localEulerAngles = new Vector3(0, -43, 0);

        GameController.control.AddBalance(GetCurrentScore());
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
        feedbackTime = 20;
        Hud.GetComponent<MinigameHUDController>().GiveFeedback(s);
    }
      
    public GameObject GetPlayer()
    {
        return player;
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

    public void CreatePickups(Transform prefab, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Instantiate(prefab, A, Quaternion.identity);
        Instantiate(prefab, B, Quaternion.identity);
        Instantiate(prefab, C, Quaternion.identity);
        Instantiate(prefab, D, Quaternion.identity);
    }
}
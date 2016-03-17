using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject Hud;

    private RacingLogic racingLogic;

    
    void Start () {
        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }
        racingLogic.SetGameStarted(false);
       
    }
	
	
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
      
        if ((c.tag.Equals("Player")|| c.tag.Equals( "PlayerTest")) && !racingLogic.GetGameStarted())
        {
            racingLogic.ActivateIntroductionCanvas();
            Cursor.visible = true;
            
            racingLogic.SetCar(car);
            racingLogic.SetHud(Hud);
        }
        
    }
           
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject Hud;
    //public GameObject player;

    private RacingLogic racingLogic;

	// Use this for initialization
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

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
       // car = GameObject.Find("Car");
        //Debug.Log("collision det with" + c.name);
        if (c.tag.Equals("PlayerCar"))
        //if (c.Equals(GameObject.Find("Car")))
        {

            GameObject Player = racingLogic.GetPlayer();
            Player.SetActive(true);
            car.SetActive(false);
            Hud.SetActive(false);
            racingLogic.GettingMoney(racingLogic.GetPoints());
            racingLogic.SetGameStarted(false);
            car.transform.position = new Vector3(318, 120, 318);
            //racingLogic.GetPlayer().SetActive(true);

            //var PlayerCamera = racingLogic.GetPlayerCamera();
            //if (PlayerCamera == null) Debug.Log("No PlayerCamera found");
            //else PlayerCamera.enabled = true;

            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }

   
}

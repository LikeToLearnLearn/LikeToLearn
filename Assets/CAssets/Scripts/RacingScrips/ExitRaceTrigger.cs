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
          GameObject Player = racingLogic.GetPlayer();
          print("Hämtade: " + Player);
          print("Playerpoisition: " + Player.transform.position);

        // car = GameObject.Find("Car");
        //Debug.Log("collision det with" + c.name);
        if (c.tag.Equals("PlayerCar"))
        //if (c.Equals(GameObject.Find("Car")))
        {

          
            print("Hämtade: " + Player);
            Player.SetActive(true);
            //car.SetActive(false);
            Hud = racingLogic.GetHud();
            Hud.SetActive(false);
            racingLogic.GettingMoney(racingLogic.GetPoints());
            racingLogic.SetGameStarted(false);
            car.transform.position = new Vector3(318, 120, 318);
            car.SetActive(false);
            car.transform.localEulerAngles = new Vector3(0, -43, 0);
            //car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            racingLogic.DeactivateSign();
            //racingLogic.GetPlayer().SetActive(true);

            //var PlayerCamera = racingLogic.GetPlayerCamera();
            //if (PlayerCamera == null) Debug.Log("No PlayerCamera found");
            //else PlayerCamera.enabled = true;

            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }

   
}

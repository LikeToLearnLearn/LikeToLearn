using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DriveIntoWaterTrigger : MonoBehaviour {

    private GameObject car;
    private  GameObject Hud;
    private GameObject Player;
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

            Player = racingLogic.GetPlayer();
            print("Hämtade: " + Player);
            car = racingLogic.GetCar();
            print("Hämtade: " + car);
            print("Playerpoisition: " + Player.transform.position);
            Hud = racingLogic.GetHud();
            Player.SetActive(true);
            Debug.Log("Huden som stängs av är: "+ Hud);
            Debug.Log("Bilen som stängs av är: " + car);
            car.SetActive(false);
            Hud.SetActive(false);
            
            racingLogic.GettingMoney(racingLogic.GetPoints());
            racingLogic.SetGameStarted(false);
            /*
            Quaternion rot = car.rotation;
            rot[0] = 0; //null rotation X
            rot[2] = 0; //null rotation Z
            car.rotation = rot;*/

            car.transform.position = new Vector3(329, 120, 318);
            //racingLogic.GetPlayer().SetActive(true);

            //var PlayerCamera = racingLogic.GetPlayerCamera();
            //if (PlayerCamera == null) Debug.Log("No PlayerCamera found");
            //else PlayerCamera.enabled = true;

            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }

   
}

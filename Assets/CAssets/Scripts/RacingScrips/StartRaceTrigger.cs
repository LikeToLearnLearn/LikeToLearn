using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    //public GameObject player;
    public GameObject Hud;
    //public Transform prefabWrong;
    //public Transform prefabRight;
    //public bool passed;
    //public GameObject light;
    //private GameObject sign;


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
        racingLogic.SetGameStarted(false);
  
        //sign = racingLogic.GetSign();
        
        //racingLogic.CreateMultiplication(4, null);
        //Debug.Log("Right answere :" + racingLogic.
            //GetMultiplicationAnswere());

        //car.GetComponent<Rigidbody>().freezeRotation = true;



    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
      
        if ((c.tag.Equals("Player")|| c.tag.Equals( "PlayerTest")) && !racingLogic.GetGameStarted())
        {
            racingLogic.StartGame();
            racingLogic.SetGameStarted(true);
            racingLogic.SetCar(car);
            racingLogic.SetHud(Hud);
            //racingLogic.CreatePickups(prefabWrong, prefabRight, 285, 120, 335 );

            //-----------
           car.SetActive(true);
           car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            
            //Camera ccam = car.GetComponentInChildren<Camera>();
            //print("CarCamer: " + ccam);
            //ccam.enabled = true;
            
            //print("Carpoisition: " + car.transform.position);
            Hud.SetActive(true);
            //print("Mjaou");
            //var CarCamera = car.GetComponent<Camera>();
            //var CarCamera = GameObject.Find("MultipurposeCameraRig").GetComponentInChildren<Transform>().GetComponentInChildren<Transform>().gameObject.GetComponent<Camera>();
            //if (CarCamera == null) Debug.Log("No CarCamera found");
            //else CarCamera.enabled = true;

            //var PlayerCamera = c.gameObject.GetComponent<Camera>();
            //if (PlayerCamera == null) Debug.Log("No PlayerCamera found");
            //else PlayerCamera.enabled = false;
            //racingLogic.SetPlayerCamera(PlayerCamera);

            //print("Sparade: " + c.gameObject);
            racingLogic.SetPlayer(c.gameObject);
            
            c.gameObject.SetActive(false);
            c.gameObject.transform.position = new Vector3(360, 120, 340);
            Camera pcam = c.gameObject.GetComponentInChildren<Camera>();
            //---------pcam.enabled = false;
            print("PlayerCamera = " + pcam);

            //Debug.Log("Found object: " + GetComponentInParent("PlayerController").name);
            //Debug.Log("StartTrigger: " + c. name);
            // sign.SetActive(true);
        }
        
    }
    /*
    float SetValue(float i)
    {
        
        return Mathf.Floor(Random.value * i) + 5 ;
       

    }*/
        
}

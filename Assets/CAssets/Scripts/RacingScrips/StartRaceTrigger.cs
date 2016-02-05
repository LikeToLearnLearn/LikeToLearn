using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject player;
    public Transform prefabWrong;
    public Transform prefabRight;
    public bool passed;
    public GameObject light;
    private GameObject sign;


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
        passed = false;
        sign = racingLogic.GetSign();
        

        racingLogic.CreateMultiplication(4, null);
        Debug.Log("Right answere :" + racingLogic.
            GetMultiplicationAnswere());


    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
      
        if ((c.tag.Equals("Player")|| c.tag.Equals( "PlayerTest")) && passed == false)
        {
            passed = true;
            racingLogic.CreatePickups(prefabWrong, prefabRight, 285, 120, 335 );
            car.SetActive(true);
            sign.SetActive(true);
            c.gameObject.SetActive(false);
            //Debug.Log("Found object: " + GetComponentInParent("PlayerController").name);
            Debug.Log("StartTrigger: " + c. name);
        }
        
    }

    float SetValue(float i)
    {
        
        return Mathf.Floor(Random.value * i) + 5 ;
       

    }
        
}

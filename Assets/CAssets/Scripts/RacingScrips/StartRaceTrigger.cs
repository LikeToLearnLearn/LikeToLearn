using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject player;
    public Transform prefabWrong;
    public Transform prefabRight;

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
      
        if (c.tag.Equals("Player"))
        {
            racingLogic.CreatePickups(prefabWrong, prefabRight, 285, 120, 335 );
            car.SetActive(true);
            player.SetActive(false);
            Debug.Log("collision player");
        }
        
    }

    float SetValue(float i)
    {
        
        return Mathf.Floor(Random.value * i) + 5 ;
       

    }
        
}

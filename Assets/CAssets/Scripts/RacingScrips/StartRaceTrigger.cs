using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject player;
    public Transform prefabWrong;
    public Transform prefabRigtht;

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

        CreatePickups();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
      
        if (c.tag.Equals("Player"))
        {
            car.SetActive(true);
            player.SetActive(false);
            Debug.Log("collision player");
        }

    }

    float SetValue(float i)
    {
        
        return Mathf.Floor(Random.value * i) + 5 ;
       

    }

    void CreatePickups()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("CreatePickUps");
            Instantiate(prefabWrong, new Vector3((SetValue(5)) + 294F,  120, (SetValue(10)) + 324), Quaternion.identity);
                        
        }

       Debug.Log("Create rigth pickUp");
            Instantiate(prefabRigtht, new Vector3((SetValue(5)) + 294F, 120, (SetValue(10)) + 324), Quaternion.identity);
    }

    
 
}

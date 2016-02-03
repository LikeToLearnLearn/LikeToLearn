using UnityEngine;
using System.Collections;

public class MuliplicationTrigger : MonoBehaviour {

    public GameObject text;
    private RacingLogic racingLogic;
    public Transform prefabWrong;
    public Transform prefabRight;
    private bool passed;


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

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
       
        if (c.tag.Equals("PlayerCar") && passed == false)
        {
            passed = true;
            racingLogic.CreatePickups(prefabWrong, prefabRight, 280, 120, 354);
            racingLogic.CreateMultiplication(6, text);
        }
         

    }

}

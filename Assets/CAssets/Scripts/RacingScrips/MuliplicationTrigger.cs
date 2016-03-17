using UnityEngine;
using System.Collections;

public class MuliplicationTrigger : MonoBehaviour {

    private RacingLogic racingLogic;

    public GameObject BalloonPointA;
    public GameObject BalloonPointB;
    public GameObject BalloonPointC;
    public GameObject BalloonPointD;

    private GameObject PlayerCar;
    
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
        racingLogic.CreatePickups(BalloonPointA.transform.position, BalloonPointB.transform.position, BalloonPointC.transform.position, BalloonPointD.transform.position);
        
    }
    
   void update()
    {

    }
}

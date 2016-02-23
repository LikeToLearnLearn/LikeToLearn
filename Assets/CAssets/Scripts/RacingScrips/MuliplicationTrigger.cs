using UnityEngine;
using System.Collections;

public class MuliplicationTrigger : MonoBehaviour {

    public GameObject text;
   // public GameObject text2;
    public GameObject PlayerCar;
    private RacingLogic racingLogic;
    public Transform prefabWrong;
    //public Transform prefabRight;
    //public GameObject firstThrowingPoint;
    //public GameObject throwingPoint;
    //public GameObject secondThrowingPoint;

    public GameObject BalloonPointA;
    public GameObject BalloonPointB;
    public GameObject BalloonPointC;
    public GameObject BalloonPointD;

    // private bool passed;
    //private Vector3 offset;
    private bool turned;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;

    //private Vector3 pointC;

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
        turned = false;

      
        //racingLogic.SetPointC(pointC);



    }

    

    void LateUpdate()
    {
        if(racingLogic.GetDirection() == 1) text.transform.position = new Vector3(PlayerCar.transform.position.x -4, PlayerCar.transform.position.y + 4, PlayerCar.transform.position.z + 4) ;
        else text.transform.position = new Vector3(PlayerCar.transform.position.x + 4, PlayerCar.transform.position.y + 4, PlayerCar.transform.position.z - 4);
    }

    // Update is called once per frame
    void Update () {
       
	}

    void OnTriggerEnter(Collider c)
    {
       
        if (c.tag.Equals("PlayerCar") && racingLogic.GetDirection()== 1)
        {
            Debug.Log("MultiplicationTrigger collision det with" + c.name);
            //passed = true;

            racingLogic.DeactivateSign();
            racingLogic.SetSign(text);
            racingLogic.SetPlayer(PlayerCar);
            text.SetActive(true);
            if (turned)
            {
                text.transform.Rotate(0, 180, 0);
                turned = false;
            }
            racingLogic.CreateMultiplication(6, text);
           
            Debug.Log("Right answere :" + racingLogic.GetMultiplicationAnswere());
            racingLogic.CreatePickups(prefabWrong, BalloonPointA.transform.position, BalloonPointB.transform.position, BalloonPointC.transform.position, BalloonPointD.transform.position);

            //offset = PlayerCar.transform.position - text.transform.position;

            //pointA = firstThrowingPoint.transform.position;
            //pointB = throwingPoint.transform.position;
            //pointC = secondThrowingPoint.transform.position;
            //racingLogic.SetPointA(pointA);
            //racingLogic.SetPointB(pointB);
            //racingLogic.SetPointC(pointC);


        }

        if (c.tag.Equals("PlayerCar") && racingLogic.GetDirection() == 2)
        {
            Debug.Log("MultiplicationTrigger collision det with" + c.name);
            //passed = true;

            racingLogic.DeactivateSign();
            racingLogic.SetSign(text);
            racingLogic.SetPlayer(PlayerCar);
            if (!turned)
            {
                text.transform.Rotate(0, 180, 0);
                turned = true;
            }
            text.SetActive(true);
            racingLogic.CreateMultiplication(5, text);

            Debug.Log("Right answere :" + racingLogic.GetMultiplicationAnswere());
            racingLogic.CreatePickups(prefabWrong, BalloonPointA.transform.position, BalloonPointB.transform.position, BalloonPointC.transform.position, BalloonPointD.transform.position);

            //offset = PlayerCar.transform.position - text.transform.position;

            //pointA = firstThrowingPoint.transform.position;
            //pointB = throwingPoint.transform.position;
            //pointC = secondThrowingPoint.transform.position;
            //racingLogic.SetPointA(pointA);
            //racingLogic.SetPointB(pointC);
            //racingLogic.SetPointC(pointB);


        }


    }

}

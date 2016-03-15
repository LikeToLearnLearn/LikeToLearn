using UnityEngine;
using System.Collections;

public class MuliplicationTrigger : MonoBehaviour {

    private RacingLogic racingLogic;
    public Transform prefabWrong;

    public GameObject BalloonPointA;
    public GameObject BalloonPointB;
    public GameObject BalloonPointC;
    public GameObject BalloonPointD;

    private bool turned;
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
        turned = false;
        PlayerCar = null;
        racingLogic.CreatePickups(prefabWrong, BalloonPointA.transform.position, BalloonPointB.transform.position, BalloonPointC.transform.position, BalloonPointD.transform.position);
        
    }



    void LateUpdate()
    {
        PlayerCar = racingLogic.GetCar();
       /* if (PlayerCar != null)
        {
            if (racingLogic.GetDirection() == 1) text.transform.position = new Vector3(PlayerCar.transform.position.x - 4, PlayerCar.transform.position.y + 4, PlayerCar.transform.position.z + 4);
            else text.transform.position = new Vector3(PlayerCar.transform.position.x + 4, PlayerCar.transform.position.y + 4, PlayerCar.transform.position.z - 4);
        }*/
    }
    
    void Update () {
       
	}

    void OnTriggerEnter(Collider c)
    {
       /*
        if (c.tag.Equals("PlayerCar") && racingLogic.GetDirection()== 1)
        {
            racingLogic.DeactivateSign();
            //racingLogic.SetSign(text);
            //text.SetActive(true);
           /* if (turned)
            {
                text.transform.Rotate(0, 180, 0);
                turned = false;
            }*/
       // }

       /* if (c.tag.Equals("PlayerCar") && racingLogic.GetDirection() == 2)
        {
            Debug.Log("MultiplicationTrigger collision det with" + c.name);

            racingLogic.DeactivateSign();
            //racingLogic.SetSign(text);
            /*text.SetActive(true);
            if (!turned)
            {
                text.transform.Rotate(0, 180, 0);
                turned = true;
            }
           
            racingLogic.CreateMultiplication(text);
        }*/
        
    }

}

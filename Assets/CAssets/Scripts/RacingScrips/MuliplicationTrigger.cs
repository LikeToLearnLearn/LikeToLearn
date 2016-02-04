using UnityEngine;
using System.Collections;

public class MuliplicationTrigger : MonoBehaviour {

    public GameObject text;
    public GameObject PlayerCar;
    private RacingLogic racingLogic;
    public Transform prefabWrong;
    public Transform prefabRight;

    private bool passed;
    private Vector3 offset;
    private 

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
    

    void LateUpdate()
    {
        text.transform.position = new Vector3(PlayerCar.transform.position.x -3, PlayerCar.transform.position.y + 2, PlayerCar.transform.position.z + 4) ;
    }

    // Update is called once per frame
    void Update () {
       
	}

    void OnTriggerEnter(Collider c)
    {
       
        if (c.tag.Equals("PlayerCar") && passed == false)
        {
            
            Debug.Log("MultiplicationTrigger collision det with" + c.name);
            passed = true;
            racingLogic.SetSign(text);
            racingLogic.SetPlayer(PlayerCar);
            text.SetActive(true);
            racingLogic.CreateMultiplication(6, text);
            Debug.Log("Right answere :" + racingLogic.GetMultiplicationAnswere());
            racingLogic.CreatePickups(prefabWrong, prefabRight, transform.position.x-33, transform.position.y-1, transform.position.z - 5);
            offset = PlayerCar.transform.position - text.transform.position;
           

        }
         

    }

}

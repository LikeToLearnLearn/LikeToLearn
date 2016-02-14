using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CrossRoadTrigger : MonoBehaviour
{
    public GameObject RockLeft;
    public GameObject RockRight;
    private RacingLogic racingLogic;
    private float way;

    // Use this for initialiation
    void Start()
    {
        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        way = racingLogic.SetValue(1) + 1;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {
            if (way == 1)
            {
                RockLeft.SetActive(true);
            }

            else RockRight.SetActive(true);


        }
    }
    

}
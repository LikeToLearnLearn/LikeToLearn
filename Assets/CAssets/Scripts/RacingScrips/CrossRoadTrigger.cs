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

       
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider c)
    {
            

        if (c.gameObject.CompareTag("PlayerCar"))
        {
            way = racingLogic.SetValue(1);
            Debug.Log("way: " + way);
          
            if (way == 1)
            {
                RockLeft.SetActive(true);
                RockRight.SetActive(false);
            }

            else
            {
                RockRight.SetActive(true);
                RockLeft.SetActive(false);
            }


            }
    }
    

}
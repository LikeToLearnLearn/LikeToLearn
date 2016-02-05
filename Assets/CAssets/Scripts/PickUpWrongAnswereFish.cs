using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpWrongAnswereFish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic racingLogic;
    private GameObject me;

    private float points;
    private float f;
    private GameObject player;


    // Use this for initialization
    void Start()
    {
        var pointA = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z - 5);


        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<FishingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        points = 0;
        player = racingLogic.GetPlayer();
        SetValue();

        racingLogic.AddPickUp(me);


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + player.transform.position;       

    }

    void OnTriggerEnter(Collider c)
    {
    }


    void SetValue()
    {
        f = Mathf.Floor(Random.value * 100f);
        text.GetComponent<TextMesh>().text = "" + f;

    }

}

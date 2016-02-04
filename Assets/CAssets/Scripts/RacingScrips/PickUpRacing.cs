using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRacing : MonoBehaviour {
    public GameObject text;
    private RacingLogic racingLogic;
    public Vector3 pointB, pointC;
    public GameObject me;

    private float points;
    private float f;
    private GameObject player;
   

    // Use this for initialization
    IEnumerator
    //void 
        Start()
    {
        var pointA = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z - 5);
        

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        points = 0;
        player = racingLogic.GetPlayer();
        SetValue();

        racingLogic.AddPickUp(me);

        pointB = new Vector3(pointA.x + racingLogic.SetPickUpPosition(5), pointA.y, pointA.z - racingLogic.SetPickUpPosition(5));
        pointC = new Vector3(pointB.x + racingLogic.SetPickUpPosition(10), pointB.y/* + racingLogic.SetPickUpPosition(1)*/, pointB.z - racingLogic.SetPickUpPosition(10));
        while (true)
        {
            yield return StartCoroutine(racingLogic.MoveObject(transform, pointC, pointB, racingLogic.SetValue(5)));
            yield return StartCoroutine(racingLogic.MoveObject(transform, pointB, pointA, racingLogic.SetValue(5)));
            //turn object
            transform.Rotate(Vector3.right * Time.deltaTime);
            yield return StartCoroutine(racingLogic.MoveObject(transform, pointC, pointA, racingLogic.SetValue(5)));
            
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + player.transform.position;       

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {

            gameObject.SetActive(false);

            racingLogic.AddScore(-2);

            points = points + f;
            //Debug.Log(points);


        }
    }


    void SetValue()
    {
        f = Mathf.Floor(Random.value * 100f);
        text.GetComponent<TextMesh>().text = "" + f;

    }

}
  
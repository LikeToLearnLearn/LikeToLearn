using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRightAnswere : MonoBehaviour {
    public GameObject text;
    private RacingLogic racingLogic;

    private float points;
    private float f;
    private GameObject sign;
    public Vector3 pointB, pointC;

    // Use this for initialization
    IEnumerator
    //void  
    Start() {

        var pointA = transform.position;

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if(racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

       // racingLogic.CreateMultiplication(4, null);
        //Debug.Log("Right answere :" + racingLogic.
          //  GetMultiplicationAnswere());

        points = 0;
        SetValue();

        pointB = new Vector3(pointA.x + racingLogic.SetPickUpPosition(5), pointA.y, pointA.z - racingLogic.SetPickUpPosition(5));
        pointC = new Vector3(pointB.x + racingLogic.SetPickUpPosition(5), pointB.y - racingLogic.SetPickUpPosition(2), pointB.z - racingLogic.SetPickUpPosition(5));
        while (true)
        {
            yield return StartCoroutine(racingLogic.MoveObject(transform, pointC, pointB, racingLogic.SetValue(5)));
            yield return StartCoroutine(racingLogic.MoveObject(transform, pointB, pointA, racingLogic.SetValue(5)));
            //turn fish
            transform.Rotate(Vector3.right* Time.deltaTime);
        yield return StartCoroutine(racingLogic.MoveObject(transform, pointC, pointA, 3.0f));



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
            gameObject.SetActive(false);

            racingLogic.AddScore(5);

            points = points + f;
            //Debug.Log(points);
            racingLogic.DestroyAllPickUps();
            racingLogic.DeactivateSign();


        }
    }


    void SetValue()
    {
        f = racingLogic.GetMultiplicationAnswere();
        text.GetComponent<TextMesh>().text = "" +f;

    }
   

   
}
  
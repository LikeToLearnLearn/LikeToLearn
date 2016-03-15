using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRacing : MonoBehaviour {
    public GameObject text;
    private RacingLogic racingLogic;
    public Vector3 pointA, pointB, pointC;
    public GameObject me;

    private float points;
    private float f;
    private GameObject player;
    private string value;



    // Use this for initialization
    //IEnumerator
    void 
        Start()
    {

        //var pointA = transform.position; //new Vector3(transform.position.x, transform.position.y, transform.position.z);
        

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

        //points = 0;
        player = racingLogic.GetPlayer();
        SetValue();

        racingLogic.AddPickUp(me);
        Debug.Log("Added" + me);
        
       // pointA = transform.position ; // racingLogic.GetPointA();
       // pointB = racingLogic.GetPointB();
        //pointC = racingLogic.GetPointC();
       // if (pointA == null) pointA = transform.position;
        //if (pointB == null) pointB = new Vector3(pointA.x + racingLogic.SetPickUpPosition(5), pointA.y, pointA.z - racingLogic.SetPickUpPosition(5));
        //if (pointC == null) pointC = new Vector3(pointB.x + racingLogic.SetPickUpPosition(10), pointB.y/* + racingLogic.SetPickUpPosition(1)*/, pointB.z - racingLogic.SetPickUpPosition(10));
        
       // while (true)
        //{
            // var kvar yield return StartCoroutine(racingLogic.MoveObject(transform, pointB, pointC, 8.0f/*racingLogic.SetValue(5)*/));
            //ield return StartCoroutine(racingLogic.MoveObject(transform, pointC, pointA, 3.0f/*racingLogic.SetValue(5)*/));
            //turn object
           // var kvar  transform.Rotate(Vector3.right * Time.deltaTime);
            //yield return StartCoroutine(racingLogic.MoveObject(transform, pointB, pointC, 2.0f));
            
       // }

       

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + player.transform.position;       
        transform.Rotate(0, 99 * Time.deltaTime, 0);
        //SetValue();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {

            gameObject.SetActive(false);     
            //points = points + f;
            racingLogic.GetQuestion().Answer(value);
            if (racingLogic.GetQuestion().IsCorrect())
            {
                //gameObject.SetActive(false);

                racingLogic.AddTime(25);
                racingLogic.AddScore(5f);
                racingLogic.PutMessage("Yes!!");
                racingLogic.CreateQuestion(4);
                //racingLogic.SetGotRight(true);
                //Debug.Log(points);
                //racingLogic.DestroyAllPickUps();
                GameController.control.AddItem(GameController.Item.RedBalloon);
                //racingLogic.SetGotRight(true);
                racingLogic.UpdateAllPickUps();
                //racingLogic.DeactivateSign();
            }
            else {
                racingLogic.AddScore(-2);
                racingLogic.AddTime(-5);
            }

            //Debug.Log(points);


        }
    }


     public void SetValue()
    {
        /* f = Mathf.Floor(Random.value * 100f);
         while(f== racingLogic.GetMultiplicationAnswere()) f = Mathf.Floor(Random.value * 100f);
         if (racingLogic.GetDirection() == 2) text.transform.Rotate(0, 180, 0);
         text.GetComponent<TextMesh>().text = "" + f;*/

        //if (racingLogic.GetGotRight() == true)
       // {
            value = racingLogic.GetQuestion().GetAlternative();
            text.GetComponent<TextMesh>().text = value;
            //racingLogic.SetGotRight(false);
        //}

    }

}
  
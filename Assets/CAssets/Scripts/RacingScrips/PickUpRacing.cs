using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRacing : MonoBehaviour {
    public GameObject text;
    public GameObject me;
    private RacingLogic racingLogic;
    public Vector3 pointA, pointB, pointC;
    

    private float points;
    private float f;
    private string value;
    
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

        SetValue();

        racingLogic.AddPickUp(me);
        Debug.Log("Added" + me);
         
    }

    void Update()
    {     
        transform.Rotate(0,99 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("PlayerCar"))
        {
            gameObject.SetActive(false);     
            racingLogic.GetQuestion().Answer(value);

            if (racingLogic.GetQuestion().IsCorrect())
            {
                racingLogic.AddTime(20);
                racingLogic.AddScore(5);

                racingLogic.CreateQuestion(4);
                racingLogic.UpdateAllPickUps();
                racingLogic.PutMessage("Yes!!");

                GameController.control.AddItem(GameController.Item.RedBalloon);
            }
            else {
                racingLogic.AddScore(-3);
                racingLogic.AddTime(-15);
            }
        }
    }


     public void SetValue()
    {
        value = racingLogic.GetQuestion().GetAlternative();
        if(text!=null) text.GetComponent<TextMesh>().text = value;
    }

}
  
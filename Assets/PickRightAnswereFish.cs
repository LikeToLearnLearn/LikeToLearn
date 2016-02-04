using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PickUpRightAnswereFish : MonoBehaviour
{
    public GameObject text;
    private RacingLogic racingLogic;

    private float points;
    private float f;

    public TextMesh text_tap;
    public bool enabledText = false;

    // Use this for initialization
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

        racingLogic.CreateMultiplication(4, null);
        Debug.Log("Right answere :" + racingLogic.
            GetMultiplicationAnswere());

        points = 0;
        SetValue();

        text_tap = GameObject.Find("weight").GetComponent<TextMesh>();

        //text.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabledText == true)
        {
            text_tap.gameObject.SetActive(true);
        }
        if (enabledText == false)
        {
            text_tap.gameObject.SetActive(false);
        }

    }


    void SetValue()
    {
        f = racingLogic.GetMultiplicationAnswere();
        text.GetComponent<TextMesh>().text = "" + f;

    }



}

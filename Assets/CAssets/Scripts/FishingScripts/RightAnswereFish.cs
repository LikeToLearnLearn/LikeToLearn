using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class RightAnswereFish : MonoBehaviour
{
    public GameObject text;
    private FishingLogic fishingLogic;

    //Point to right fish
    private static float point = 5;

    private float f;
    public bool displayValue = false;

    // Use this for initialization
    void Start()
    {

        GameObject fishingLogicObject = GameObject.FindWithTag("FishingController");
        if (fishingLogicObject != null)
        {
            fishingLogic = fishingLogicObject.GetComponent<FishingLogic>();
        }
        if (fishingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }
        
        SetValue();

    }

    // Update is called once per frame
    void Update()
    {
        if (!displayValue)
        {
            text.SetActive(false);
        }
        if (displayValue)
        {
            text.SetActive(true);
        }
        displayValue = false;
    }

    public void SetValue()
    {
        f = fishingLogic.GetMultiplicationAnswere();
        text.GetComponent<TextMesh>().text = "" + f;
    }

    void OnMouseDown()
    {
        fishingLogic.AddScore(point);
        fishingLogic.SetAnswered(true);
        Destroy(gameObject);
    }

}

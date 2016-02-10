using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatGameHUDController : MonoBehaviour {

    MiniGameAbstract minigame;
    Text timeText;
    Text scoreText;

	// Use this for initialization
	void Start () {

        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        minigame = GameObject.Find("BoatGame").GetComponent<BoatGame>();
        /*
                if (GetComponentInParent<Transform>().gameObject.name.Equals("BoatGame"))
                {
                    minigame = GetComponentInParent<BoatGame>();
                }
                //else if parent name equals another minigame...

                Text[] uiTexts = GetComponentsInChildren<Text>();
                foreach (Text text in uiTexts)
                {
                    if (text.gameObject.name.Equals("TimeText"))
                    {
                        timeText = text;
                    }
                    else if (text.gameObject.name.Equals("ScoreText"))
                    {
                        scoreText = text;
                    }
                }
                */
    }
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time left: " + minigame.GetRemainingTime().ToString("f1");
        scoreText.text = "Score: " + minigame.GetCurrentScore().ToString();

	}
}

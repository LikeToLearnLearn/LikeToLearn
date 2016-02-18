using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatGameHUDController : MonoBehaviour {

    private MiniGameAbstract minigame;
    private QuestionPoint qp;
    private Text timeText;
    private Text scoreText;
    private Text questionText;

	// Use this for initialization
	void Start() {

        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        questionText = GameObject.Find("HUDQuestionText").GetComponent<Text>();
        
        //Subclass to minigame
        minigame = GameObject.Find("BoatGame").GetComponent<BoatGame>();


        qp = GameObject.Find("QuestionPoint Easy").GetComponent<QuestionPoint>();
        
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
	void Update() {
        timeText.text = "Time left: " + minigame.GetRemainingTime().ToString("f1");
        scoreText.text = "Score: " + minigame.GetCurrentScore().ToString();
        
        UpdateHUDQuestion();
	}

    public void UpdateHUDQuestion()
    {
        print(questionText.text);
        questionText.text = qp.GetQuestionAsString();
        questionText.GetComponent<Animator>().Play("GrowIn");
        //questionText.GetComponent<Animator>().Stop();

    }
}

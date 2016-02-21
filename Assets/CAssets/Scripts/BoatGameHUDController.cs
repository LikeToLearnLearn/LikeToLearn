using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatGameHUDController : MonoBehaviour {

    private MiniGameAbstract minigame;
    private QuestionPoint qp;
    private Text timeText;
    private Text scoreText;
    private Text questionText;

    private string q;

	// Use this for initialization
	void Start() {

        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        questionText = GameObject.Find("HUDQuestionText").GetComponent<Text>();
        
        //Subclass to minigame
        minigame = GameObject.Find("BoatGame").GetComponent<BoatGame>();


        qp = GameObject.Find("QuestionPoint Easy").GetComponent<QuestionPoint>();


    }
	
	// Update is called once per frame
	void Update() {
        timeText.text = "Time left: " + minigame.GetRemainingTime().ToString("f1");
        scoreText.text = "Score: " + minigame.GetCurrentScore().ToString();
        
        if (q != qp.GetQuestionAsString())
        {
           UpdateHUDQuestion();
        }
	}

    public void UpdateHUDQuestion()
    {
        print(questionText.text);
        questionText.text = qp.GetQuestionAsString();
        q = qp.GetQuestionAsString();
        questionText.GetComponent<Animator>().SetTrigger("OnNewQuestion");

    }

}

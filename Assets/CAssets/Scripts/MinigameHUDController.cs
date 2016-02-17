using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameHUDController : MonoBehaviour {

    public GameObject minigameGO; // gameobject the minigame script is attached to
    private MiniGameAbstract minigame; // minigame script, subclass of minigameabstract
    private QuestionPoint qp;
    private Text timeText;
    private Text scoreText;
    private Text questionText;

	// Use this for initialization
	void Start() {
        // Use the gameobjects with these names. They must each have a Text component.
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        questionText = GameObject.Find("HUDQuestionText").GetComponent<Text>();

        minigame = minigameGO.GetComponent<MiniGameAbstract>();

        //System.Type a = minigame.GetType();
        //print(a);
    }
	
	// Update is called once per frame
	void Update() {
        // Update time
        timeText.text = "Time left: " + minigame.GetRemainingTime().ToString("f1");

        // Update score
        scoreText.text = "Score: " + minigame.GetCurrentScore().ToString();

        //UpdateQuestion();
	}


    // Update question
    private void UpdateQuestion()
    {   
        questionText.text = minigame.GetQuestion().question;
        // TODO add effect
    }
}

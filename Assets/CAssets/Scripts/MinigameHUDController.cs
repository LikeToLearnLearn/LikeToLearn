using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameHUDController : MonoBehaviour {

    public GameObject minigameGO; // gameobject the minigame script is attached to
    private MiniGameAbstract minigame; // minigame script, subclass of minigameabstract
    private Text timeText;
    private Text scoreText;
    private Text questionText;

    private GameObject timeBar, scoreBar, timeTextGB, scoreTextGB, secondTextGB, HUDQuestionTextGB;
    private GameObject gameoverText;
    private GameObject endScore;
    private GameObject crosshair;

    private string endText;

	// Use this for initialization
	void Start() {
        // Use the gameobjects with these names. They must each have a Text component.
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        questionText = GameObject.Find("HUDQuestionText").GetComponent<Text>();

        gameoverText = GameObject.Find("GameoverText");
        endScore = GameObject.Find("EndScore");
        timeBar = GameObject.Find("TimeBar");
        scoreBar = GameObject.Find("ScoreBar");
        timeTextGB = GameObject.Find("TimeText");
        scoreTextGB = GameObject.Find("ScoreText");
        secondTextGB = GameObject.Find("SecondText");
        HUDQuestionTextGB = GameObject.Find("HUDQuestionText");
        crosshair = GameObject.Find("Crosshair");

        minigame = minigameGO.GetComponent<MiniGameAbstract>();

        GameStart();
        //System.Type a = minigame.GetType();
        //print(a);
    }
	
	// Update is called once per frame
	void Update() {
        // Update time
        timeText.text = "Time left: " + minigame.GetRemainingTime().ToString("f1");

        // Update score
        scoreText.text = "Score: " + minigame.GetCurrentScore().ToString();
        
        if (minigame.GetQuestion() == null)
        {
            questionText.text = "";
        }
        else if (questionText.text != minigame.GetQuestion().GetQuestion())
        {
            UpdateQuestion();
        }
    }


    // Update question
    private void UpdateQuestion()
    {   
	if (minigame.GetQuestion() != null)
	{
		questionText.text = minigame.GetQuestion().GetQuestion();
	}
        // If exception: Add an animator component on questionText and set Controller to "GUIAnimatorController"
        questionText.GetComponent<Animator>().SetTrigger("OnNewQuestion"); 
    }

    public void GameStart()
    {
        timeBar.SetActive(true);
        scoreBar.SetActive(true);
        timeTextGB.SetActive(true);
        scoreTextGB.SetActive(true);
        secondTextGB.SetActive(true);
        HUDQuestionTextGB.SetActive(true);
        gameoverText.SetActive(false);
        endScore.SetActive(false);
        //crosshair.SetActive(false);
    }

    public void GameOver()
    {
        endScore.GetComponent<Text>().text = endText;
        timeBar.SetActive(false);
        scoreBar.SetActive(false);
        timeTextGB.SetActive(false);
        scoreTextGB.SetActive(false);
        secondTextGB.SetActive(false);
        HUDQuestionTextGB.SetActive(false);
        gameoverText.SetActive(true);
        endScore.SetActive(true);
    }

    public void GiveFeedback(string s)
    {
        endScore.SetActive(true);
        endText = s;
        endScore.GetComponent<Text>().text = endText;
        
    }

    public void DeactivateFeedback()
    {
        endScore.SetActive(false);

    }

    public void SetEndText(string s)
    {
        endText = s;
    }

    public void SetCrosshair(bool b)
    {
        crosshair.SetActive(b);
    }
}

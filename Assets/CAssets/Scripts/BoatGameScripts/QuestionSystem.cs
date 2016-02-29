using UnityEngine;
using System.Collections;

public class QuestionSystem : MonoBehaviour {

    public GameObject minigameGO; // gameobject the minigame script is attached to
    private MiniGameAbstract minigame; // minigame script, subclass of minigameabstract

    private AnswerPointTrigger[] aps;
    private Question currentQuestion;
    private int correctAnswers;
    

    // Use this for initialization
    void Start()
    {
        correctAnswers = 0;

        if (minigameGO != null) minigame = minigameGO.GetComponent<MiniGameAbstract>();

        aps = GetComponentsInChildren<AnswerPointTrigger>();

        GetNewQuestion();
    }

    public void GetNewQuestion()
    {
        minigame.CreateQuestion(aps.Length);
        currentQuestion = minigame.GetQuestion();

        //currentQuestion = GameController.control.GetQuestion(aps.Length);

        // Update every answer point
        foreach (AnswerPointTrigger ap in aps)
        {
            ap.SetAnswer(currentQuestion.GetAlternative());
        }
    }


    // Update is called once per frame

    void Update()
    {

    }

    // Puts answer into Question and returns if answer was correct or not, 
    // second parameter to update question (true) or not (false) after a guess
    public bool Guess(string a)
    {
        return Guess(a, false);
    }

    public bool Guess(string a, bool updateQ)
    {
        currentQuestion.Answer(a);
        bool correct = currentQuestion.IsCorrect();

        if (minigame != null) MinigameUpdate(correct);

        if (updateQ)
        {
            GetNewQuestion();
        }

        return correct;
    }

    private void MinigameUpdate(bool correct)
    {
        if (correct)
        {
            correctAnswers++;
            minigame.AddScore(UnityEngine.Random.Range(0, 5) + 10);
            minigame.AddTime(10);   
        }
        else
        {
            minigame.AddScore(-(UnityEngine.Random.Range(0, 5) + 2));
        }
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }



}

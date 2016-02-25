using UnityEngine;
using System.Collections;

public class QuestionPoint : MonoBehaviour {
 
    private BoatGame boatgame;
    private BoatGameHUDController boatgameHUD;
    private AnswerPoint[] aps;

    private bool rewarded;
    private bool answered;

    private int[] question; // Current question as array, index 0 is a number, 1 is an operator, 2 is a number, 3 an operator etc.
    private string questionAsString; // Current question in string format
    private string questionWAnswer;
    private float correctAnswer;
    private float playersAnswer;
    private string difficulty;
    private int difficultyAsInt;

    // Use this for initialization
    void Start()
    {
        boatgame = GameObject.Find("BoatGame").GetComponent<BoatGame>();
        boatgameHUD = GameObject.Find("MinigameHUDCanvas").GetComponent<BoatGameHUDController>();
        aps = GetComponentsInChildren<AnswerPoint>();

        // The name of the object this script is attached to is expected to have the difficulty seperated by space.
        // Like so: "[anything] [difficulty]". E.g: "QuestionPoint Medium".
        string[] splits = this.gameObject.name.Split(' ');
        difficulty = splits[1];
        if (difficulty.Equals("Easy"))
        {
            difficultyAsInt = 1;
        }
        else if (difficulty.Equals("Medium"))
        {
            difficultyAsInt = 2;
        }
        else if (difficulty.Equals("Hard"))
        {
            difficultyAsInt = 3;
        }

        UpdateQuestion();
    }

    // Update is called once per frame

    void Update()
    {


    }

    public bool AnswerQuestion(float guess)
    {
        bool correct = false; // bool returned; true if correct guess, false if wrong guess

        if (!answered)
        {
            answered = true; // flag to only allow answering once per question

            if (guess == correctAnswer)
            {
                boatgame.AnsweredCorrect(difficultyAsInt);


                rewarded = true;
                correct = true;
            }
            else
            {
                boatgame.AnsweredFalse(difficultyAsInt);
            }
        }

        return correct;
    }


    public void UpdateQuestion()
    {
        questionAsString = "";
        if (difficulty.Equals("Easy"))
        {
            question = MathQGenerator.GenerateIntMultiplicationQ(1, 1, 9); //1 operation, numbers 1 to 9
            correctAnswer = question[0] * question[2];
            questionAsString = question[0] + " * " + question[2] + " = ?";

            questionWAnswer = question[0] + " * " + question[2] + " = " + correctAnswer;
        }
        else if (difficulty.Equals("Medium")) {
            question = MathQGenerator.GenerateIntMultiplicationQ(2, 1, 9); //2 operations, numbers 1 to 9
            correctAnswer = question[0] * question[2] * question[4];
            questionAsString = question[0] + " * " + question[2] + " * " + question[4] + " = ?";
        }
        else if (difficulty.Equals("Hard")) {
            question = MathQGenerator.GenerateIntMultiplicationQ(3, 1, 9); //3 random operations, numbers 1 to 9
            correctAnswer = question[0] * question[2] * question[4] * question[6];
            questionAsString = question[0] + " * " + question[2] + " * " + question[4] + " * " + question[6] + " = ?";

        }
        GetComponentInChildren<TextMesh>().text = questionAsString;

        // Randomize answers
        int correctPosition = Random.Range(0, aps.Length); // randomize which index will get correct answer
        for (int i = 0; i < aps.Length; i++)
        {
            if (i == correctPosition)
            {
                aps[i].SetCurrentAnswer(correctAnswer);
            }
            else
            {
                // Randomize false answer
                while (aps[i].GetCurrentAnswer() == correctAnswer || aps[i].GetCurrentAnswer() == 0)
                {
                    aps[i].SetCurrentAnswer(correctAnswer + (int)Random.Range(-correctAnswer * 1f, correctAnswer * 1f));
                    if (correctAnswer == 0)
                    {
                        aps[i].SetCurrentAnswer(Random.Range(-aps.Length, aps.Length));
                    }
                }
            }
            // Update text with an answer
            aps[i].GetComponentInChildren<TextMesh>().text = aps[i].GetCurrentAnswer().ToString();

        }
        rewarded = false;
        answered = false;
    }

    public float GetCorrectAnswer() {
        return correctAnswer;
    }

    public string GetQuestionAsString()
    {
        return questionAsString;
    }


}

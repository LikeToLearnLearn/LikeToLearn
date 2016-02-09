using UnityEngine;
using System.Collections;

public class QuestionPoint : MonoBehaviour {

    public float timeBetweenAnsweredQ;

    private float timeSinceAnswer;

    private BoatGame boatgame;
    private AnswerPoint[] aps;

    private bool rewarded;
    private bool answered;
    private float correctAnswer;
    private float playersAnswer;
    private string difficulty;
    private int difficultyAsInt;

    // Use this for initialization
    void Start ()
    {
        boatgame = GameObject.Find("BoatGame").GetComponent<BoatGame>();
        aps = GetComponentsInChildren<AnswerPoint>();
        timeBetweenAnsweredQ = 5f;

        //The name of the object this script is attached to is expected to have the difficulty seperated by space.
        //Like so: "[anything] [difficulty]". E.g: "QuestionPoint Medium".
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
    
    void Update ()
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
        if (difficulty.Equals("Easy"))
        {
            int[] q = MathQGenerator.GenerateIntMultiplicationQ(1, 1, 9); //1 operation, numbers 1 to 9
            correctAnswer = q[0] * q[2];
            GetComponentInChildren<TextMesh>().text = q[0] + " x " + q[2] + " = ?";
        }
        else if (difficulty.Equals("Medium")) {
            int[] q = MathQGenerator.GenerateIntMultiplicationQ(2, 1, 9); //2 operations, numbers 1 to 9
            correctAnswer = q[0] * q[2] * q[4];
            GetComponentInChildren<TextMesh>().text = q[0] + " x " + q[2] + " x " + q[4] + " = ?";
        }
        else if (difficulty.Equals("Hard")) {
            int[] q = MathQGenerator.GenerateIntMultiplicationQ(3, 1, 9); //3 random operations, numbers 1 to 9
            correctAnswer = q[0] * q[2] * q[4] * q[6];
            GetComponentInChildren<TextMesh>().text = q[0] + " x " + q[2] + " x " + q[4] + " x " + q[6] + " = ?";

        }

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


}

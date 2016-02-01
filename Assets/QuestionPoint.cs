using UnityEngine;
using System.Collections;

public class QuestionPoint : MonoBehaviour {

    public float timeBetweenAnsweredQ;

    private float timeSinceAnswer;

    private AnswerPoint[] aps;

    private bool rewarded;
    private bool answered;
    private float correctAnswer;
    private float playersAnswer;

    // Use this for initialization
    void Start ()
    {
        aps = GetComponentsInChildren<AnswerPoint>();
        timeBetweenAnsweredQ = 5f;

        UpdateQuestion();
    }

    // Update is called once per frame

    //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    void Update ()
    {
        timeSinceAnswer += Time.deltaTime;
        foreach (AnswerPoint ap in aps)
        {
            if (ap.GetTriggered() == true)
            {
                answered = true;
                print("ANSWERED");


                if (ap.GetCurrentAnswer() == correctAnswer && !rewarded)
                {
                    // reward PLAYER
                    //FIX This can run twice on one question NEEDS DELAY

                    
                    print("REWARD");

                    rewarded = true;
                }
                else if (ap.GetCurrentAnswer() != correctAnswer)
                {
                    print("WRONG");
                    TriggerWait();
                    UpdateQuestion();
                }

                if (rewarded)
                {
                    TriggerWait();
                    UpdateQuestion();
                }

            }
        }
	}
    IEnumerator TriggerWait()
    {
        foreach (AnswerPoint ap in aps)
        {
            ap.GetComponentInChildren<AnswerPoint>().enabled = false;
        }
            yield return new WaitForSeconds(5);
        foreach (AnswerPoint ap in aps)
        {
            ap.GetComponentInChildren<AnswerPoint>().enabled = true;
        }
    }

    private void UpdateQuestion()
    {
        int[] q = MathQGenerator.GenerateIntMultiplicationQ(1, 0, 9); //1 operation, numbers 0 to 9
        correctAnswer = q[0] * q[2];
        GetComponentInChildren<TextMesh>().text = q[0] + " x " + q[2] + " = ?";


        int correctPosition = Random.Range(0, aps.Length);
        for (int i = 0; i < aps.Length; i++)
        {
            if (i == correctPosition)
            {
                aps[i].SetCurrentAnswer(correctAnswer);
            }
            else
            {
                aps[i].SetCurrentAnswer(correctAnswer + (int) Random.Range(-correctAnswer*0.3f, correctAnswer*0.3f));
                if (correctAnswer == 0)
                {
                    aps[i].SetCurrentAnswer(Random.Range(-aps.Length, aps.Length));
                }
            }
            aps[i].SetTriggered(false);
            aps[i].GetComponentInChildren<TextMesh>().text = aps[i].GetCurrentAnswer().ToString();


            timeSinceAnswer = 0f;
            rewarded = false;
            
        }
    }

}

using UnityEngine;
using System.Collections;


// An answer point in a question system.
// Informs question system when activated.
public class AnswerPointTrigger : MonoBehaviour {

    private QuestionSystem questionSystem; // script on parent gameobject, has question system script to oversee the answer points
    public GameObject answerText; // text gameobject answer/guess shows on, must have MeshRenderer and TextMesh

    private bool triggered; // flag to make sure only trigger once
    private string answer;


    // Use this for initialization
    void Start()
    {
        questionSystem = GetComponentInParent<QuestionSystem>();
        
        // Got script order problems using this line. Question system wanted to set answers faster than answerpoint found its text.
        //answerText = this.gameObject.GetComponentsInChildren<Transform>()[1].gameObject; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {

        if (c.tag.Equals("PlayerBoat") && !triggered)
        {
            SetTriggered(true);
            StartCoroutine(AnswerAndDelay());
        }
    }

    
    public void SetTriggered(bool b)
    {
        triggered = b;
    }

    public void SetAnswer(string a)
    {
        this.answer = a;
        answerText.GetComponentInChildren<TextMesh>().text = a;
    }


    private IEnumerator AnswerAndDelay()
    {
        // Save original text color
        Color oldcolor = answerText.GetComponent<TextMesh>().color;

        // Flash three times
        answerText.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        answerText.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        answerText.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        answerText.GetComponent<MeshRenderer>().enabled = true;

        // Change color to green on correct, red on false
        bool guessCorrect = questionSystem.Guess(answer);
        if (guessCorrect)
        {
            Color c = new Color(0, 255, 0);
            answerText.GetComponent<TextMesh>().color = c;
                
        }
        else
        {
            Color c = new Color(255, 0, 0);
            answerText.GetComponent<TextMesh>().color = c;
        }

        yield return new WaitForSeconds(2f); // 2 second delay

        questionSystem.GetNewQuestion();
        
        answerText.GetComponent<TextMesh>().color = oldcolor;
        SetTriggered(false);
    }

}

using UnityEngine;
using System.Collections;

public class AnswerPoint : MonoBehaviour
{

    private float currentAnswer;
    private bool triggered;
    private QuestionPoint qp;

    // Use this for initialization
    void Start()
    {
        qp = this.gameObject.GetComponentInParent<QuestionPoint>();
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

    private IEnumerator AnswerAndDelay()
    {
        Color oldcolor = GetComponentInChildren<TextMesh>().color;

        GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<MeshRenderer>().enabled = true;
        bool ans = qp.AnswerQuestion(currentAnswer);
        if (ans)
        {
            Color c = new Color(0, 255, 0);
            GetComponentInChildren<TextMesh>().color = c;
                
        }
        else
        {
            Color c = new Color(255, 0, 0);
            GetComponentInChildren<TextMesh>().color = c;
        }

        yield return new WaitForSeconds(8f); // 8 second delay
        qp.UpdateQuestion();
        GetComponentInChildren<TextMesh>().color = oldcolor;
        SetTriggered(false);
    }

    public void SetTriggered(bool b)
    {
        triggered = b;
    }

    public bool GetTriggered()
    {
        return triggered;
    }

    public void SetCurrentAnswer(float ans)
    {
        currentAnswer = ans;
    }
    public float GetCurrentAnswer()
    {
        return currentAnswer;
    }
}

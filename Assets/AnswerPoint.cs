using UnityEngine;
using System.Collections;

public class AnswerPoint : MonoBehaviour
{

    private float currentAnswer;
    private bool triggered;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag.Equals("PlayerBoat"))
        {
            SetTriggered(true);
        }
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

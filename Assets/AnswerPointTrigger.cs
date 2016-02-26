using UnityEngine;
using System.Collections;


// TO BE new answer ring trigger
public class AnswerPointTrigger : MonoBehaviour {
    public GameObject minigameGO; // gameobject the minigame script is attached to
    private MiniGameAbstract minigame; // minigame script, subclass of minigameabstract
    
    private bool triggered;


    // Use this for initialization
    void Start()
    {

        minigame = minigameGO.GetComponent<MiniGameAbstract>();
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
        bool ans = minigame.GetQuestion().IsCorrect();
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

        yield return new WaitForSeconds(2f); // 2 second delay
        //minigame.SetUpQuestionAndAnswers(); FIXFIXIFIXFIX
        GetComponentInChildren<TextMesh>().color = oldcolor;
        SetTriggered(false);
    }

    public void SetTriggered(bool b)
    {
        triggered = b;
    }
}

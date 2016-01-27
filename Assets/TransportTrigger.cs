using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransportTrigger : MonoBehaviour
{
    // 200x300 px window will apear in the center of the screen.
    private Rect windowRect = new Rect((Screen.width - 200) / 2, (Screen.height - 300) / 2, 200, 300);
    // Only show it if needed.
    private bool show = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "Transport");
    }

    // This is the actual window.
    void DialogWindow(int windowID)
    {
        float y = 20;
        GUI.Label(new Rect(5, y, windowRect.width, 20), "Where do you want to go?");

        if (GUI.Button(new Rect(5, y+20, windowRect.width - 10, 20), "Racingisland"))
        {
            SceneManager.LoadScene("game_racingisland");
            show = false;
        }

        if (GUI.Button(new Rect(5, y+40, windowRect.width - 10, 20), "Exit"))
        {
            Application.Quit();
            show = false;
        }
    }

    // To open the dialogue from outside of the script.
    public void Open()
    {
        show = true;
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("collision det");
        if (c.tag.Equals("Player"))
        {

            Debug.Log("collision det with player");
            show = true;
        }

    }
}
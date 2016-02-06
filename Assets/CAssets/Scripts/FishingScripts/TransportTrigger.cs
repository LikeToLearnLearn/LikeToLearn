using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransportTrigger : MonoBehaviour
{
    private static int windowWidth = Screen.width / 3;
    private static int windowHeight = Screen.height / 2;

    private Rect windowRect = new Rect(Screen.width / 3, Screen.height / 4, windowWidth, windowHeight);
    // Only show it if needed.
    private bool show = false;
    private GameObject sceneHandler;
    private SceneHandler sh;

    // Use this for initialization
    void Start()
    {
        sceneHandler = GameObject.Find("SceneHandlerO");
        sh = sceneHandler.GetComponent<SceneHandler>();
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
        GUI.Label(new Rect(5, y, windowRect.width, Screen.height / 20), "Where do you want to go?");

        if (GUI.Button(new Rect(5, y + 20, windowRect.width - 10, 20), "Centralisland"))
        {
            sh.changeScene("game_fishingscene", "city_centralisland");
            show = false;
        }

        if (GUI.Button(new Rect(5, y+40, windowRect.width - 10, 20), "Racingisland"))
        {
            sh.changeScene("game_fishingscene", "game_racingisland");
            show = false;
        }
        if (GUI.Button(new Rect(5, y+60, windowRect.width - 10, 20), "Goldisland"))
        {
            sh.changeScene("game_fishingscene", "game_goldisland");
            show = false;
        }
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

    void OnTriggerExit(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            Application.Quit();
            show = false;
        }
    }
}
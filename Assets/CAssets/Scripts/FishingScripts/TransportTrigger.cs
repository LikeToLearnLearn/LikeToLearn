using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class TransportTrigger : MonoBehaviour
{
    private GameObject sceneHandler;
    private SceneHandler sh;
    private GameObject tc;
    private int worldLevel;

    // Use this for initialization
    void Start()
    {
        sceneHandler = GameObject.Find("SceneHandlerO");
        sh = sceneHandler.GetComponent<SceneHandler>();

        tc = GameObject.Find("TransportCanvas");
        tc.SetActive(false);

        //original code
        //worldLevel = GameController.control.unlockedWorldLevel;

        //cheat to reach the highest worldlevel
        worldLevel = 3;
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("collision det");
        if (c.tag.Equals("Player"))
        {

            Debug.Log("collision det with player");
            //show = true;
            tc.SetActive(true);
        }

    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag.Equals("Player"))
        {
            tc.SetActive(false);
        }
    }
    
    public void SetDestination(String s)
    {
        if (s == "game_goldisland" && worldLevel < 2)
            return;
        else if (s == "game_racingisland" && worldLevel < 3)
            return;
        else
            sh.ChangeScene("game_fishingscene", s);
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    GameObject sco, iho, sho;
    SceneHandler sc;

	public GameObject cam;
	public GameObject newGame, loadGame, options, credits;

	// Use this for initialization
	void Start () {
        sco = GameObject.Find("SceneHandlerO");
        sc = sco.GetComponent<SceneHandler>();
        iho = GameObject.Find("InventoryHandlerO");
        sho = GameObject.Find("SaveHandlerO");
	}

	float zoom = 0.0f;

	// Update is called once per frame
	void Update () {
		if (zoom > 0.0f) {
			float move = Mathf.Log(zoom);
			cam.transform.Translate(new Vector3(0.0f, 0.0f, move));
			zoom -= move;
		} else if (zoom < 0.0f) {
			float move = Mathf.Log(-zoom);
			cam.transform.Translate(new Vector3(0.0f, 0.0f, -move));
			zoom += move;
		}
	}

	public void NewGameButtonEvent()
	{
		zoom = 240.0f;
		newGame.SetActive(true);
	}

    public void LoadGameButtonEvent()
    {
		zoom = 240.0f;
		loadGame.SetActive(true);
    }

	public void OptionsButtonEvent()
	{
		zoom = 240.0f;
		options.SetActive(true);
	}

	public void CreditsButtonEvent()
	{
		zoom = 240.0f;
		credits.SetActive(true);
	}

	public void BackToMainMenu() {
		zoom = -240.0f;
		newGame.SetActive(false);
		loadGame.SetActive(false);
		options.SetActive(false);
		credits.SetActive(false);
	}

	public void StartGame() {
		sc.changeScene("new", "city_centralisland");
		SceneManager.LoadScene("city_centralisland");
	}

}

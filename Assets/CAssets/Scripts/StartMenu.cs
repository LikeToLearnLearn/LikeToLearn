using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour {
	public GameObject cam;

	// pseudo pannels
	public GameObject newGame, loadGame, options, credits;

	// new game
	public GameObject takenNameText, invalidNameText, nameInput;

	// load game
	public GameObject loadPannelButton, nextButton, prevButton, nameText;

	// credits
	public GameObject creditsText;

	float zoom = 0.0f;
	bool rollCredits = false;
	bool isZoomed = false;
	int nameIndex = 0;
	SceneHandler sceneHandler;
	List<string> names;
	Text nameToLoad;

	void Start () {
        GameObject sco = GameObject.Find("SceneHandlerO");
        sceneHandler = sco.GetComponent<SceneHandler>();
		loadPannelButton.SetActive(GameController.control.GotSavedGames());
		names = GameController.control.GetNames();
		nameToLoad = nameText.GetComponent<Text>();
	}

	void Update () {
		if (isZoomed) {
			if (zoom > 0.0f) {
				float move = Mathf.Log(zoom);
				cam.transform.Translate(new Vector3(0.0f, 0.0f, move));
				zoom -= move;
			} else {
				isZoomed = false;
			}
		} else {
			if (zoom < 0.0f) {
				float move = Mathf.Log(-zoom);
				cam.transform.Translate(new Vector3(0.0f, 0.0f, -move));
				zoom += move;
			} else {
				isZoomed = true;
			}
		}
		if (rollCredits) {
			creditsText.transform.Translate(new Vector3(0.0f, 0.09f, 0.0f));
		}
	}

	public void NewGameButtonEvent()
	{
		takenNameText.SetActive(false);
		invalidNameText.SetActive(false);
		if (isZoomed) return;
		zoom += 240.0f;
		isZoomed = true;
		newGame.SetActive(true);
	}

    public void LoadGameButtonEvent()
    {
		if (isZoomed) return;
		zoom += 240.0f;
		isZoomed = true;
		nameToLoad.text = names[nameIndex];
		loadGame.SetActive(true);
    }

	public void NextNameButton()
	{
		nameIndex = (nameIndex + 1) % names.Count;
		nameToLoad.text = names[nameIndex];
	}

	public void PrevNameButton()
	{
		nameIndex = (nameIndex < 1 ? names.Count : nameIndex) - 1;
		nameToLoad.text = names[nameIndex];
	}

	public void OptionsButtonEvent()
	{
		if (isZoomed) return;
		zoom += 240.0f;
		isZoomed = true;
		options.SetActive(true);
	}

	public void CreditsButtonEvent()
	{
		if (isZoomed) return;
		zoom += 240.0f;
		isZoomed = true;
		rollCredits = true;
		credits.SetActive(true);
	}

	public void BackToMainMenu() {
		if (!isZoomed) return;
		zoom -= 240.0f;
		isZoomed = false;
		rollCredits = false;
		newGame.SetActive(false);
		loadGame.SetActive(false);
		options.SetActive(false);
		credits.SetActive(false);
	}

	public void StartGame() {
		var input = nameInput.GetComponent<InputField>();
		GameController.control.name = input.text;
		if (GameController.control.NameTaken()) {
			takenNameText.SetActive(true);
		} else if (GameController.control.NameInvalid()) {
			invalidNameText.SetActive(true);
		} else {
			takenNameText.SetActive(false);
			invalidNameText.SetActive(false);
			GameController.control.NewGame();
			sceneHandler.changeScene("new", "city_centralisland");
		}
	}

	public void LoadGame() {
		GameController.control.name = names[nameIndex];
		GameController.control.LoadGame();
		// todo: save last location and reload it?
		sceneHandler.changeScene("new", "city_centralisland");
	}
}

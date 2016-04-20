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
	public GameObject takenNameText, invalidNameText, nameInput, passwordInput, onlinesign;

	// load game
	public GameObject loadPannelButton, nextButton, prevButton, nameText;

	// credits
	public GameObject creditsText;

	float zoom = 0.0f;
	bool rollCredits = false;
	bool isZoomed = false;
	int nameIndex = 0;
	List<string> names;
	Text nameToLoad;

    void Start()
    {
        GameController.control.recive.checkOnline();

        if (!GameController.control.gotSavedGames)
        {
            Button b = loadPannelButton.GetComponent<Button>();
            b.interactable = false;
            // fixme: decrease load game button text alpha
        }
        names = GameController.control.GetNames();
        nameToLoad = nameText.GetComponent<Text>();

        
        
    }

    void Update () 
    {

        if (GameController.control.recive.Online())
        {
            //bool online = onlinesign.GetComponent<Text>();
            onlinesign.GetComponent<Text>().text = "online";
        }
        else
        {
            print(""); // Johans bidrag :)
            onlinesign.GetComponent<Text>().text = "offline";
        }
        Debug.Log(" Online i StartMeny = " + GameController.control.recive.Online());

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

        GameController.control.recive.authentication();
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

	public void BackToMainMenu()
	{
		if (!isZoomed) return;
		zoom -= 240.0f;
		isZoomed = false;
		rollCredits = false;
		newGame.SetActive(false);
		loadGame.SetActive(false);
		options.SetActive(false);
		credits.SetActive(false);
	}

	public void StartGame()
	{
		var input = nameInput.GetComponent<InputField>();
        var passwordinput  = passwordInput.GetComponent<InputField>();
        string name = input.text;
        string password = passwordinput.text;

		if (GameController.control.NameTaken(name)) {
			takenNameText.SetActive(true);
			invalidNameText.SetActive(false);
		} else if (GameController.control.NameInvalid(name)) {
			invalidNameText.SetActive(true);
			takenNameText.SetActive(false);
		} else {
			takenNameText.SetActive(false);
			invalidNameText.SetActive(false);
			GameController.control.NewGame(name, password);
		}
	}

	public void LoadGame()
	{
		GameController.control.LoadGame(names[nameIndex]);
	}
}

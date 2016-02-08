using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStatistics : MonoBehaviour {


	int[,] tableArray; //Which problems the player has completed

	Dictionary<string, int> highscores;

	// Use this for initialization
	void Start () {
		
		tableArray = new int[10, 10];
		highscores = new Dictionary<string, int>() {
			{ "RacingGame", 0 },
			{ "BoatGame", 0 },
			{ "FishGame", 0 }
		};
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateTableArray(int number1, int number2){
		tableArray [number1, number2]++;
	}

	public void updateHighscore(string minigame, int newScore){
		if (highscores [minigame] < newScore) {
			highscores [minigame] = newScore;
		}
	}

	public bool rowCompleted(int row){
		bool completed = true;
		for(int col = 0; col <= tableArray.Length; col++) {
			if (tableArray[row, col] == 0) {
				completed = false;
			}
		}

		return completed;
	}

	public bool tableCompleted(){
		bool completed = true;
		foreach (int i in tableArray) {
			if (i == 0) {
				completed = false;
			}
		}

		return completed;
	}
}

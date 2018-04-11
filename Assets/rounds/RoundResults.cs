using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundResults : MonoBehaviour {

	// Actual game object GameManager
	GameObject GM;
	public GameObject playerScorePrefab;
	GameObject[] playerScorePrefabClones = new GameObject[4];

	private int[] scoreKeys;
	private int[] indexValues;
	private int topScore;


	[SerializeField]
	private TextMeshProUGUI roundResultsText = null;
	[SerializeField]
	private TextMeshProUGUI headingText = null;

	// Script attached to GameManager
	private GameManager GMScript;

	// Use this for initialization
	void Awake () {
		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();
	}

	void Start() {
		// Debug.Log("It is round " + GMScript.currentRound + " of " + GMScript.totalRounds);
		if (GMScript.currentRound == GMScript.totalRounds) {
			headingText.text = "Game Results";
			roundResultsText.text = "";
            GameObject.Find("Continue Button").SetActive(false);
            GameObject.Find("Main Menu Button").SetActive(true);
            GameObject.Find("Play Again Button").SetActive(true);
        }
		else {
			roundResultsText.text = "Round " + GMScript.currentRound + " of " + GMScript.totalRounds;
            GameObject.Find("Continue Button").SetActive(true);
            GameObject.Find("Main Menu Button").SetActive(false);
            GameObject.Find("Play Again Button").SetActive(false);
        }
		
		scoreKeys = new int[GMScript.numPlayers];		// stores scores and act as keys for sort()
		indexValues = new int[GMScript.numPlayers];		// stores indices and act as values for sort()
		TextMeshProUGUI[] allTextMeshPros;

		// Sort indexValues in order of score, ascending
		for (int i = 0; i < GMScript.numPlayers; i++) {
			scoreKeys[i] = GMScript.tankInfos[i].score;
			indexValues[i] = i;
		}
		Array.Sort(scoreKeys, indexValues);
		topScore = scoreKeys[GMScript.numPlayers - 1];
		
		for (int i = 0; i < GMScript.numPlayers; i++) {
			playerScorePrefabClones[i] = Instantiate(playerScorePrefab,new Vector3(0,120 - i*84,0),Quaternion.identity);
			playerScorePrefabClones[i].transform.SetParent(this.transform, false);
			playerScorePrefabClones[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,100 - i*70);

			foreach (Image img in playerScorePrefabClones [i].GetComponentsInChildren<Image> ()) {

				if (img.name.Equals("Tank"))
					img.color = GMScript.tankInfos[indexValues[GMScript.numPlayers - 1 - i]].GetComponent<SpriteRenderer>().color;
				
			}//..GetComponent<Image>().color = GMScript.tankInfos[i].GetComponent<SpriteRenderer>().color;

			allTextMeshPros = playerScorePrefabClones[i].GetComponentsInChildren<TextMeshProUGUI>();
			foreach (TextMeshProUGUI tempText in allTextMeshPros) {
				if (tempText.name == "Score") {
					tempText.text = scoreKeys[GMScript.numPlayers - 1 - i].ToString();
				}
				if (tempText.name == "PlayerName") {
					tempText.text = GMScript.tankInfos[indexValues[GMScript.numPlayers - 1 - i]].GetComponent<tankInfo>().name;
				}
			}

			if (scoreKeys[GMScript.numPlayers - 1 - i] < topScore) {
				playerScorePrefabClones[i].GetComponent<RectTransform>().localScale = new Vector3(0.65f, 0.65f, 1.0f);
			}
            else
            {
                playerScorePrefabClones[i].GetComponent<RectTransform>().localScale = new Vector3(0.85f, 0.85f, 1.0f);
            }

		}

	}

}

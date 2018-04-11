using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameHUD : MonoBehaviour {
    private GameManager GM;
    private TMP_Text playerName;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerName = GameObject.Find("Player Name HUD").GetComponentInChildren<TMP_Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GM == null)
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (playerName == null)
        {
            playerName = GameObject.Find("Player Name HUD").GetComponentInChildren<TMP_Text>();
        }
        else
        {
            playerName.text = GM.tankInfos[GM.playerTurn].name;
        }
	}
}

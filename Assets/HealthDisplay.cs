using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour {

    private GameManager GM;
    private TMP_Text currentHealth;
    private TMP_Text maxHealth;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = GameObject.Find("Current Health").GetComponent<TextMeshProUGUI>();
        maxHealth = GameObject.Find("Max Health").GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentHealth == null || maxHealth == null)
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            currentHealth = GameObject.Find("Current Health").GetComponent<TextMeshProUGUI>();
            maxHealth = GameObject.Find("Max Health").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            currentHealth.text = GM.tankInfos[GM.playerTurn].health < 0 ? "0" : GM.tankInfos[GM.playerTurn].health.ToString();
            maxHealth.text = GM.tankInfos[GM.playerTurn].maxHealth.ToString();
        }
    }
}

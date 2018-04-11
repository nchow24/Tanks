using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandingsButtonTriggers : MonoBehaviour {

    GameObject GM;
    private GameManager GMScript;

    private void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
    }

    public void mainMenuTriggered()
    {
        Debug.Log("Main Menu button pressed");
        GMScript.currentRound = 1;
        GMScript.mapInstantiated = false;
        GMScript.launchedFromMenu = false;
        GMScript.tanksInit = false;
        GMScript.DeleteTanks();
        SceneManager.LoadScene("Main Menu");
    }

    public void continueTriggered()
    {
        Debug.Log("Continue button pressed");
        GMScript.currentRound++;
        Debug.Log(GMScript.currentRound);
        SceneManager.LoadScene("Shop");
    }

    public void playAgainTriggered()
    {
        Debug.Log("Play Again button pressed");
        resetSettings();
        SceneManager.LoadScene("GameWinston");
    }

    void nextRoundSettings()
    {
        Debug.Log("Need to implement changing settings for next round");
        GMScript.currentRound++;
    }

    void resetSettings()
    {
        Debug.Log("StandingsButtonTrigger.resetSettings() triggered");
        GMScript.playAgain();
    }
}

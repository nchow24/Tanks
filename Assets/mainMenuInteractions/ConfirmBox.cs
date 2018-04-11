using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmBox : MonoBehaviour {
	private GameManager GM;
    private GameObject confirmBoxPanel;
    private Animator boxAnim;
    
	void Start () {
        Time.timeScale = 1;
		GM = GameObject.Find ("GameManager").GetComponent<GameManager>();
        confirmBoxPanel = GameObject.Find("Confirm Box");
        boxAnim = confirmBoxPanel.GetComponent<Animator>();
        boxAnim.SetBool("IsDisplayed", false);
	}

    public void playIsPressed() {
		boxAnim.SetBool("IsDisplayed", true);
    }
    
    public void goBack() {
        boxAnim.SetBool("IsDisplayed", false);
    }

    public void startGame()
    {
        GM.gameSetup();
        SceneManager.LoadScene("GameWinston");
    }
}
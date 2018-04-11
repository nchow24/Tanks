using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KeyPress : MonoBehaviour {

    private TMP_Text pressAnyKey;
    private AudioSource opening;

	// Use this for initialization
	void Start () {
        pressAnyKey = GameObject.Find("Press any key").GetComponent<TMP_Text>();
        opening = GameObject.Find("Opening").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Paused Menu(Clone)") == null) {
			loadScene();
        }
    }

    void loadScene() {
        opening.Stop();
        GetComponent<AudioSource>().Play();
        pressAnyKey.color = Color.white;
        StartCoroutine(onKeyPressDelay());
    }

    IEnumerator onKeyPressDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("mainMenuInteractions/Main Menu");
    }
}

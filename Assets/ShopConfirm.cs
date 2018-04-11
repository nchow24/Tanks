using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopConfirm : MonoBehaviour {

    private GameObject GM;
    private GameManager GMScript;
    private bool lastplayer;
	private ShopController shopControllerScript;

    // Go Back Button Pressed
	public void OnBack()
    {
        // Destroy confirm box
        Destroy(gameObject);
    }

    // Yes Button Pressed
    public void OnYes()
	{
		shopControllerScript = GameObject.Find ("ShopController").GetComponent<ShopController> ();
		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();

		Debug.Log ("ShopConfirm.cs  You saved changes.");
		shopControllerScript.saveChanges ();
  
		if (shopControllerScript.isMorePlayers ()) {
			Debug.Log ("ShopConfirm.cs :  Setting up next player");
			shopControllerScript.setNextPlayer ();
		} else {
			Debug.Log ("ShopConfirm.cs : Loading next scene");
//			GMScript.initRound ();
			SceneManager.LoadScene ("GameWinston");
		}

		Destroy(gameObject);
    }

	public void onClear() {
		shopControllerScript = GameObject.Find("ShopController").GetComponent<ShopController>();

		shopControllerScript.onClearCart ();

		Destroy(gameObject);

	}
}

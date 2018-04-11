using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTanks : MonoBehaviour {

	GameObject GM;
	private GameManager GMScript;
    private MenuHandler menu;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();

        // Test data for when you want to run straight from GameWinston
        string[] testNames = { "Player 1", "Player 2", "Player 3" };
		bool[] bots = new bool[] {false, false, false, false};
        Color32[] testColours = { new Color32(64, 128, 192, 255), new Color32(155, 159, 200, 255), new Color32(21, 243, 94, 255) };

		if (!GMScript.tanksInit && GMScript.launchedFromMenu)
        {
			GMScript.initTanks(GMScript.names, bots, GMScript.colours);
        }
        else if (!GMScript.tanksInit)
        {
            GMScript.initTanks(testNames, bots, testColours);
        }
		GMScript.initRound ();
	}
	
	// Update is called once per frame
	void Update () {
		if(GMScript.tanksLeft > 0)
			GMScript.updateTank ();
		// Temporary code to emulate shooting
		//if (Input.GetKeyDown (KeyCode.Space))
		//	GMScript.nextTurn ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NumberRounds : MonoBehaviour { 
	GameManager GM;

	[SerializeField]
    private static int rounds;
    private Text roundsLabel;
    
    void Start () {
        // Default number of rounds to 3
        rounds = 3;
        roundsLabel = GameObject.Find("Number of Rounds Text").GetComponent<Text>();

		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		GM.totalRounds = rounds;
	}
	
	void Update () {
		if (rounds == 1) { roundsLabel.text = GM.totalRounds + " ROUND"; }
        else { roundsLabel.text = GM.totalRounds + " ROUNDS"; }
	}

    public void incrementRounds()
    {
        // TODO: Tell user that max is 7 rounds
        if ( rounds == 7 ) {
            return;
        } else {
            rounds = rounds + 2;
			GM.totalRounds = rounds;
        }
    }

    public void decrementRounds()
    {
        // TODO: Tell user that minimum number of rounds is 1
        if ( rounds == 1 ){
            return;
        } else {
            rounds = rounds - 2;
			GM.totalRounds = rounds;
        }
    }

	public static int getRounds(){
		return rounds;
	}
}

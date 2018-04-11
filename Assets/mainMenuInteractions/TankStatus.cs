using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TankStatus: ScriptableObject
 *
 * Handles if the tank selected is a player, CPU, or inactive
 */

[System.Serializable]
public class TankStatus : ScriptableObject {
    private string[] playerType = { "PLAYER", "NONE" };

    private Text playerTypeValue;
    private Image playerTypeBox;
    private int playerIndex;
	private string player;
    
    public string getStatus() { return playerType[playerIndex]; }
	public void setStatus(int index){ playerIndex = index; }

	public void initialize(Text tankText, string playerName)
	{
        playerTypeValue = tankText;
        playerTypeBox = tankText.transform.GetComponentInParent<Image>();
        string currentStatus = playerTypeValue.text;
        playerIndex = Array.IndexOf(playerType, currentStatus);

		player = playerName;
    }

    public void increment()
    {
		playerIndex = (playerIndex + 1) % playerType.Length;
		playerTypeValue.text = playerType [playerIndex];
    }

    public void setPlayerBoxColour()
    {
        switch (playerIndex)
        {
            case 0:
                playerTypeBox.color = new Color32(72, 151, 182, 255);
                break;
            case 1:
                playerTypeBox.color = new Color32(0, 0, 0, 255);
                break;
            default:
                Debug.Log("Game broke, bug happened. Figure out why.");
                break;
        }
    }
}

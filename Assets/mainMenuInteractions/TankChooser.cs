using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* TankChooser : ScriptableObject
 *
 * TankChooser class that handles the choosing of the tank colours
 */

[System.Serializable]
public class TankChooser : ScriptableObject {
    private Color32[] tankColours = {
        new Color32(148, 255, 202, 255),
        new Color32(255, 148, 208, 255),
        new Color32(43, 159, 59, 255),
        new Color32(255, 61, 61, 255)
    };
    private Image tankImage;
    private Image questionMark;
    private string status;
    private int colourIndex;

	public void Update(){
		checkIfActive();
	}

    public void initialize(string player, string stat, Image tank)
    {
        status = stat;
        tankImage = tank;
        questionMark = tank.transform.Find("Question Mark").gameObject.GetComponent<Image>();
        colourIndex = int.Parse(player[player.Length - 1].ToString()) - 1;
        checkIfActive();
    }

    public Color32 getTankColour()
    {
        return tankColours[colourIndex];
    }

    public bool isActive()
    {
        return !(status.Equals("NONE"));
    }

    public void incrementColour()
    {
        if (isActive())
        {
            colourIndex = (colourIndex + 1) % tankColours.Length;
            tankImage.color = tankColours[colourIndex];
        }
    }

    public void decrementColour()
    {
        if (isActive())
        {
            colourIndex = (colourIndex - 1) % tankColours.Length;
            if (colourIndex == -1)
            {
                colourIndex = tankColours.Length - 1;
            }
            tankImage.color = tankColours[colourIndex];
        }
    }

    public void updateTankStatus(string stat)
    {
		status = stat;
        checkIfActive();
    }

    void checkIfActive()
    {
        if (status.Equals("NONE"))
        {
            tankImage.color = Color.black;
            questionMark.enabled = true;
        }
        else
        {
            tankImage.color = tankColours[colourIndex];
            questionMark.enabled = false;
        }
    }
}

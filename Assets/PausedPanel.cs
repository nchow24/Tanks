using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPanel : MonoBehaviour {

	public void onResume()
    {
        Destroy(gameObject);
    }

    public void onQuit()
    {
        Application.Quit();
    }
}

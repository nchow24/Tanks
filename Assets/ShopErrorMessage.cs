using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopErrorMessage : MonoBehaviour {

    /* Uncomment these lines if you want error message to disappear after you move the mouse around
    private int negativeLimit = -1;
    private int positiveLimit = 1;
    

	// Destroy error message if mouse is moved a bit
	void Update () {
        if (Input.GetAxis("Mouse X") < negativeLimit || Input.GetAxis("Mouse X") > positiveLimit || 
            Input.GetAxis("Mouse Y") < negativeLimit || Input.GetAxis("Mouse Y") > positiveLimit)
        {
            Destroy(gameObject);
        }
    }
    */

    // Destroy error message when it is clicked on
    public void onErrorMessageClick()
    {
        Destroy(gameObject);
    }
}

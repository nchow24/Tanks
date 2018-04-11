using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTut : MonoBehaviour {
	
	void Update () {
	    if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
            Destroy(gameObject);
        }
    }
}

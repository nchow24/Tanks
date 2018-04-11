using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkTMP : MonoBehaviour {

	public TMP_Text text;
	public float last, current;
	public bool state, blink;

	void Start()
	{
		state = true;
        blink = true;
		last = Time.time;
		text = GetComponent<TMP_Text> ();
		text.color = Color.white;
	}

	void Update()
	{
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Paused Menu(Clone)") == null) { blink = false; }
        current = Time.time;
		if (current > last + 0.4 && blink == true) {
			state = !state;
			last = current;
			if (text.color == Color.white)
				text.color = Color.clear;
			else
				text.color = Color.white;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wind : MonoBehaviour {

	GameObject GM;
	private GameManager GMScript;
	GameObject windTextObj;
	private TextMeshProUGUI windText;
	GameObject windImageObj;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager");
		windTextObj = GameObject.Find("windText");
		windText = windTextObj.GetComponent<TextMeshProUGUI> ();
		windImageObj = GameObject.Find ("windImage");
		GMScript = GM.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		changeWind ();
	}

	void changeWind(){
		string temp  = Mathf.Round(Mathf.Abs(GMScript.windSpeed)).ToString();
		float angle = windImageObj.transform.eulerAngles.y;
		if (GMScript.windSpeed < 0) {
			if(Mathf.Abs(angle - 0) > 0.1)
				windImageObj.transform.eulerAngles = new Vector3 (0,angle + 5,0);
			temp = temp + " W";
		} else {
			if(Mathf.Abs(angle - 180) > 0.1)
				windImageObj.transform.eulerAngles = new Vector3 (0,angle - 5,0);
			temp = temp + " E";
		}
		windText.text = temp;
	}
}

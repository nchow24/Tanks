using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour {
	[SerializeField]
	private float vertOffset;
	[SerializeField]
	private float horOffset;


		// Use this for initialization
	void Start () {
		Vector3 parent = (this.transform.parent).transform.position;
		Vector3 newPos = new Vector3(parent.x + horOffset, parent.y = vertOffset, 0);
		transform.eulerAngles = new Vector3(0,0,0);
		transform.position = Camera.main.WorldToScreenPoint (newPos);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 parent = (this.transform.parent).transform.position;
		Vector3 newPos = new Vector3(parent.x + horOffset, parent.y + vertOffset, 0);
		//string x = parent.x.ToString() +" "+ parent.y.ToString() + " "+parent.z.ToString();
		//print (x);
		transform.eulerAngles = new Vector3(0,0,0);
		//print (transform.eulerAngles);
		transform.position = Camera.main.WorldToScreenPoint (newPos);
	}
}

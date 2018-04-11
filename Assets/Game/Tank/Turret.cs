using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turret : MonoBehaviour
{
    private GameManager GM;
    public KeyCode pressUp;
    public KeyCode pressDown;
    public float min = 0f;
    public float max = 180f;
    private Image turret;
    private TMP_Text turretAngle;

    // Use this for initialization
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Get new vector
        transform.eulerAngles = new Vector3(0, 0, 359.99f);
        
        // Set indicator to current turret vector direction
        turret = GameObject.Find("Turret Angle").GetComponent<Image>();
        turret.color = GM.tankInfos[GM.playerTurn].colour;
        turret.transform.eulerAngles = new Vector3(0, 0, 360.00f);

        // Set angle indicator text
        turretAngle = GameObject.Find("Turret Angle Text").GetComponent<TMP_Text>();
        turretAngle.text = "180°";
    }

    // Update is called once per frame
    void Update()
    {
        // Find GameObjects if unassigned (new rounds)
        if (GM == null)
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (turret == null)
        {
            turret = GameObject.Find("Turret Angle").GetComponent<Image>();
            turret.color = GM.tankInfos[GM.playerTurn].colour;
            turret.transform.eulerAngles = new Vector3(0, 0, 360.00f);
        }

        if (turretAngle == null)
        {
            turretAngle = GameObject.Find("Turret Angle Text").GetComponent<TMP_Text>();
            turretAngle.text = "180°";
        }

        turret.transform.eulerAngles = GM.tankInfos[GM.playerTurn].armAngle;
        turret.color = GM.tankInfos[GM.playerTurn].colour;

        float rotation = -Input.GetAxis("Vertical");
        if (transform.localEulerAngles.z >= 0f && transform.localEulerAngles.z <= 90f)
            rotation = Mathf.Min(0, rotation);
		if (transform.localEulerAngles.z <= 180f && transform.localEulerAngles.z >= 90f)
            rotation = Mathf.Max(0, rotation);
        
        transform.Rotate(0, 0, rotation);
        turret.transform.Rotate(0, 0, rotation);

        double zValue = (Math.Round(turret.transform.rotation.eulerAngles.z) - 180);
        if (zValue == -180) { zValue = 180; }
        turretAngle.text = zValue.ToString() + "°";
        //transform.eulerAngles.z = Mathf.Clamp (transform.eulerAngles.z, min, max);
        //if (Input.GetKeyDown (pressUp))
        //	transform.Rotate(0,0,10);
        //if (Input.GetKeyDown (pressDown))
        //	transform.eulerAngles = new Vector3 (0, 0, 0);

    }

}
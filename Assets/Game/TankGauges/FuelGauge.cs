using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelGauge : MonoBehaviour {
    private GameManager GM;
    private RawImage fuelTank;
    private TMP_Text fuelDisplay;
    [SerializeField]
	private float gasAmount;
    [SerializeField]
    private float maxFuel;
    [SerializeField]
    private float fuelEfficiency = 0.05f;
	
	public void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        fuelTank = GameObject.Find("Fuel Gauge Image").GetComponent<RawImage>();
        fuelDisplay = GameObject.Find("Fuel Gauge Text").GetComponent<TMP_Text>();
    }
	
	public void Update()
    {
        if (GM == null || fuelTank == null || fuelDisplay == null)
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            fuelTank = GameObject.Find("Fuel Gauge Image").GetComponent<RawImage>();
            fuelDisplay = GameObject.Find("Fuel Gauge Text").GetComponent<TMP_Text>();
        }
        else
        {
            gasAmount = GM.tankInfos[GM.playerTurn].fuel;
            maxFuel = GM.tankInfos[GM.playerTurn].maxFuel;
            fuelDisplay.text = Math.Round(gasAmount).ToString() + "/\n" + maxFuel.ToString();

            if (gasAmount > 0)
            {
                gasAmount = GM.tankInfos[GM.playerTurn].fuel;
                fuelDisplay.text = Math.Round(gasAmount).ToString() + "/\n" + maxFuel.ToString();
                fuelTank.color = Color.Lerp(Color.black, Color.white, (gasAmount / maxFuel));
            }
        }
        
	}
}
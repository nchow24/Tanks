using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankObject : ScriptableObject {
	public bool isBot;
	public string name;
	public int tankid;		//tankid determines the order of the game and the raycast color

	public int score;
	public int health;
	public int coins;

	public int armAngle;
	public int tankPos;

	public int[,] weaponsAmmo;
	public int activeWeapon;

	public int repairs;
	public int fuel;

	public int fueltank;
	public int maxHealth;
}

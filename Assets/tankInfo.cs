using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tankInfo : MonoBehaviour {

	public Renderer rend;
	public bool isBot;

	public string name;
	public int tankid;		//tankid determines the order of the game and the raycast color
	public Color32 colour;

    public int health;
	public int maxHealth;	// Variable representing maxHealth; equivalent to 100 + 20 x armorLevel

	public int score;		//cumulative sum of coins
	public int funds;		//cumulative sum of coins minus cumulative spent on upgrades and weapons

	public int[] ammo;		// Index of array = weapon
	public int activeWeapon;	//Currently selected weapon

	public Vector3 pos;			// Position on map (x,y,z)
	public Vector3 armAngle;	// 0 = Horizontally right

	public float fuel;
	public int repairCount; // repairCount is now at ammo[0] 

	public float maxFuel; 	// Variable representing maxFuel; equivalent to 100 + 20 x fuelLevel
	public int fuelLevel;	// Max is 5
	public int armorLevel;	// Max upgrades is 5

	GameObject GM;
	private GameManager GMScript;

	public bool destroyingTank = false;		// Variable to allow tank to be destroyed only once

	// Hide when instantiated
	void Start(){
		//rend = GetComponent<Renderer>();
		//rend.enabled = false;

		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();
	}

	public void damageTank (int damage) {
		health -= damage;
		if (health <= 0) {
			GMScript.destroyTank (tankid);
			Debug.Log("KILL TANK");
		}
	}

	public void increaseScore (int score) {
		score += score;
	}

	// TODO: Logic when purchase can't be performed (e.g. not enough cash)
	public void buyAmmo (int weaponIndex, int amount) {
		if (weaponIndex == 0) {
			Debug.Log("Tank should have infinite default ammo. Exiting early");
			return;
		}
		if (ammo[weaponIndex] + amount > 50) {	// TODO: Check max quantity of ammo
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		ammo[weaponIndex] += amount;
	}

	public void buyFuel(int amount) {
		if (fuelLevel + amount > 5) {
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		fuelLevel += amount;
	}

	public void buyArmor(int amount) {
		if (armorLevel + amount > 5) {
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		armorLevel += amount;
	}

	public void buyRepair(int amount) {
		if (repairCount + amount > 20) {			// TODO: Check max quantity of repairs
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		repairCount += amount;
	}

	public int getScore() {
		return score;
	}

	public string getName() {
		return name;
	}

    public int[] getAmmo()
    {
        return ammo;
    }


    public int getFunds()
    {
        return funds;
    }

    public void setActiveWeapon(int i)
    {
        activeWeapon = i;
        GameObject.Find("PullMenuCanvas").GetComponent<InventoryMenu>().WeaponLogoChange(i, ammo[i]);
    }

}

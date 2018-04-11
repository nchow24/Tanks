using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	public Renderer rend;
	public bool isBot;
	
	[System.Serializable]
	public class Stats {
		public string name;
        public int health;
		public int maxHealth;
		public int pos;			// Position on map. Should make double/float?

		public int score;
		public int funds;

		public int[] ammo;		// Index of array = weapon
		public int armAngle;	// 0 = Horizontally right
								// Should make double/float for more precision?
		public int fuel;
		public int repairCount;

		public int fuelLevel;	// Max is 5
		public int armorLevel;	// Max is 5
	}

	public Stats stats = new Stats();

	// Hide when instantiated
	void Start(){
		rend = GetComponent<Renderer>();
		rend.enabled = false;
	}

	public void damageTank (int damage) {
		stats.health -= damage;
		if (stats.health <= 0) {
			Debug.Log("KILL TANK");
		}
	}

	public void increaseScore (int score) {
		stats.score += score;
	}

	// TODO: Logic when purchase can't be performed (e.g. not enough cash)
	public void buyAmmo (int weaponIndex, int amount) {
		if (weaponIndex == 0) {
			Debug.Log("Tank should have infinite default ammo. Exiting early");
			return;
		}
		if (stats.ammo[weaponIndex] + amount > 50) {	// TODO: Check max quantity of ammo
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		stats.ammo[weaponIndex] += amount;
	}

	public void buyFuel(int amount) {
		if (stats.fuelLevel + amount > 5) {
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		stats.fuelLevel += amount;
	}

	public void buyArmor(int amount) {
		if (stats.armorLevel + amount > 5) {
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		stats.armorLevel += amount;
	}

	public void buyRepair(int amount) {
		if (stats.repairCount + amount > 20) {			// TODO: Check max quantity of repairs
			Debug.Log("Tank should not be able to purchase more than max. Exiting early");
			return;
		}
		stats.repairCount += amount;
	}

	public int getScore() {
		return stats.score;
	}

	public string getName() {
		return stats.name;
	}
	
}

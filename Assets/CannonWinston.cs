using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonWinston : MonoBehaviour {

    int weaponSelected;
    public Transform BarrelTip;
    public float power = 0;
    public float max = 1000;
    public float windMagnitude = 2f;
    public float powerScale = 0.65f;

    GameObject GM;
    private GameManager GMScript;

    public GameObject[] weaponPrefabs = new GameObject[8];
    private GameObject weaponPrefabClone;
    private TMP_Text powerText;
    private Image powerAmount;

    private void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
        powerText = GameObject.Find("Power Value").GetComponent<TMP_Text>();
        powerAmount = GameObject.Find("Power Amount").GetComponent<Image>();        
    }

    void Update () {

        // Find GameObjects if unassigned (new rounds)
        if (powerText == null)
        {
            powerText = GameObject.Find("Power Value").GetComponent<TMP_Text>();
        }

        if (powerAmount == null)
        {
            powerAmount = GameObject.Find("Power Amount").GetComponent<Image>();
        }

        // Initialize current percentage
        powerText.text = Math.Round(((power / max) * 100)).ToString() + "%";
        powerAmount.rectTransform.localScale = new Vector3((power / max), 1, 1);
        
        // When power increases
        if (Input.GetKey(KeyCode.PageUp))
        {
            if (power < 1000)
            {
                power = power + 2;
            }
            powerText.text = Math.Round(((power/max) * 100)).ToString() + "%";
            powerAmount.rectTransform.localScale = new Vector3((power / max), 1, 1);
        }

        // When power decreases
        if (Input.GetKey(KeyCode.PageDown))
        {
            if (power > 0)
            {
                power = power - 2;
            }
            powerText.text = Math.Round(((power / max) * 100)).ToString() + "%";
            powerAmount.rectTransform.localScale = new Vector3((power / max), 1, 1);
        }

		if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
            // Don't decrement ammo for default missile
            if (weaponSelected != 0)
            {
                GMScript.tankInfos[GMScript.playerTurn].ammo[GetWeaponSelected()]--;
                // If ammo reaches 0, change to default weapon
                if (GMScript.tankInfos[GMScript.playerTurn].ammo[GetWeaponSelected()] <= 0)
                {
                    GMScript.tankInfos[GMScript.playerTurn].setActiveWeapon(1);
                }
            }
            // End turn after time depending on shot
            // May need fine-tuning
            /*switch (weaponSelected)
            {
                // Frag
                case 3:
                    Invoke("EndTurn", 3f);
                    break;
                // Ball
                case 5:
                    Invoke("EndTurn", 4f);
                    break;
                // Incendiary
                case 6:
                    Invoke("EndTurn", 6.5f);
                    break;
                // Heli
                case 7:
                    Invoke("EndTurn", 4f);
                    break;
                default:
                    Invoke("EndTurn", 2.5f);
                    break;
            }*/
        }
	}

    int GetWeaponSelected()
    {
        return GMScript.tankInfos[GMScript.playerTurn].activeWeapon;

    }

    void ShootProjectile()
    {
		GMScript.movementEnabled = false;
		weaponSelected = GetWeaponSelected() - 1;
        weaponPrefabClone = Instantiate(weaponPrefabs[weaponSelected], BarrelTip.transform.position, BarrelTip.transform.rotation);
        weaponPrefabClone.GetComponent<Rigidbody2D>().AddForce(weaponPrefabClone.transform.right * -(power * powerScale));
        
        // Adding wind
        weaponPrefabClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(GMScript.windSpeed * windMagnitude,0));
        Destroy(weaponPrefabClone, 4.5f);
    }

    void EndTurn()
    {
        //TODO
        GMScript.nextTurn();
    }

    /***********************************************************
     * PROJECTILE DYNAMICS
     * 
     * 0 BULLET
     * 
     * 1 BIG BULLET 
     * 
     * 2 ARMOR PIERCER
     * 
     * 3 FRAG
     * 
     * 4 EXPLOSION
     * 
     * 5 BALL
     * 
     * 6 INCENDIARY
     * > Shoots Incendiary Projectile (IncenProj.cs)
     *     > Spawns 5 Fires (IncenFire.cs)
     *         > Damages player continuously while in fire
     * 
     * 7 HELI-STRIKE
     * > Shoots Heli Strike Marker (HeliStrikeProj.cs)
     *     > Spawns Helicopter (Helicopter.cs)
     *         > Spawns 5 Bombs (Bomb.cs)
     *             > Spawns Explosion (HeliExplosion.cs)
     *             > Damage Players where bomb lands (20 dmg)
     * *********************************************************
     */
}

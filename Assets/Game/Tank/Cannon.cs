using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Turret
{
    public GameObject bulletPrefab; //will be taken out for weapon model
    public float power;
    public Transform barrelTip;
    private GameObject GM;
    private GameManager GMscript;

    private void Start()
    {
        GM = GameObject.Find("GameManager");
        GMscript = GM.GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.PageUp))
        {
            if (power < 1000)
            {
                power++;
            }
        }

        if (Input.GetKey(KeyCode.PageDown))
        {
            if (power > 0)
            {
                power--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bulletPrefab.name == "bullet")
            {
                SmallMissle.Shoot(this, bulletPrefab, barrelTip, power);
            }
            if (bulletPrefab.name == "Frag")
            {
                Frag.Shoot(this, bulletPrefab, barrelTip, power);
            }
            if (bulletPrefab.name == "Big Bullet")
            {
                LargeMissle.Shoot(this, bulletPrefab, barrelTip, power);
                GMscript.tankInfos[GMscript.playerTurn].ammo[1]--;
            }
            if (bulletPrefab.name == "Ball")
            {
                Ball.Shoot(this, bulletPrefab, barrelTip, power);
                GMscript.tankInfos[GMscript.playerTurn].ammo[7]--;
            }

        }
    }
}

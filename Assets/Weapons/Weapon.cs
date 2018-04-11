using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon: ScriptableObject
{
    public int damage { get; set; }
    public int ammo { get; set; }
    public int maxAmmo { get; set; }
    public GameObject model { get; set; }
    public Vector3 offset { get; set; }
    public LayerMask whatToHit;

    void Hit()
    {
        Tank Player = new Tank();
        Player.stats.health = Player.stats.health - damage;
    }

}

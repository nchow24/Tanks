using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliStrikeProj : MonoBehaviour {

    Vector3 hitPos;
    public GameObject helicopterPrefab;
    private GameObject helicopterPrefabClone;
    private Helicopter heliScript;

    private GameObject GM;
    private GameManager GMScript;
    private bool timedOut = true;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag.Equals("Terrain") || collisionInfo.tag.Equals("Player"))
        {
            timedOut = false;
            
            // Store position where projectile landed
            hitPos = gameObject.transform.position;

            // Spawn plane on the right
            helicopterPrefabClone = Instantiate(helicopterPrefab, helicopterPrefab.transform.position, helicopterPrefab.transform.rotation);
            // Helicopter movement and bomb dropping is done in helicopter.cs
            // Send position of projectile to Helicopter.cs
            heliScript = helicopterPrefabClone.GetComponent<Helicopter>();
            heliScript.hitPos = hitPos;

            // Delete helicopter just in case
            Destroy(helicopterPrefabClone, 8);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (timedOut)
        {
            GMScript.projDestroyed();
        }
    }
}

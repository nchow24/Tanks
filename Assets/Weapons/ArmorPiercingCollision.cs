using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPiercingCollision : MonoBehaviour {

    public GameObject exp2;
    private GameObject GM;
    private GameManager GMScript;

    void Start ()
    {
        Invoke("Explode", 3);
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
    }

    void Explode()
    {
        exp2 = (GameObject)Instantiate(exp2);
        exp2.transform.position = transform.position;
        Destroy(gameObject, 0);
        Destroy(exp2, 1);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("Terrain") || coll.tag.Equals("Player"))
        {
            Explode();
            if (coll.gameObject.tag.Equals("Player"))
            {
                coll.gameObject.GetComponent<tankInfo>().damageTank(55);
                GMScript.tankInfos[GMScript.playerTurn].score += 550;
                GMScript.tankInfos[GMScript.playerTurn].funds += 550;

            }
        }
    }

    void OnDestroy()
    {
        GMScript.projDestroyed();
    }
}

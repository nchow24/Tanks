using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {

    public GameObject exp2;
    private GameObject GM;
    private GameManager GMScript;
    //private bool timedOut = true;

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
            //timedOut = false;
            Explode();
            if (coll.gameObject.tag.Equals("Player"))
            {
                coll.gameObject.GetComponent<tankInfo>().damageTank(5);
                GMScript.tankInfos[GMScript.playerTurn].score += 50;
                GMScript.tankInfos[GMScript.playerTurn].funds += 50;
            }
        }
    }

    void OnDestroy()
    {
        GMScript.projDestroyed();
    }
}

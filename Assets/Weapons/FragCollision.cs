using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragCollision : MonoBehaviour
{

    public GameObject exp2;
    public GameObject frag1, frag2, frag3;
    private GameObject GM;
    private GameManager GMScript;
    //private bool timedOut = true;

    void Start()
    {
        Invoke("Explode", 3);
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();

    }

    void Explode()
    {
        //timedOut = false;
        exp2 = (GameObject)Instantiate(exp2);
        exp2.transform.position = transform.position;


        Destroy(gameObject, 0);
        Destroy(exp2, 1);

        invokeGrenades();
    }

    void invokeGrenades()
    {
        Debug.Log("called");
        frag1 = (GameObject)Instantiate(frag1);
        frag2 = (GameObject)Instantiate(frag2);
        frag3 = (GameObject)Instantiate(frag3);


        frag1.transform.position = transform.position;
        frag2.transform.position = transform.position;
        frag3.transform.position = transform.position;

        //frag1.GetComponent<Rigidbody2D>().AddForce(frag1.transform.up * (float)100);
        //frag2.GetComponent<Rigidbody2D>().AddForce(frag2.transform.up * (float)90);
        //frag3.GetComponent<Rigidbody2D>().AddForce(frag3.transform.up * (float)500);

        frag1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        frag2.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 5);
        frag3.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 5);


        Physics2D.IgnoreCollision(frag1.GetComponent<Collider2D>(), frag2.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(frag1.GetComponent<Collider2D>(), frag3.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(frag1.GetComponent<Collider2D>(), exp2.GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(frag2.GetComponent<Collider2D>(), frag3.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(frag2.GetComponent<Collider2D>(), exp2.GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(frag3.GetComponent<Collider2D>(), exp2.GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("Terrain") || coll.tag.Equals("Player"))
        {
            //timedOut = false;
            Explode();
            if (coll.gameObject.tag.Equals("Player"))
            {
                coll.gameObject.GetComponent<tankInfo>().damageTank(15);
                GMScript.tankInfos[GMScript.playerTurn].score += 150;
                GMScript.tankInfos[GMScript.playerTurn].score += 150;

            }
        }
    }
}

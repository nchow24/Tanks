using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour{

    public GameObject exp2;
    private GameObject exp2_clone;
    private float timeStart;
    private GameObject GM;
    private GameManager GMScript;

    void Start()
    {
        timeStart = Time.time;
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
        if (GameObject.Find("Walls") != null)
        {
            foreach (BoxCollider2D boxCollider in GameObject.Find("Walls").GetComponents<BoxCollider2D>())
            {
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), boxCollider);
            }
        }
    }

    private void Update()
    {
        if (Time.time > timeStart + 3) {
            Explode();
        }
    }

    void Explode()
    {
        exp2_clone = Instantiate(exp2, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(exp2_clone, 1);
    }

    void OnDestroy()
    {
        GMScript.projDestroyed();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenProj : MonoBehaviour {

    private GameObject GM;
    private GameManager GMScript;
    public GameObject firePrefab;
    private GameObject firePrefabClone;
    private float impactY = 4f;
    private float impactXStep = 0.5f;
    public int numFires = 2;
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
            Vector3 startingPos = transform.position;
            startingPos.y += 0.5f;

            firePrefabClone = Instantiate(firePrefab, startingPos, Quaternion.identity);
            firePrefabClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, impactY);
            Destroy(firePrefabClone, 5);

            for (int i = 1; i <= numFires; i++)
            {
                firePrefabClone = Instantiate(firePrefab, startingPos, Quaternion.identity);
                firePrefabClone.GetComponent<Rigidbody2D>().velocity = new Vector2(impactXStep * i, impactY);
                Destroy(firePrefabClone, 5);

                firePrefabClone = Instantiate(firePrefab, startingPos, Quaternion.identity);
                firePrefabClone.GetComponent<Rigidbody2D>().velocity = new Vector2(impactXStep * -i, impactY);
                Destroy(firePrefabClone, 5);
            }

            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if(timedOut)
        {
            GMScript.projDestroyed();
        }
    }
}

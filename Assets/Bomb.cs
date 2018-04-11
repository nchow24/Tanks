using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosionPrefab;
    GameObject explosionPrefabClone;
    private GameObject GM;
    public AudioSource source;
    private AudioClip boom;
    private GameManager GMScript;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag.Equals("Terrain") || collisionInfo.tag.Equals("Player"))
        {
            explosionPrefabClone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            source = explosionPrefabClone.GetComponent<AudioSource>();
            source.PlayOneShot(boom);
            Destroy(explosionPrefabClone, 2f);

            if (collisionInfo.tag.Equals("Player"))
            {
                collisionInfo.gameObject.GetComponent<tankInfo>().damageTank(25);
                GMScript.tankInfos[GMScript.playerTurn].score += 250;
                GMScript.tankInfos[GMScript.playerTurn].funds += 250;
            }


            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GMScript.projDestroyed();
    }
}

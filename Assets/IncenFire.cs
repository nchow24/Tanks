using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenFire : MonoBehaviour {

    private Vector3 impactPos;
    private float impactTime;
    public float damageInterval = 0.2f;
    private GameObject GM;
    private GameManager GMScript;
    private AudioSource source;
    private AudioClip boom;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
        source = gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerStay2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag.Equals("Terrain"))
        {
            gameObject.transform.position = impactPos;
        }
        if (collisionInfo.tag.Equals("Player"))
        {
            if (Time.time - impactTime > damageInterval)
            {
                collisionInfo.gameObject.GetComponent<tankInfo>().damageTank(1);
                GMScript.tankInfos[GMScript.playerTurn].score += 10;
                GMScript.tankInfos[GMScript.playerTurn].funds += 10;
                impactTime = Time.time;
                
            }
            Debug.Log("TODO: Increase Score");
        }
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag.Equals("Terrain"))
        {
            source.PlayOneShot(boom);
            impactPos = gameObject.transform.position;
            impactTime = Time.time;
        }
    }

    void OnDestroy()
    {
        GMScript.projDestroyed();
    }

}

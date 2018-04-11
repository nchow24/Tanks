using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private GameObject GM;
    private GameManager GMScript;
    private AudioSource source;
    private AudioClip boom;

    private void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<GameManager>();
        source = gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<tankInfo>().damageTank(30);
            GMScript.tankInfos[GMScript.playerTurn].score += 300;
            GMScript.tankInfos[GMScript.playerTurn].funds += 300;
            source.PlayOneShot(boom);
        }

    }

}

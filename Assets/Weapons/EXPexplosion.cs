using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPexplosion : MonoBehaviour
{
    private GameObject GM;
    private AudioSource source;
    private AudioClip boom;
    private GameManager GMScript;


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
            collision.gameObject.GetComponent<tankInfo>().damageTank(50);
            source.PlayOneShot(boom);
            GMScript.tankInfos[GMScript.playerTurn].score += 500;
            GMScript.tankInfos[GMScript.playerTurn].funds += 500;
        }
    }

}

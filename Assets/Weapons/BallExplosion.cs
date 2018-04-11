using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExplosion : MonoBehaviour
{
    public AudioClip boom;
    private GameObject GM;
    private GameManager GMScript;
    private AudioSource source;

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
            GMScript.tankInfos[GMScript.playerTurn].score += 500;
            GMScript.tankInfos[GMScript.playerTurn].funds += 500;
            source.PlayOneShot(boom);

        }
    }

}

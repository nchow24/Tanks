using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

    public Vector3 hitPos;
    private Vector3 moveTowardsPos;
    public float speed;
    public GameObject bombPrefab;
    private GameObject bombPrefabClone;
    private int bombsDropped = 0;
    public int numBombs = 5;
    private float bombTime = 0f;

    void Start () {
        // Move towards outside the screen onto the left
        moveTowardsPos = transform.position;
        moveTowardsPos.x = -10f;
	}
	
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTowardsPos, step);

        // Drop when close to position
        if (Mathf.Abs(transform.position.x - (hitPos.x + 1f)) <= 0.085)
        {
            bombPrefabClone = Instantiate(bombPrefab,transform.position, Quaternion.identity);
            bombTime = Time.time;
            bombsDropped++;
            Destroy(bombPrefabClone, 6);
        }

        // Drop more bombs based on time
        if (bombsDropped < numBombs && (Time.time - bombTime) > 0.035f && bombTime != 0)
        {
            bombPrefabClone = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bombTime = Time.time;
            bombsDropped++;
            Destroy(bombPrefabClone, 6);
        }


	}
}

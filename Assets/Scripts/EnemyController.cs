using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Vector3 initialPos;
    private int direction = 1;
    

    public float speed = 3;
    public float speedFactor = 1;
    public float rangeY = 2;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        speedFactor = direction == -1 ? 5f : 1;

        float movementY = speedFactor * speed * Time.deltaTime * direction;

        // new position y
        float newY = transform.position.y + movementY;

        // checking whether we have left our range
        if (Mathf.Abs(newY - initialPos.y) > rangeY) {
            direction *= -1;
        } else {
            transform.position += new Vector3(0, movementY, 0);
        }
	}
}

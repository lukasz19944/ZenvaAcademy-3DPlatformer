using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 4;
    public float jumpForce = 8;

    public AudioSource coinSound;

    public float cameraDistZ = 6;

    private Rigidbody rb;
    private Collider col;

    private bool pressedJump = false;
    private Vector3 size;

    private float minY = -2.5f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        size = col.bounds.size;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        WalkHandler();
        JumpHandler();
        CameraFollowPlayer();
        FallHandler();
    }

    private void FallHandler() {
        if (transform.position.y <= minY) {
            GameManager.instance.GameOver();
        }
    }

    // walking
    void WalkHandler() {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);

        Vector3 newPos = transform.position + movement;

        rb.MovePosition(newPos);

        if (hAxis != 0 || vAxis != 0) {
            Vector3 direction = new Vector3(hAxis, 0, vAxis);

            //transform.forward = direction;

            rb.rotation = Quaternion.LookRotation(direction);
        } 
    }

    // jumping
    void JumpHandler() {
        float jAxis = Input.GetAxis("Jump");

        bool isGrounded = CheckGrounded();

        if (jAxis > 0) {
            if (!pressedJump && isGrounded) {
                pressedJump = true;

                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);

                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        } else {
            pressedJump = false;
        }
    }

    private bool CheckGrounded() {
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        // check if we are grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Coin")) {
            GameManager.instance.IncreaseScore(1);

            coinSound.Play();

            Destroy(other.gameObject);
        } else if (other.CompareTag("Enemy")) {
            GameManager.instance.GameOver();
        } else if (other.CompareTag("Goal")) {
            GameManager.instance.IncreaseLevel();
        }
    }

    private void CameraFollowPlayer() {
        Vector3 cameraPos = Camera.main.transform.position;

        cameraPos.z = transform.position.z - cameraDistZ;

        Camera.main.transform.position = cameraPos;
    }
}

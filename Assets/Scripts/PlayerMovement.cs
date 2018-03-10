using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.5f;
    public float jumpForce = 100f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask whatIsGround;

    private float horizontal = 0f;
    private float vertical = 0f;
    private bool jump = false;
    private bool onGround = true;

    private Rigidbody rb;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        GetInput();
        Movement();
	}

    void GetInput() {
        // Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Jumping
        jump = Input.GetKeyDown(KeyCode.Space);
        onGround = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    void Movement() {
        transform.position = new Vector3(
            transform.position.x + (horizontal * speed), 
            transform.position.y, 
            transform.position.z + (vertical * speed)
        );

        if (jump && onGround) {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce);
        }
    }
    
    void OnDrawGizmos() {
        // Drawing the ground check sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

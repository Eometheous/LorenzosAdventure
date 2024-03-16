using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float movementSpeed = 10f;
    private readonly float jumpForceGround = 100f;
    private readonly float jumpForceAir = 7;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private bool touchingGround;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        touchingGround = false;
        jumping = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        if (Input.GetAxis("Vertical") > 0) jumping = true;
        else jumping = false;
    }

    void FixedUpdate() 
    {
        if (touchingGround) {
            rb.AddForce((movementDirection * movementSpeed) - rb.velocity);
            if (jumping) rb.AddForce(Vector2.up * jumpForceGround);
        }
        if (jumping && rb.velocity.y > 1.5) rb.AddForce(Vector2.up * jumpForceAir);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.GetContact(0).normal;
        if (other.gameObject.CompareTag("Ground") && normal == Vector2.up) {
            touchingGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Vector2 normal = other.GetContact(0).normal;
        if (other.gameObject.CompareTag("Ground") && normal == Vector2.up) {
            touchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground")) touchingGround = false;
    }
}

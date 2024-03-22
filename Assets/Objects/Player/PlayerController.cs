using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private readonly float movementForce = 10f;
    private readonly float airMovementForce = 4f;
    private readonly float jumpVelocity = 140f;
    private readonly float jumpForceAir = 7f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private bool touchingGround;
    private bool jumping;
    private bool ignorePlatform;

    // Start is called before the first frame update
    void Start()
    {
        touchingGround = false;
        jumping = false;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6,1, true);
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (Input.GetAxis("Vertical") > 0) jumping = true;
        else jumping = false;

        if (rb.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (rb.velocity.x > 0) GetComponent<SpriteRenderer>().flipX = false;
    }

    void FixedUpdate() 
    {
        Vector2 force = Vector2.zero;
        
        if (touchingGround) 
        {
            force += movementDirection * movementForce - rb.velocity;
            if (jumping) force += Vector2.up * jumpVelocity;
        }
        else force += movementDirection * airMovementForce - rb.velocity;
        rb.AddForce(force);
        if (jumping && rb.velocity.y > 0) rb.AddForce(Vector2.up * jumpForceAir);
        
        ignorePlatform = rb.velocity.y > 0 || Input.GetAxis("Vertical") < 0; 
        Physics2D.IgnoreLayerCollision(6, 7, ignorePlatform);
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

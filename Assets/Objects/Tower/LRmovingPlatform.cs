using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2;
    private bool left;

    // Start is called before the first frame update
    void Start()
    {
        // left = true;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // left = rb.velocity.x > 0;
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounds")) 
        {
            rb.velocity *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.collider.transform.SetParent(null);
        }
    }
}

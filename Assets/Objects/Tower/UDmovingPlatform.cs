using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDmovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2;
    private bool up;

    // Start is called before the first frame update
    void Start()
    {
        up = true;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (up) {
            rb.velocity = Vector2.up * speed;
        }
        else {
            rb.velocity = Vector2.down * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounds")) 
        {
            if (up) {
                rb.velocity = Vector2.up * speed;
                up = false;
            }
            else {
                rb.velocity = Vector2.down * speed;
                up = true;
            }
            
        }
    }
}

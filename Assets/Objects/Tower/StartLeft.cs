using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLeft : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }
}

using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float groundSpeed = 5f;
    private readonly float airMovementForce = 4f;
    private readonly float jumpForce = 275f;
    private readonly float jumpForceAir = 7f;
    public Rigidbody2D rb;
    private Vector2 movementDirection;
    public bool touchingGround;
    public bool jumping;
    public bool jumpingDown;
    public bool ignorePlatform;
    private float idleTime;
    private float relativeVelocity;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip lorenzoMeow1;
    public AudioClip lorenzoMeow2;
    public AudioClip lorenzoPurring;
    public AudioClip lorenzoJump1;
    public AudioClip lorenzoJump2;
    private float timeSinceMeow;
    private bool playingMeow;

    public bool phasingThroughPlatform;

    // Start is called before the first frame update
    void Start()
    {
        touchingGround = false;
        jumping = false;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6,1, true);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playingMeow = false;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        
        if (Input.GetKeyDown(KeyCode.W)) jumping = true;
        if (Input.GetKeyUp(KeyCode.W)) jumping = false;
        if (Input.GetKeyDown(KeyCode.S)) jumpingDown = true;
        if (Input.GetKeyUp(KeyCode.S)) jumpingDown = false;

        if (rb.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (rb.velocity.x > 0) GetComponent<SpriteRenderer>().flipX = false;
        animator.SetFloat("Speed", Math.Abs(relativeVelocity));
        animator.SetBool("OnGround", touchingGround);
        animator.SetFloat("IdleTime", idleTime);
    }

    void FixedUpdate() 
    {
        Vector2 force = Vector2.zero;
        
        if (touchingGround) 
        {
            rb.velocity = movementDirection * groundSpeed;
            if (transform.parent != null) {
                rb.velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
                relativeVelocity = (rb.velocity - transform.parent.GetComponent<Rigidbody2D>().velocity).magnitude;
            }
            else relativeVelocity = rb.velocity.magnitude;
            if (jumping) force += Vector2.up * jumpForce;
        }
        else force += movementDirection * airMovementForce;
        rb.AddForce(force);

        if (jumping && rb.velocity.y > 0) rb.AddForce(Vector2.up * jumpForceAir);
        
        ignorePlatform = rb.velocity.y > 0 || jumpingDown;

        if (!phasingThroughPlatform) Physics2D.IgnoreLayerCollision(6, 7, ignorePlatform);

        idleTime += Time.deltaTime;
        if (math.abs(rb.velocity.x) > 0 || math.abs(rb.velocity.y) > 0) idleTime = 0;

        if (jumping  && touchingGround) {
            PlayJumpSound();
        }

        timeSinceMeow += Time.deltaTime;
        if (timeSinceMeow > 10 && !playingMeow) {
            PlayMeowSound();
            if (idleTime > 4 ) audioSource.PlayOneShot(lorenzoPurring);
            playingMeow = true;
            timeSinceMeow = UnityEngine.Random.Range(-5, 0);
        }
        else playingMeow = false;
    }

    private void PlayJumpSound() {
        audioSource.volume = .05f;
        int jumpToPlay = UnityEngine.Random.Range(0,2);
        if (jumpToPlay == 0) audioSource.PlayOneShot(lorenzoJump1);
        else audioSource.PlayOneShot(lorenzoJump2);
    }

    private void PlayMeowSound() {
        audioSource.volume = 1;
        int meowToPlay = UnityEngine.Random.Range(0,2);
        if (meowToPlay == 0) audioSource.PlayOneShot(lorenzoMeow1);
        else audioSource.PlayOneShot(lorenzoMeow2);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        for (int i = 0; i < other.contactCount; i++) {
            Vector2 normal = other.GetContact(i).normal;
            if (other.gameObject.CompareTag("Ground") && normal.y == 1) {
                touchingGround = true;
            }
            if (other.gameObject.CompareTag("Ground") && normal.x == -1 && jumping) {
                rb.velocity = new Vector2(-3, 5);
                PlayJumpSound();
            }
            if (other.gameObject.CompareTag("Ground") && normal.x == 1 && jumping) {
                rb.velocity = new Vector2(3, 5);
                PlayJumpSound();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        for (int i = 0; i < other.contactCount; i++) {
            Vector2 normal = other.GetContact(i).normal;
            if (other.gameObject.CompareTag("Ground") && normal.y == 1) {
                touchingGround = true;
            }
            if (other.gameObject.CompareTag("Ground") && normal.x == -1 && jumping) {
                rb.velocity = new Vector2(-3, 5);
                touchingGround = false;
                PlayJumpSound();
            }
            if (other.gameObject.CompareTag("Ground") && normal.x == 1 && jumping) {
                rb.velocity = new Vector2(3, 5);
                touchingGround = false;
                PlayJumpSound();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground")) touchingGround = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("platform") && jumpingDown) phasingThroughPlatform = true;
        if (other.gameObject.CompareTag("platform") && !touchingGround) phasingThroughPlatform = true;

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("platform") && jumpingDown) phasingThroughPlatform = true;
        if (other.gameObject.CompareTag("platform") && touchingGround) phasingThroughPlatform = false;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("platform")) phasingThroughPlatform = false;
    }
}

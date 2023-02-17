using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int speed = 10;
    private bool faceRight = false;
    private bool isWalking = false;
    public int jumpPower = 500;
    private float moveX, moveY;

    //Sound
    public AudioClip walkSfx;
    public AudioClip jumpSfx;
    public AudioClip pickupSfx;
    public float walkVolume;
    public float jumpVolume;
    public float pickupVolume;
    AudioSource audioSource;

    private bool isGrounded = true;
    public LayerMask groundLayer;

    private bool isClimbing;
    RaycastHit2D onLadder;
    public LayerMask ladderLayer;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        //check is grounded or on ladder
        PlayerRayCast();
        Move();

        //make jump more realistic (go down faster)
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
        }
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        if (!isGrounded)
        {
            isWalking = false;
        }
        else
        {
            isWalking = !Mathf.Approximately(moveX, 0f);
        }
        animator.SetBool("IsWalking", isWalking);

        //Climbing
        Climb();
        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpPower));
            PlayJumpSound();
        }
        //flip left and right
        if(moveX < 0.0f && faceRight == false)
        {
            Flip();
        }
        else if(moveX > 0.0f && faceRight == true)
        {
            Flip();
        }
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    void Climb()
    {
        if (onLadder.collider != null)
        {
            moveY = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
            else
            {
                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        isClimbing = false;
                    }
                }
            }
        }
        else
        {
            if (!Mathf.Approximately(moveX, 0f))
            {
                isClimbing = false;
            }
        }

        if (isClimbing && onLadder.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveY * speed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
        animator.SetBool("IsClimbing", isClimbing);
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void PlayerRayCast()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider2D.bounds.extents.y + .01f, groundLayer);
        onLadder = Physics2D.Raycast(transform.position, Vector2.up, capsuleCollider2D.bounds.center.y + .01f, ladderLayer);
    }

    void PlayWalkSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(walkSfx, walkVolume);
        }
    }

    void PlayJumpSound()
    {  
       audioSource.PlayOneShot(jumpSfx, jumpVolume);
    }

    void PlayPickupSound()
    {
        audioSource.PlayOneShot(pickupSfx, pickupVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key") || collision.CompareTag("Pickup"))
        {
            PlayPickupSound();
        }
    }
}

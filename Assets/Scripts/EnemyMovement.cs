using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed = 5;
    [HideInInspector]
    public int moveX = 1;
    private bool faceRight = false;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;


    private bool isGrounded = false;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider2D.bounds.extents.y + .1f, groundLayer);
        if (isGrounded)
        {
            Move();
        }
    }

    void Move()
    {
        animator.SetBool("IsWalking", true);
        rb.velocity = new Vector2(moveX, 0) * speed;
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        //flip left and right
        if (moveX > 0)
        {
            moveX = -1;
        }
        else
        {
            moveX = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("Player"))
            foreach(ContactPoint2D hitPos in collision.contacts)
            {
                if(hitPos.point.y - transform.position.y  > 0)
                {
                    Flip();
                }
            }
     
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    public int jumpPowerAfterKill = 2000;
    public bool isKillEnemy = false;
    public LayerMask enemyLayer;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider2D.bounds.extents.y + .1f, enemyLayer);
        isKillEnemy = hit;
        if (isKillEnemy)
        {
            rb.AddForce(Vector2.up * jumpPowerAfterKill);
            hit.collider.gameObject.GetComponent<EnemyDead>().Dead();
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;

    public AudioClip deadSfx;
    public float deadVolume;
    AudioSource audioSource;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            Dead();
        }
    }

    public void Dead()
    {
        rb.AddForce(Vector2.right * 200);
        rb.gravityScale = 8;
        rb.freezeRotation = false;
        capsuleCollider2D.enabled = false;
        animator.SetBool("IsDead", true);
        PlayDeadSound();
        Destroy(gameObject, deadSfx.length);
    }

    void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadSfx, deadVolume);
    }
}

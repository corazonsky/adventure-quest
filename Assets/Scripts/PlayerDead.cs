using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDead : MonoBehaviour
{
    public static event Action OnPlayerDead;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider2D;
    private bool isKillEnemy = false;
    public float deadTime = 5;

    //audio
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

    private void Update()
    {
        isKillEnemy = gameObject.GetComponent<PlayerKill>().isKillEnemy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Dead();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Boom");
            if (!isKillEnemy)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        gameObject.GetComponent<PlayerKill>().enabled = false;
        gameObject.GetComponent<PlayerControl>().enabled = false;
        animator.SetBool("IsDead", true);
        capsuleCollider2D.enabled = false;
        rb.AddForce(Vector2.right * 200);
        PlayDeadSound();
        
        Destroy(gameObject, deadTime);
    }

    private void OnDestroy()
    {
        OnPlayerDead?.Invoke();
    }

    void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadSfx, deadVolume);
    }
}

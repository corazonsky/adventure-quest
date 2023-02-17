using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    //private bool shoot = true;
    public float bulletForce = 1000;
    public bool shootToRight;
    private int shootDirection;
    GameObject currentBullet = null;
    Animator animator;

    //audio
    public AudioClip shootSfx;
    public float shootVolume;
    AudioSource audioSource;



    private void Start()
    {
       animator = GetComponentInParent<Animator>();
       audioSource = gameObject.GetComponent<AudioSource>();
        if (shootToRight)
        {
            shootDirection = 1;
        }
        else
        {
            shootDirection = -1;
        }
    }

    private void Update()
    {
        if (currentBullet == null)
        {
            PlayShootSound();
            animator.SetBool("IsShooting", true);

            currentBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D bulletRb = currentBullet.GetComponent<Rigidbody2D>();
            EnemyMovement enemy = currentBullet.GetComponent<EnemyMovement>();

            //make enemy face and move towards the cannon shoot direction
            Vector2 localScale = currentBullet.transform.localScale;
            localScale.x *= -shootDirection;
            currentBullet.transform.localScale = localScale;
            enemy.moveX = shootDirection;

            bulletRb.AddForce(shootDirection* transform.right* bulletForce);
            bulletRb.AddForce(transform.up * bulletForce);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSfx, shootVolume);
    }
}

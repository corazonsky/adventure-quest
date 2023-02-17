using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    public static event Action OnEndLevel;
    public static event Action OnCollectMore;
    Animator anim;
    private bool isOnTrigger;
    bool hasAllPickupCollected = false;

    private void Awake()
    {
        ShowPickup.OnAllPickupCollected += UnlockLevel;
    }

    private void OnDestroy()
    {
        ShowPickup.OnAllPickupCollected -= UnlockLevel;
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOnTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (hasAllPickupCollected)
            {
                OnEndLevel?.Invoke();
                anim.SetBool("IsOpen", true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                OnCollectMore?.Invoke();
            }
        }
    }

    void UnlockLevel()
    {
        hasAllPickupCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isOnTrigger = true;
        }
   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOnTrigger = false;
        }
    }
}

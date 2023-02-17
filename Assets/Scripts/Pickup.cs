using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public static event Action OnPickupGrab;
    public static event Action OnKeyGrab;
    void Grab()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Grab();
            if (gameObject.CompareTag("Pickup"))
            {
                OnPickupGrab?.Invoke();
            }
            else if (gameObject.CompareTag("Key"))
            {
                OnKeyGrab?.Invoke();
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCompleted : MonoBehaviour
{
    public static event Action OnEndLevel;
    public static event Action OnCollectMore;
    public GameObject loveEnding;
    public int endGameTime;
    bool hasAllPickupCollected = false;

    private void Awake()
    {
        ShowPickup.OnAllPickupCollected += UnlockLevel;
    }

    private void OnDestroy()
    {
        ShowPickup.OnAllPickupCollected -= UnlockLevel;
    }

    void UnlockLevel()
    {
        hasAllPickupCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boom");
            if (hasAllPickupCollected)
            {
                loveEnding.SetActive(true);
                Invoke("EndLevel", endGameTime);
            }
            else
            {
                OnCollectMore?.Invoke();
            }
        }
    }

    void EndLevel()
    {
        OnEndLevel?.Invoke();
    }
}

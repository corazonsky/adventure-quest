using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowPickup : MonoBehaviour
{
    public static event Action OnAllPickupCollected;
    private int pickups;
    private int totalPickups;
    public Image[] items;

    private void Awake()
    {
        Pickup.OnPickupGrab += PickupCollected;
    }

    private void OnDestroy()
    {
        Pickup.OnPickupGrab -= PickupCollected;
    }

    private void Start()
    {
        pickups = 0;
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }

    private void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
           
            if (i < pickups)
            {
                items[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                items[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, .4f);
            }
            if (i < totalPickups)
            {
                items[i].enabled = true;
            }
            else
            {
                items[i].enabled = false;
            }
        }

        if(pickups == totalPickups)
        {
            OnAllPickupCollected?.Invoke();
        }
    }

    void PickupCollected()
    {
        pickups++;
        Debug.Log(pickups);
    }

}

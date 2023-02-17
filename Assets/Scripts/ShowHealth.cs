using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    private int health;
    private int maxHealth;

    public Image[] items;


    private void Start()
    {
        health = GameManager.GetManager().health;
        maxHealth = GameManager.GetManager().maxHealth;
    }
    private void Update()
    {
        for(int i = 0; i < items.Length; i++)
        {
            if( i >= health)
            {
                items[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, .4f);
            }
            if(i < maxHealth)
            {
                items[i].enabled = true;
            }
            else
            {
                items[i].enabled = false;
            }
        }
    }
}

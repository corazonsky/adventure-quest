using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockedDoor : MonoBehaviour
{
    public GameObject key;
    public GameObject door;
    Animator anim;
    BoxCollider2D boxCollider;

    private void Start()
    {
        anim = door.GetComponent<Animator>();
        boxCollider = door.GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        if(key == null)
        {
            anim.SetBool("IsOpen", true);
            boxCollider.enabled = false;

        }
    }
}

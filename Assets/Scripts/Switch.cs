using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool IsOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator anim = gameObject.GetComponent<Animator>();
        if(collision.gameObject.tag == "Player")
        {
            anim.SetBool("IsOn", true);
            IsOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Animator anim = gameObject.GetComponent<Animator>();
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("IsOn", false);
            IsOn = false;
        }
    }
}

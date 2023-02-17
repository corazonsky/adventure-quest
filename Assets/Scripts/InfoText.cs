using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    Text pickupText;
    public int textShowTime = 3;

    private void Awake()
    {
        Pickup.OnPickupGrab += ShowPickupsText;
        Pickup.OnKeyGrab += ShowKeyText;
        Door.OnCollectMore += ShowCollectMoreText;
        GameCompleted.OnCollectMore += ShowCollectMoreText;
    }

    private void OnDestroy()
    {
        Pickup.OnPickupGrab -= ShowPickupsText;
        Pickup.OnKeyGrab -= ShowKeyText;
        Door.OnCollectMore -= ShowCollectMoreText;
        GameCompleted.OnCollectMore -= ShowCollectMoreText;
    }

    private void Start()
    {
        pickupText = gameObject.GetComponent<Text>();
    }


    void ShowPickupsText()
    {
        pickupText.text = "Pickup collected!";
        Invoke("Delete", textShowTime);
    }

    void ShowKeyText()
    {

        pickupText.text = "Key collected!, Steel door opened";
        Invoke("Delete", textShowTime);
    }

    void ShowCollectMoreText()
    {
        pickupText.text = "Collect all carrots to proceed!";
        Invoke("Delete", textShowTime);
    }

    void Delete()
    {
        pickupText.text = "";
    }
}

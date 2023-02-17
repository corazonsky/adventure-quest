using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupManager : MonoBehaviour
{
    public static Action OnRespawnClicked;
    public GameObject tutorialImage;
    public GameObject respawnImage;


    private void Awake()
    {
        PlayerDead.OnPlayerDead += ShowRespawnImage;
    }

    private void OnDestroy()
    {
        PlayerDead.OnPlayerDead -= ShowRespawnImage;
    }

    private void Start()
    {
        if(GameManager.GetManager().currentLevel == 1)
        {
            tutorialImage.SetActive(true);
        }
     
        respawnImage.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            tutorialImage.SetActive(false);
        }
    }

    public void ShowRespawnImage()
    {
        if(GameManager.GetManager().health > 1)
        {
            respawnImage.SetActive(true);
        }
        else
        {
            OnRespawnClicked?.Invoke();
        }
    }

    public void HideTutorialImage()
    {
        tutorialImage.SetActive(false);
    }

    public void Respawn()
    {
        OnRespawnClicked?.Invoke();
        respawnImage.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GetManager()
    {
        return currentManager;
    }

    static GameManager currentManager = null;


    [Range(1,5)]
    public int maxHealth;
    [HideInInspector]
    public int health;
    public int respawnTime;
    public int numberOfStage;
    public int currentLevel = 1;

    private void Awake()
    {
        if (currentManager == null)
        {
            currentManager = this;
            DontDestroyOnLoad(gameObject);
            PopupManager.OnRespawnClicked += Respawn; //call event if subscribed
            Door.OnEndLevel += PlayerFinish;
            GameCompleted.OnEndLevel += PlayerFinish;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void OnDestroy()
    {
        if (currentManager == this)
        {
            currentManager = null;
            PopupManager.OnRespawnClicked -= Respawn; //unsubscribe
            Door.OnEndLevel -= PlayerFinish;
            GameCompleted.OnEndLevel -= PlayerFinish;
        }
    }

    void Respawn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        health--;
        if (health > 0)
        {
            Debug.Log(health);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart screen
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            Reset();
        }
    }

    void PlayerFinish()
    {
        Invoke("NextLevel", respawnTime);
    }

    void NextLevel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentLevel++;
        if (currentLevel <= numberOfStage)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    private void Reset()
    {
        Destroy(gameObject);
    }
}
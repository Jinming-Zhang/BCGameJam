using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField]
    float gameDurationInSec = 120;

    private float timeCounter;
    private bool gameRunning;

    private void Update()
    {
        if (gameRunning)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > gameDurationInSec)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
    }

    [ContextMenu("Test Restart")]
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StartGame()
    {
        gameRunning = true;
    }
}

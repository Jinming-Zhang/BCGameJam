using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
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

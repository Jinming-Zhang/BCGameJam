using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance;


    [Header("Game Ending settings")]
    [SerializeField]
    GameObject endingBackground;
    [SerializeField]
    Transform endingBackgroundDistination;
    [SerializeField]
    float slidingSpeed = 1f;

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

    [ContextMenu("End Restart")]
    public void EndGame()
    {
        StartCoroutine(MovingEndingInCR());
        IEnumerator MovingEndingInCR()
        {
            Vector3 distinationPosition = endingBackgroundDistination.transform.position;
            while (Vector3.Distance(endingBackground.transform.position, distinationPosition) > .001f)
            {
                endingBackground.transform.position = Vector3.MoveTowards(endingBackground.transform.position, distinationPosition, slidingSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
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

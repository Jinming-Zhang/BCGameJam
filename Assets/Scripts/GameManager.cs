using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]
    LandScroller landScroller;
    [SerializeField]
    float landInSpeed;
    //[SerializeField]
    //float midlandSlidSpeed = 7;

    [Header("Game Ending settings")]
    [SerializeField]
    GameObject endingBackground;
    [SerializeField]
    Transform endingBackgroundDistination;
    [SerializeField]
    float slidingSpeed = 1f;

    [SerializeField]
    float pausingDuration = 2f;

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
    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (gameRunning)
        {
            timeCounter += Time.deltaTime;
            UIManager.Instance.UpdateCountdownTimer(gameDurationInSec - timeCounter);
            if (timeCounter > gameDurationInSec)
            {
                gameRunning = false;
                EndGame();
            }
        }
    }

    [ContextMenu("End Game")]
    public void EndGame()
    {
        StartCoroutine(MovingEndingInCR());
        IEnumerator MovingEndingInCR()
        {
            Vector3 pos = endingBackgroundDistination.transform.position;
            Vector3 distinationPosition = new Vector3(pos.x, pos.y, endingBackground.transform.position.z);
            while (Vector3.Distance(endingBackground.transform.position, distinationPosition) > .001f)
            {
                endingBackground.transform.position = Vector3.MoveTowards(endingBackground.transform.position, distinationPosition, slidingSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(pausingDuration);
            ShowResultScreen();
        }
    }

    public void ShowResultScreen()
    {

    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        gameRunning = true;
    }

    [ContextMenu("MovingIn Land")]
    public void MovingInLand()
    {
        StartCoroutine(MovingLandInCR());
        IEnumerator MovingLandInCR()
        {
            float distX = endingBackgroundDistination.transform.position.x;
            Vector3 direction = (endingBackgroundDistination.transform.position - landScroller.PivotPos).normalized;
            direction.y = 0;
            direction.z = 0;
            landScroller.StopLooping();
            while (Mathf.Abs(landScroller.PivotPos.x - distX) > 0.1f)
            {
                landScroller.transform.position += direction * landInSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            landScroller.StartLooping();
            yield return new WaitForSeconds(8);
            MovingOutLand();
        }


    }
    [ContextMenu("Moving out land")]
    public void MovingOutLand()
    {
        StartCoroutine(MovingOutLandCR());
        IEnumerator MovingOutLandCR()
        {
            landScroller.StopLooping();
            while (landScroller.transform.position.x > -80)
            {
                Vector3 newPos = landScroller.transform.position;
                newPos.x -= landInSpeed * Time.deltaTime;
                landScroller.transform.position = newPos;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

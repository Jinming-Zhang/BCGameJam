using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [Header("Game Ending settings")]
    [SerializeField]
    GameObject endingBackground;
    [SerializeField]
    Transform endingBackgroundDistination;
    [SerializeField]
    float slidingSpeed = 1f;

    [SerializeField]
    float pausingDuration = 2f;

    [SerializeField]
    string endingSceneWinningString = "Ending";

    [SerializeField]
    string endingSceneLoseString = "Ending";

    string endingSceneFinalString;
    [SerializeField]
    CanvasGroup hudCg;

    [SerializeField]
    Texture2D defaultEndingTexture;
    [SerializeField]
    Texture2D successfulEndingTexture;
    [SerializeField]
    Material endingBackgroundMaterial;

    public AudioClip gameEndClip;
    [SerializeField]
    AudioClip winClip;
    [SerializeField]
    AudioClip loseClip;

    bool gameEnded = false;
    bool shakingCamera = false;
    [SerializeField]
    float shakeDuration = .5f;
    [SerializeField]
    CinemachineVirtualCamera vc;
    [SerializeField]
    float shakeIntensity;
    [SerializeField]
    PersistantNumber finalScore;


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
        gameEnded = false;
    }

    [SerializeField]
    float gameDurationInSec = 200;

    private float timeCounter;
    private bool gameRunning;

    bool movedInLand;
    bool movedOutLand;
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
            if (timeCounter > gameDurationInSec / 3f && !movedInLand)
            {
                movedInLand = true;
                MovingInLand();
            }
            else if (timeCounter > gameDurationInSec * 0.666f && !movedOutLand)
            {
                movedOutLand = true;
                MovingOutLand();
            }
        }
    }
    public void ChangeEndingMaterial(bool success)
    {
        if (success)
        {
            endingBackgroundMaterial.mainTexture = successfulEndingTexture;
            gameEndClip = winClip;
            endingSceneFinalString = endingSceneWinningString;
        }
        else
        {
            endingBackgroundMaterial.mainTexture = defaultEndingTexture;
            gameEndClip = loseClip;
            endingSceneFinalString = endingSceneLoseString;
        }
    }

    [ContextMenu("End Game")]
    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            StartCoroutine(MovingEndingInCR());
            StartCoroutine(FadingOutCanvasCR());
            BGMusic.Instance.PlayMusic(gameEndClip);
            finalScore.Score = getPlayerScore();
        }
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

        IEnumerator FadingOutCanvasCR()
        {
            hudCg.alpha = 1;
            while (hudCg.alpha > 0)
            {
                hudCg.alpha -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    float getPlayerScore()
    {
        int lv = TornandoPlayerController.Instance.PowerupCount;
        int xp = TornandoPlayerController.Instance.GetComponent<PowerLevel>().currentXP;
        return lv * 200 + xp;
    }
    public void ShowResultScreen()
    {
        SceneManager.LoadScene(endingSceneFinalString);
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

    public void ShakeCamera()
    {
        if (!shakingCamera)
        {
            shakingCamera = true;
            StartCoroutine(shakeItOff());
        }
        IEnumerator shakeItOff()
        {
            CinemachineBasicMultiChannelPerlin noisy = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noisy.m_AmplitudeGain = shakeIntensity;
            yield return new WaitForSeconds(shakeDuration);
            noisy.m_AmplitudeGain = 0;
            shakingCamera = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Sprite paris;
    [SerializeField] Sprite parisDestroy;
    [SerializeField] Sprite newYork;
    [SerializeField] Sprite newYorkDestroy;
    [SerializeField] Sprite darkGod;
    [SerializeField] Sprite newsPaper;
    private int randomNo;
    [SerializeField] GameObject darkGodLaugh;

    [SerializeField]
    List<Sprite> slideShow;
    //private bool isPlayEnding = false;

    // Start is called before the first frame update
    [SerializeField]
    FinalScoreText scoreText;
    bool listenToRestart = false;
    void Start()
    {
        scoreText.gameObject.SetActive(false);
        StartCoroutine(SlideShowCR());
        listenToRestart = false;
    }
    IEnumerator SlideShowCR()
    {
        for (int i = 0; i < slideShow.Count; i++)
        {
            background.GetComponent<Image>().sprite = slideShow[i];
            yield return new WaitForSeconds(5f);
        }
        FlashDarkGod();
        yield return new WaitForSeconds(3f);
        ShowScore();
        listenToRestart = true;
    }

    void ShowScore()
    {
        background.GetComponent<Image>().sprite = newsPaper;
        scoreText.gameObject.SetActive(true);
        scoreText.Refresh();
    }
    // Update is called once per frame
    void Update()
    {
        if (listenToRestart)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                BGMusic.Instance.Restart();
                SceneManager.LoadScene(0);
            }
        }
        //if (randomNo == 0 && !isPlayEnding)
        //{
        //    Debug.Log("Ending 1");
        //    isPlayEnding = true;
        //    StartCoroutine(Ending1());
        //}
        //if (randomNo > 0 && !isPlayEnding)
        //{
        //    Debug.Log("Ending 2");
        //    isPlayEnding = true;
        //    StartCoroutine(Ending2());
        //}

    }

    public IEnumerator Ending1()
    {
        background.GetComponent<Image>().sprite = paris;
        yield return new WaitForSeconds(5.0f);
        FlashDarkGod();
        yield return new WaitForSeconds(3.0f);
        background.GetComponent<Image>().sprite = parisDestroy;
        yield return new WaitForSeconds(5.0f);
        background.GetComponent<Image>().sprite = newsPaper;

    }
    public IEnumerator Ending2()
    {
        background.GetComponent<Image>().sprite = newYork;
        yield return new WaitForSeconds(5.0f);
        FlashDarkGod();
        yield return new WaitForSeconds(3.0f);
        background.GetComponent<Image>().sprite = newYorkDestroy;
        yield return new WaitForSeconds(5.0f);
        background.GetComponent<Image>().sprite = newsPaper;
    }

    void FlashDarkGod()
    {
        darkGodLaugh.GetComponent<AudioSource>().Play();
        background.GetComponent<Image>().sprite = darkGod;
    }

}

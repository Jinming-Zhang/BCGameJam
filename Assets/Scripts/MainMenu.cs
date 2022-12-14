using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string loadingScreen;
    private GameObject butterflyStage;
    private Image image;
    [SerializeField] GameObject butterfly;
    [SerializeField] GameObject cocoon;
    [SerializeField] GameObject cocoonHatch;
    private bool isStart;
    [SerializeField] float stageTimer;

    [SerializeField] GameObject crack;
    private AudioSource crackSound;

    void Start()
    {
        SetButterflyStage(cocoon);
        crackSound = crack.GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        if (!isStart)
        {
            isStart = true;
            StartCoroutine(ChangeButterfly());
        }
    }

    public IEnumerator ChangeButterfly()
    {
        PlayCrackSound();
        RemoveButterflyStage(cocoon);
        SetButterflyStage(cocoonHatch);
        yield return new WaitForSeconds(stageTimer);
        PlayCrackSound();
        RemoveButterflyStage(cocoonHatch);
        SetButterflyStage(butterfly);
        yield return new WaitForSeconds(stageTimer);
        SceneManager.LoadScene(loadingScreen);
    }

    void SetButterflyStage(GameObject stage)
    {
        float transparency = 0;
        butterflyStage = stage;
        image = butterflyStage.GetComponent<Image>();
        var tempColor = image.color;

        while (transparency < 1.0f)
        {
            tempColor.a = transparency;
            image.color = tempColor;

            transparency += Time.deltaTime / 1000;
        }
    }
    void RemoveButterflyStage(GameObject stage)
    {
        float transparency = 1;
        butterflyStage = stage;
        image = butterflyStage.GetComponent<Image>();
        var tempColor = image.color;

        while (transparency > 0.0f)
        {
            tempColor.a = transparency;
            image.color = tempColor;

            transparency -= Time.deltaTime / 1000;
        }
    }

    void PlayCrackSound()
    {
        crackSound.Play();
    }
}

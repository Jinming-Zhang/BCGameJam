using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    [SerializeField] float loadingTimer = 0.0f;
    [SerializeField] string mainGame;
    [SerializeField] GameObject gustSound;
    private AudioSource audioSound;

    [SerializeField] Sprite butterfly1;
    [SerializeField] Sprite butterfly2;

    [SerializeField] Button button;

    [SerializeField] GameObject tornado;

    // Start is called before the first frame update
    void Start()
    {
        audioSound = gustSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TransitionToMainGame();
        UpdateButterflyImage();
        UpdateTornadoAnimSize();
    }

    public void IncreaseLoadingTimer()
    {
        loadingTimer += 1;
        loadingSlider.GetComponent<Slider>().value = loadingTimer;
        Debug.Log(loadingSlider.GetComponent<Slider>().value);
        playSound();
    }

    public void playSound()
    {
        audioSound.Play();
    }

    public void StopMenuMusic()
    {
        if (GameObject.FindGameObjectWithTag("BG Music") != null)
        {
            GameObject.FindGameObjectWithTag("BG Music").GetComponent<BGMusic>().StopMusic();
        }
    }

    private void TransitionToMainGame()
    {
        if (loadingTimer >= 10)
        {
            StopMenuMusic();
            //Debug.Log("Load main game");
            SceneManager.LoadScene(mainGame);
        }
    }

    private void UpdateButterflyImage()
    {
        float imageNo = loadingTimer % 2;
        if(imageNo != 0)
        {
            button.GetComponent<Image>().sprite = butterfly2; //load image 2
        }
        else
        {
            //load image 1
            button.GetComponent<Image>().sprite = butterfly1;
        }
    }

    private void UpdateTornadoAnimSize()
    {
        float scale = loadingTimer / 10;
        tornado.transform.localScale = new Vector3(scale, scale, 0);
    }    
}

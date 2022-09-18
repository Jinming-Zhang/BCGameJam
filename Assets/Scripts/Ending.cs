using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private bool isPlayEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Image>().sprite = paris;
        randomNo = Random.Range(0, 1);
        Debug.Log(randomNo);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomNo == 0 && !isPlayEnding)
        {
            Debug.Log("Ending 1");
            isPlayEnding = true;
            StartCoroutine(Ending1());
        }
        if(randomNo > 0 && !isPlayEnding)
        {
            Debug.Log("Ending 2");
            isPlayEnding = true;
           StartCoroutine(Ending2());
        }
        
    }

    public IEnumerator Ending1()
    {
        background.GetComponent<Image>().sprite = paris;
        yield return new WaitForSeconds(2.0f);
        FlashDarkGod();
        yield return new WaitForSeconds(2.0f);
        background.GetComponent<Image>().sprite = parisDestroy;
        yield return new WaitForSeconds(2.0f);
        background.GetComponent<Image>().sprite = newsPaper;

    }
    public IEnumerator Ending2()
    {
        background.GetComponent<Image>().sprite = newYork;
        yield return new WaitForSeconds(2.0f);
        FlashDarkGod();
        yield return new WaitForSeconds(2.0f);
        background.GetComponent<Image>().sprite = newYorkDestroy;
        yield return new WaitForSeconds(2.0f);
        background.GetComponent<Image>().sprite = newsPaper;
    }

    void FlashDarkGod()
    {
        darkGodLaugh.GetComponent<AudioSource>().Play();
        background.GetComponent<Image>().sprite = darkGod;
    }

}

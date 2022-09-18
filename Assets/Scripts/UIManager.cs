using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField]
    GameObject hudUI;
    [SerializeField]
    GameObject resultUI;


    [SerializeField]
    TextMeshProUGUI countdownText;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCountdownTimer(float value)
    {
        value = Mathf.Max(value, 0);
        countdownText.text = value.ToString("0.00");
    }
}

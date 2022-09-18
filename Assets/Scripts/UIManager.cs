using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] GameObject hudUI;
    [SerializeField] GameObject resultUI;
    [SerializeField] private GameObject gameAreaWorldMin;
    [SerializeField] private GameObject gameAreaWorldMax;


    [SerializeField] TextMeshProUGUI countdownText;

    public Vector2 ViewportMin => gameAreaWorldMin == null
        ? Vector2.zero
        : Camera.main.WorldToViewportPoint(gameAreaWorldMin.transform.position);
    public Vector2 ViewportMax => gameAreaWorldMax == null
        ? Vector2.one
        : Camera.main.WorldToViewportPoint(gameAreaWorldMax.transform.position);


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
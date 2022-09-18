using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] GameObject hudUI;
    [SerializeField] GameObject resultUI;
    [SerializeField] private RectTransform gameArea;


    [SerializeField] TextMeshProUGUI countdownText;

    public Vector2 ViewportMin { get; private set; }
    public Vector2 ViewportMax { get; private set; }

    private void setViewportMinMax()
    {
        var viewportCorners = new Vector3[4];
        gameArea.GetWorldCorners(viewportCorners); // It is screen corners, as RectTransform.position is screen space

        var minDist = 1f;
        var maxDist = 0f;
        var vMin = Vector3.zero;
        var vMax = Vector3.one;
        for (var i = 0; i < 4; i++)
        {
            viewportCorners[i] *= -1f;
            viewportCorners[i] = Camera.main.ScreenToViewportPoint(viewportCorners[i]);
            viewportCorners[i].y = 1f - viewportCorners[i].y;
            var dist = Vector3.Distance(viewportCorners[i], Vector3.zero);
            if (dist > maxDist)
            {
                maxDist = dist;
                vMax = viewportCorners[i];
            }
            else if (dist < minDist)
            {
                minDist = dist;
                vMin = viewportCorners[i];
            }
        }

        ViewportMin = vMin;
        ViewportMax = vMax;
    }

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
        setViewportMinMax();
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
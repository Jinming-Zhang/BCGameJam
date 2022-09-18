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
    [SerializeField] private RectTransform gameArea;


    [SerializeField] TextMeshProUGUI countdownText;

    public Vector2 ViewportMin { get; private set; } = Vector2.zero;
    public Vector2 ViewportMax { get; private set; } = Vector2.one;

    private void setViewportMinMax()
    {
        var viewportCorners = new Vector3[4];
        gameArea.GetWorldCorners(viewportCorners); // It is screen corners, as RectTransform.position is screen space
        Debug.Log(gameArea.localPosition);

        var minDist = 1f;
        var maxDist = 0f;
        var vMin = Vector3.zero;
        var vMax = Vector3.one;
        for (var i = 0; i < 4; i++)
        {
            viewportCorners[i] *= -1f;
            Debug.Log(viewportCorners[i]);
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

        // var screenDimension = new Vector3(gameArea.rect.width, gameArea.rect.height, 0);
        // vMin = gameArea.anchoredPosition3D - screenDimension * .5f - gameArea.localPosition;
        // vMin = Camera.main.ScreenToViewportPoint(vMin);
        // vMax = gameArea.anchoredPosition3D + screenDimension * .5f - gameArea.localPosition;
        // vMax = Camera.main.ScreenToViewportPoint(vMax);
        
        // Vector2 size= Vector2.Scale(gameArea.rect.size, gameArea.lossyScale);
        // float x= transform.position.x + gameArea.anchoredPosition.x;
        // float y= Screen.height - gameArea.position.y - gameArea.anchoredPosition.y;
        // var screenRect = new Rect(x, y, size.x, size.y);
        
        //  Vector2 size = Vector2.Scale(gameArea.rect.size, gameArea.lossyScale);
        //  float x = gameArea.position.x - (gameArea.pivot.x * size.x);
        //  float y = gameArea.position.y - ((1.0f - gameArea.pivot.y) * size.y);
        //  var screenRect= new Rect(x, y, size.x, size.y);
        // vMax =   -1 * Camera.main.ScreenToViewportPoint( screenRect.min );
        // vMin = -1 *  Camera.main.ScreenToViewportPoint(screenRect.max);

        ViewportMin = vMin;
        ViewportMax = vMax;
        Debug.Log($"min:{ViewportMin}, max:{ViewportMax}");
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
        // setViewportMinMax();
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;
    [SerializeField] private float initialPowerupCount = 1;
    [SerializeField] private float scaleStep = .1f;
    [SerializeField] private float scaleMax = 2f;
    [SerializeField] private float scaleMin = .1f;

    private BoxCollider2D box2D;
    private Vector2 viewportMin;
    private Vector2 viewportMax;

    public float PowerupCount { get; private set; }


    void Start()
    {
        PowerupCount = initialPowerupCount;
        viewportMin = UIManager.Instance.ViewportMin;
        viewportMax = UIManager.Instance.ViewportMax;
        box2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        if (Mathf.Approximately(move.magnitude, 0f)) return;

        var colliderOffset = new Vector3(box2D.size.x * .5f, box2D.size.y *.5f, 0);
        var tryMoveMax = gameObject.transform.position + colliderOffset + move * Time.deltaTime;
        var tryMoveMin = gameObject.transform.position - colliderOffset + move * Time.deltaTime;
        
        
        
        tryMoveMax = Camera.main.WorldToViewportPoint(tryMoveMax);
        tryMoveMin = Camera.main.WorldToViewportPoint(tryMoveMin);
        
        if (tryMoveMax.x > UIManager.Instance.ViewportMax.x || tryMoveMin.x < UIManager.Instance.ViewportMin.x) move.x = 0;
        if (tryMoveMax.y > UIManager.Instance.ViewportMax.y || tryMoveMin.y < UIManager.Instance.ViewportMin.y) move.y = 0;
        // Debug.Log($"move: {move}, trymin:{tryMoveMin}, trymax:{tryMoveMax}, viewport min: {UIManager.Instance.ViewportMin}, viewport max:{UIManager.Instance.ViewportMax}");
        
        // characterController.Move(move * Time.deltaTime);
        transform.position += move * Time.deltaTime;

    }

    public override void DoPowerup(float value)
    {
        PowerupCount += value;
        PowerupCount = Mathf.Max(0, PowerupCount);
        Debug.Log($"Player powerup count: {PowerupCount}");
        if (PowerupCount < 0) DoDie();
        var newLocalScale = transform.localScale;
        newLocalScale += Vector3.one * scaleStep * (value >= 0 ? 1 : -1);
        newLocalScale.x = Mathf.Clamp(newLocalScale.x, scaleMin, scaleMax);
        newLocalScale.y = Mathf.Clamp(newLocalScale.y, scaleMin, scaleMax);
        newLocalScale.z = transform.localScale.z;
        transform.localScale = newLocalScale;
    }

    private void DoDie()
    {
        Debug.Log("Player Died");
        GameManager.Instance.EndGame();
    }
}
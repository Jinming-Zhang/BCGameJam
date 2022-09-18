using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Collider))]
public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;
    [SerializeField] private float initialPowerupCount = 1;

    private CharacterController characterController;

    public float PowerupCount { get; private set; }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        PowerupCount = initialPowerupCount;
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        if (move.magnitude <= 0f) return;
        // Debug.Log($"move: {move}");

        var tryMoveMax = gameObject.transform.position +
                         new Vector3(characterController.radius, characterController.height * .5f, 0) +
                         move * Time.deltaTime;

        var tryMoveMin = gameObject.transform.position -
                         new Vector3(characterController.radius, characterController.height * .5f, 0) +
                         move * Time.deltaTime;

        tryMoveMax = Camera.main.WorldToViewportPoint(tryMoveMax);
        tryMoveMin = Camera.main.WorldToViewportPoint(tryMoveMin);
        
        if (tryMoveMax.x > UIManager.Instance.ViewportMax.x || tryMoveMin.x < UIManager.Instance.ViewportMin.x) move.x = 0;
        if (tryMoveMax.y > UIManager.Instance.ViewportMax.y || tryMoveMin.y < UIManager.Instance.ViewportMin.y) move.y = 0;

        characterController.Move(move * Time.deltaTime);
    }

    public override void DoPowerup(float value)
    {
        PowerupCount += value;
        Debug.Log($"Player powerup count: {PowerupCount}");
        if (PowerupCount < 0) DoDie();
    }

    private void DoDie()
    {
        Debug.Log("Player Died");
        GameManager.Instance.EndGame();
    }
}
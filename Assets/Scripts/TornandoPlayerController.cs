using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
// [RequireComponent(typeof(Collider))]
public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;
    [SerializeField] private float initialPowerupCount = 1;

    private CharacterController characterController;
    private Vector2 viewportMin;
    private Vector2 viewportMax;

    public float PowerupCount { get; private set; }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        PowerupCount = initialPowerupCount;
        viewportMin = UIManager.Instance.ViewportMin;
        viewportMax = UIManager.Instance.ViewportMax;
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        if (Mathf.Approximately(move.magnitude, 0f)) return;

        var colliderOffset = new Vector3(characterController.radius, characterController.height * .5f, 0);
        var tryMoveMax = gameObject.transform.position + colliderOffset + move * Time.deltaTime;
        var tryMoveMin = gameObject.transform.position - colliderOffset + move * Time.deltaTime;
        
        
        
        tryMoveMax = Camera.main.WorldToViewportPoint(tryMoveMax);
        tryMoveMin = Camera.main.WorldToViewportPoint(tryMoveMin);
        
        if (tryMoveMax.x > UIManager.Instance.ViewportMax.x || tryMoveMin.x < UIManager.Instance.ViewportMin.x) move.x = 0;
        if (tryMoveMax.y > UIManager.Instance.ViewportMax.y || tryMoveMin.y < UIManager.Instance.ViewportMin.y) move.y = 0;
        Debug.Log($"move: {move}, trymin:{tryMoveMin}, trymax:{tryMoveMax}, viewport min: {UIManager.Instance.ViewportMin}, viewport max:{UIManager.Instance.ViewportMax}");
        
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
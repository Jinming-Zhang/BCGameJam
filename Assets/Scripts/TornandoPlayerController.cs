using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController ))]
[RequireComponent(typeof(Collider))]
public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;

    private CharacterController characterController;
    
    public int PowerupCount { get; private set; }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        characterController.Move(move * Time.deltaTime);
    }

    public override void DoPowerup(float value)
    {
        PowerupCount++;
        Debug.Log($"Powerup count: {PowerupCount}");
    }
    
}

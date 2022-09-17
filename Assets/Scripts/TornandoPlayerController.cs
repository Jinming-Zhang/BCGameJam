using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController ))]
public class TornandoPlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;

    private CharacterController characterController;

    private int powerupCount = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        characterController.Move(move * Time.deltaTime);
    }

    public void DoPowerup(float value)
    {
        powerupCount++;
        Debug.Log($"Powerup count: {powerupCount}");
    }
    
}

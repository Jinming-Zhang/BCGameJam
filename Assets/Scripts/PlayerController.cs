using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController ))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        characterController.Move(move * Time.deltaTime);
    }
    
}

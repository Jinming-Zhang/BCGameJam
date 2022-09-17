using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController ))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        // if (Input.GetKey(KeyCode.D))
        // {
        //     gameObject.transform.position += Vector3.right * speedPerSec * Time.deltaTime;
        // }
        // else if (Input.GetKey(KeyCode.A))
        // {
        //     gameObject.transform.position += Vector3.left * speedPerSec * Time.deltaTime;
        // }
        
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        characterController.Move(move * Time.deltaTime * playerSpeed);
    }
}

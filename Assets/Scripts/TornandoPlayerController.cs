using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof( CharacterController ))]
[RequireComponent(typeof(Collider))]
public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;

    private CharacterController characterController;
    
    public float PowerupCount { get; private set; }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        var tryMove = gameObject.transform.position + move * Time.deltaTime;
        // TODO: sprite width height
        tryMove = Camera.main.WorldToViewportPoint(tryMove);
        if (tryMove.x is > 1 or < 0 || tryMove.y is < 0 or > 1)
        {
            return;
        }
        characterController.Move(move * Time.deltaTime);
    }

    public override void DoPowerup(float value)
    {
        PowerupCount += value;
        if (PowerupCount <= 0) DoDie();
    }

    private void DoDie()
    {
        GameManager.Instance.EndGame();
    }
    
}

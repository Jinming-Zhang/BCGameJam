using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour, IPowerupable
{
    [SerializeField] private int powerValue;


        //if player >= powerValue
            //PowerUp
        //else
            //PowerDown
    enum PowerUpType
    {
        Speed = 1,
        Size = 2,
        Force = 3
    }
    [SerializeField] PowerUpType powerUpType;
    public void ForceUp()
    {
        throw new System.NotImplementedException();
    }

    public void SizeUp()
    {
        throw new System.NotImplementedException();
    }

    public void SpeedUp()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var playerController = other.gameObject.GetComponent<TornandoPlayerController>();
        if (playerController == null) return;
        int playerPowerValue = playerController.PowerupCount;
        if (playerPowerValue < powerValue)
        {
            playerController.DoPowerup(-1);
        }
        else {
            playerController.DoPowerup(1);
        }
        Destroy(this.gameObject);
    }
}

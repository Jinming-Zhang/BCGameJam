using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour, IPowerupable
{
    [SerializeField] private int powerValue;
    [SerializeField] private float speed = 1f;
    

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
    
    
    [Header("Debug")]
    [SerializeField] private bool dontDestroyAfterTrigger;
    
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var playerController = other.gameObject.GetComponent<TornandoPlayerController>();
        if (playerController == null) return;
        float playerPowerValue = playerController.PowerupCount;
        if (playerPowerValue < powerValue)
        {
            playerController.DoPowerup(-1);
        }
        else {
            playerController.DoPowerup(1);
        }

        if (dontDestroyAfterTrigger) return;
        Destroy(this.gameObject);
    }
    void Update()
    {
        transform.Translate(speed * Vector3.left * Time.deltaTime);
    }
}

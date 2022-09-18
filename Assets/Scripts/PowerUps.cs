using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour, IPowerupable
{
    [SerializeField] private int powerValue;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float currSpeed;
    private float lifeTime;
    private float totalLifeTime = 10;
    public int countDownTime = 3;
   
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
    
    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }
    private void Awake()
    {
        currSpeed = Random.Range(minSpeed, maxSpeed);
        
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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<TornandoPlayerController>();
            if (playerController == null) return;
            float playerPowerValue = playerController.PowerupCount;
            if (playerPowerValue < powerValue)
            {
                playerController.DoPowerup(-1);
            }
            else
            {
                playerController.DoPowerup(1);
            }

            if (dontDestroyAfterTrigger) return;
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("DeathZone")) 
        {
           // Debug.Log("Touched the death zone!");
            this.gameObject.SetActive(false);
        }
        
    }
    void Update()
    {
      transform.Translate(currSpeed * Vector3.left * Time.deltaTime);

    }
   
   
}

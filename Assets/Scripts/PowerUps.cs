using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour, IPowerupable
{
    public int powerValue;
    public int powerExperience;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float currSpeed;
    private float lifeTime;
    private float totalLifeTime = 10;
    public int countDownTime = 3;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject caneatBG;
    [SerializeField]
    GameObject donteatBG;
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

        spriteRenderer.sortingOrder = 5;

        caneatBG = new GameObject();
        caneatBG.transform.parent = transform;
        caneatBG.transform.position = transform.position;
        caneatBG.transform.localScale = Vector3.one * 1.1f;
        SpriteRenderer caneatSR = caneatBG.AddComponent<SpriteRenderer>();
        caneatSR.sprite = spriteRenderer.sprite;
        caneatSR.sortingOrder = 0;
        caneatSR.color = Color.green;


        donteatBG = new GameObject();
        donteatBG.transform.parent = transform;
        donteatBG.transform.position = transform.position;
        donteatBG.transform.localScale = Vector3.one * 1.1f;
        SpriteRenderer donteatSR = donteatBG.AddComponent<SpriteRenderer>();
        donteatSR.sprite = spriteRenderer.sprite;
        donteatSR.sortingOrder = 0;
        donteatSR.sortingOrder = 0;
        donteatSR.color = Color.red;
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<TornandoPlayerController>();
            if (playerController == null) return;
            PowerLevel powerLevel = other.gameObject.GetComponent<PowerLevel>();
            powerLevel.UpdateXP(powerValue);
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
        UpdateUI(powerValue <= TornandoPlayerController.Instance.PowerupCount);
        var caneatSR = caneatBG.GetComponent<SpriteRenderer>();
        caneatSR.sprite = spriteRenderer.sprite;
        SpriteRenderer donteatSR = donteatBG.GetComponent<SpriteRenderer>();
        donteatSR.sprite = spriteRenderer.sprite;
    }

    private void UpdateUI(bool canEat)
    {
        if (caneatBG)
        {
            var caneatSR = caneatBG.GetComponent<SpriteRenderer>();
            caneatSR.sprite = spriteRenderer.sprite;
            caneatSR.sortingOrder = 0;
            caneatSR.color = Color.green;
        }
        else
        {
            SpriteRenderer donteatSR = donteatBG.GetComponent<SpriteRenderer>();
            donteatSR.sprite = spriteRenderer.sprite;
            donteatSR.sortingOrder = 0;
            donteatSR.color = Color.red;
        }
        caneatBG.SetActive(canEat);
        donteatBG.SetActive(!canEat);

    }


}

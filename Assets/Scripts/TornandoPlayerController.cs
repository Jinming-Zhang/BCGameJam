
using UnityEngine;

using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class TornandoPlayerController : PlayerController
{
    [SerializeField] private float playerSpeedX = 1f;
    [SerializeField] private float playerSpeedY = 1f;
    [SerializeField] private int initialPowerupCount = 0;
    [SerializeField] private Vector3 initialScale = new Vector3(1, 1, 1);
    
    [SerializeField] private float scaleStep = .5f;
    [SerializeField] private float scaleMax = 3.5f;
    [SerializeField] private float scaleMin = 1f;
    [SerializeField] private int powerUpMax = 5;

    [SerializeField] private TextMeshProUGUI speedText;

    private BoxCollider2D box2D;
    private Vector2 viewportMin;
    private Vector2 viewportMax;

    public int PowerupCount { get; private set; }
    public int PowerupMax => powerUpMax;


    void Start()
    {
        PowerupCount = initialPowerupCount;
        viewportMin = UIManager.Instance.ViewportMin;
        viewportMax = UIManager.Instance.ViewportMax;
        box2D = GetComponent<BoxCollider2D>();
        transform.localScale = initialScale;
        if (speedText != null) speedText.text = $"{(1 + PowerupCount) * 100}KM/H";
        playerSpeedX = 1 + PowerupCount;
        playerSpeedY = 1 + PowerupCount;
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeedX, Input.GetAxis("Vertical") * playerSpeedY, 0);
        if (Mathf.Approximately(move.magnitude, 0f)) return;

        var colliderOffset = new Vector3(box2D.size.x * .5f, box2D.size.y *.5f, 0);
        var tryMoveMax = gameObject.transform.position + colliderOffset + move * Time.deltaTime;
        var tryMoveMin = gameObject.transform.position - colliderOffset + move * Time.deltaTime;
        
        
        
        tryMoveMax = Camera.main.WorldToViewportPoint(tryMoveMax);
        tryMoveMin = Camera.main.WorldToViewportPoint(tryMoveMin);
        
        if (tryMoveMax.x > UIManager.Instance.ViewportMax.x || tryMoveMin.x < UIManager.Instance.ViewportMin.x) move.x = 0;
        if (tryMoveMax.y > UIManager.Instance.ViewportMax.y || tryMoveMin.y < UIManager.Instance.ViewportMin.y) move.y = 0;
        // Debug.Log($"move: {move}, trymin:{tryMoveMin}, trymax:{tryMoveMax}, viewport min: {UIManager.Instance.ViewportMin}, viewport max:{UIManager.Instance.ViewportMax}");
        
        // characterController.Move(move * Time.deltaTime);
        transform.position += move * Time.deltaTime;

    }

    public override void DoPowerup(int value)
    {
        var isIncrease = value < PowerupCount || PowerupCount == PowerupMax;
        if(isIncrease) PowerupCount++; else PowerupCount--;
        if (PowerupCount < 0) DoDie();
        
        PowerupCount = Mathf.Clamp(PowerupCount, 0, PowerupMax);
        
        Debug.Log($"Player powerup count: {PowerupCount}");
        if (speedText != null) speedText.text = $"{(1+PowerupCount) * 100}KM/H";
        playerSpeedX = 1 + PowerupCount;
        playerSpeedY = 1 + PowerupCount;        
        var newLocalScale = transform.localScale;
        newLocalScale += Vector3.one * scaleStep * (isIncrease ? 1 : -1);
        newLocalScale.x = Mathf.Clamp(newLocalScale.x, scaleMin, scaleMax);
        newLocalScale.y = Mathf.Clamp(newLocalScale.y, scaleMin, scaleMax);
        newLocalScale.z = transform.localScale.z;
        transform.localScale = newLocalScale;
    }

    private void DoDie()
    {
        Debug.Log("Player Died");
        GameManager.Instance.EndGame();
    }
}
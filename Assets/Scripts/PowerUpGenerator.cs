using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpObj;
    [SerializeField] private int coolDownTimer = 3;
    public List<Sprite> sprites;
    //Const
    //private Vector3 xPosition = Camera.main.ViewportToWorldPoint(1,0.5,0);


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolDown());     
    }


   
 

    IEnumerator CoolDown()
    {   float y = Random.Range(0, 1f);
        Vector2 cameraPos = Camera.main.ViewportToWorldPoint(new Vector2(1, y));
        //Instantiate(powerUpObj, new Vector3(cameraPos.x, cameraPos.y, 0),Quaternion.identity);
        Vector2 spawnPos = new Vector2(cameraPos.x, cameraPos.y);
        GameObject powerups = ObjectPooling.SharedInstance.GetPooledObject();
        if (powerups != null)
        {
            powerups.transform.position = spawnPos;
            powerups.transform.rotation = Quaternion.identity;
            SpriteRenderer spriteImage = powerups.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            int index = Random.Range(0, sprites.Count);
            spriteImage.sprite = sprites[index];
            
            powerups.GetComponent<PowerUps>().powerValue=index;
            PolygonCollider2D polyCollider = powerups.GetComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
            Destroy(polyCollider);
            PolygonCollider2D newPolyColliderpowerups = powerups.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
            newPolyColliderpowerups.isTrigger = true;
           
            
            
            powerups.SetActive(true);
        }
        yield return new WaitForSeconds(coolDownTimer);
        StartCoroutine(CoolDown());
    }
}

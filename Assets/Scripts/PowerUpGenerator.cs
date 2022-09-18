using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField] private GameObject powerUpObj;
    [SerializeField] private int coolDownTimer;
    //Const
    //private Vector3 xPosition = Camera.main.ViewportToWorldPoint(1,0.5,0);


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolDown());
    }


    // Update is called once per frame
    void Update()
    {
        
       
    }

    IEnumerator CoolDown()
    {   float y = Random.Range(0, 1f);
        Vector2 cameraPos = Camera.main.ViewportToWorldPoint(new Vector2(1, y));
        Instantiate(powerUpObj, new Vector3(cameraPos.x, cameraPos.y, 0),Quaternion.identity);
        yield return new WaitForSeconds(coolDownTimer);
        StartCoroutine(CoolDown());
    }
}

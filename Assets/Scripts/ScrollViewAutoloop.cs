using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ScrollViewAutoloop : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    ScrollRect scrollRect;
    ScrollRect Scroller
    {
        get
        {
            if (!scrollRect)
            {
                scrollRect = GetComponent<ScrollRect>();
            }
            return scrollRect;
        }
    }
    private void Update()
    {
        float hposition = 0.5f * Mathf.Sin(speed * Time.timeSinceLevelLoad) + 0.5f;
        Scroller.normalizedPosition = new Vector2(hposition, 0);
    }
}

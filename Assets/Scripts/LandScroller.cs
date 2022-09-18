using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScroller : MonoBehaviour
{
    [SerializeField]
    GameObject pivot;
    public Vector3 PivotPos => pivot.transform.position;
    [SerializeField]
    ScrollingTexture midScrolling;
    public void Moveto(Vector3 distination, float slidingSpeed)
    {
    }

    public void StartLooping()
    {
        midScrolling.enabled = true;
    }
    public void StopLooping()
    {
        midScrolling.enabled = false;
    }
}

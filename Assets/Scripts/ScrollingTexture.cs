using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField]
    float hspeed = .5f;
    [SerializeField]
    float vspeed = .5f;

    Renderer bgRenderer;
    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(hspeed * Time.deltaTime, vspeed * Time.deltaTime);
    }
}

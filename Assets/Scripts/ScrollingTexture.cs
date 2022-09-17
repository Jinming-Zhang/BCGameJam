using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField]
    float hspeed = .5f;
    [SerializeField]
    float vspeed = .5f;
    [SerializeField]
    float vAmplitute = .1f;

    Renderer bgRenderer;
    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // hoffset
        bgRenderer.material.mainTextureOffset += new Vector2(hspeed * Time.deltaTime, 0);
        // voffset
        //float vOffset = vAmplitute * Mathf.Sin(vspeed);
        //bgRenderer.material.mainTextureOffset += new Vector2(0, vOffset);
    }
}

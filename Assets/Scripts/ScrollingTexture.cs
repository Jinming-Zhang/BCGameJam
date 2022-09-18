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
    Renderer BgRenderer
    {
        get
        {
            if (!bgRenderer)
            {
                bgRenderer = GetComponent<Renderer>();
            }
            return bgRenderer;
        }
    }
    private void Update()
    {
        // hoffset
        BgRenderer.material.mainTextureOffset += new Vector2(hspeed * Time.deltaTime, 0);
        // voffset
        float vOffset = vAmplitute * Mathf.Sin(vspeed * Time.timeSinceLevelLoad);
        Debug.Log(vOffset);
        BgRenderer.material.mainTextureOffset += new Vector2(0, vOffset);
    }
    public void ResetOffset()
    {
        BgRenderer.material.mainTextureOffset = Vector2.zero;
    }
}

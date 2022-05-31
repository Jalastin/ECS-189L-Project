using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credits: https://www.youtube.com/watch?v=A5YSbgqr3sc
public class BackgroundImageScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material material;

    void Start()
    {
        this.material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Auto scroll background image.
        this.offset += (Time.deltaTime * scrollSpeed) / 10f;
        this.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}

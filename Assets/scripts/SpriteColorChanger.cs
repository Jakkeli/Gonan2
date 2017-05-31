using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorChanger : MonoBehaviour
{

    public Color color = Color.white;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer[] Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in Sprites)
        {
            sprite.color = color;
        }
    }
}
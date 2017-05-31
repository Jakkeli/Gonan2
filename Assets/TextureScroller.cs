using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour {
    public float scrollSpeed = 1;
    private Material material;
    public Vector2 textureTiling = new Vector2(1,1);

	// Use this for initialization
	void Start () {

        material = GetComponent<SpriteRenderer>().material;
        material.SetTextureScale("_MainTex", textureTiling);
		
	}
	
	// Update is called once per frame
	void Update () {
        material.SetTextureOffset("_MainTex", new Vector2(0, Time.time*scrollSpeed));
	}
}

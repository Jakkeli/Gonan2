using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotator : MonoBehaviour {

    SpriteRenderer sr;
    Transform sprite;
    public float rotSpeed = 1;
    Quaternion rotation;
    float zRot;

	void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
        sprite = GetComponentInChildren<Transform>();
	}
	
	void Update () {
        zRot += rotSpeed;
        sprite.Rotate(0, 0, rotSpeed);
	}
}

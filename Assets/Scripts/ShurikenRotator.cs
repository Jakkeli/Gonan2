using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotator : MonoBehaviour {

    public float rotSpeed = 1;
	
	void Update () {
        GetComponentInChildren<Transform>().Rotate(0, 0, rotSpeed * Time.deltaTime);
	}
}

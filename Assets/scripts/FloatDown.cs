using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatDown : MonoBehaviour {

    bool start;
    float tickTime;
    public float timer = 4;
    public float speedH = 1;
    public float speedV = -0.1f;
    public AudioClip pickupSound;

    public void DoIt() {
        start = true;
    }

    void GoRight() {
        transform.position += new Vector3(speedH * 1, speedV, 0) * Time.deltaTime;
    }

    void GoLeft() {
        transform.position += new Vector3(speedH * -1, speedV, 0) * Time.deltaTime;
    }

    void Update() {
        if (start) {
            tickTime += Time.deltaTime;
            if (tickTime >= timer) {
                start = false;
                GetComponent<SpriteRenderer>().enabled = false;
            } 

            if (tickTime <= 0.38f) {
                GoRight();
            } else if (tickTime <= 1) {
                GoLeft();
            } else if (tickTime <= 1.75) {
                GoRight();
            } else if (tickTime <= 2.25) {
                GoLeft();
            } else if (tickTime < 3) {
                GoRight();
            } else if (tickTime < timer) {
                GoLeft();
            }
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player" && start) {
            // do things in gamemanager
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.5f);
            GetComponent<SpriteRenderer>().enabled = false;
            start = false;
        }
    }
}

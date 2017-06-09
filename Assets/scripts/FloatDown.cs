using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatDown : MonoBehaviour {

    bool start;
    float tickTime;
    public float timer = 4;
    public float speedH = 1;
    public float speedV = -0.1f;
    FabricCtrl fabCtrl;

    private void Start() {
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
    }

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

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && start) {
            fabCtrl.PlaySoundPickup();
            GetComponent<SpriteRenderer>().enabled = false;
            start = false;
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobTheBlob : MonoBehaviour {

    bool start;
    float tickTime;
    public float timer = 5;
    public float speedH = 1;
    public float speedV = -0.1f;
    FabricCtrl fabCtrl;
    bool flash;

    private void Start() {
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponentInChildren<ParticleSystem>().enableEmission = false;
    }

    public void DropBlob() {
        start = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponentInChildren<ParticleSystem>().enableEmission = true;
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
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponentInChildren<ParticleSystem>().enableEmission = false;
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
            if (tickTime > 4.5f) {
                if (flash) {
                    GetComponent<SpriteRenderer>().enabled = true;
                    flash = false;
                } else {
                    GetComponent<SpriteRenderer>().enabled = false;
                    flash = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && start) {
            fabCtrl.PlaySoundPickup();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().enableEmission = false;
            start = false;
            gameObject.SetActive(false);
        }
    }
}

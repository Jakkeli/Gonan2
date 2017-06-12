using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobTheBlob : MonoBehaviour {

    bool start;
    bool canMove;
    float tickTime;
    public float timer = 5;
    public float speedH = 1;
    public float speedV = -0.1f;
    public float rotSpeed = 1;
    FabricCtrl fabCtrl;
    bool flash;
    ParticleSystem particles;
    Player player;
    public int myValue = 1;

    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        particles = GetComponentInChildren<ParticleSystem>();
        particles.enableEmission = false;
    }

    public void DropBlob() {
        start = true;
        canMove = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        particles.enableEmission = true;
    }

    void GoRight() {
        transform.position += new Vector3(speedH * 1, speedV, 0) * Time.deltaTime;
        transform.Rotate(0, 0, rotSpeed);
    }

    void GoLeft() {
        transform.position += new Vector3(speedH * -1, speedV, 0) * Time.deltaTime;
        transform.Rotate(0, 0, rotSpeed);
    }



    void Update() {
        if (start) {
            tickTime += Time.deltaTime;
            if (tickTime >= timer) {
                start = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                particles.enableEmission = false;
            }

            if (canMove) {
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
            
            if (tickTime > 4f) {
                if (flash) {
                    particles.enableEmission = true;
                    flash = false;
                } else {
                    particles.enableEmission = false;
                    flash = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && start) {
            fabCtrl.PlaySoundPickup();
            player.HeartPickup(myValue);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            particles.enableEmission = false;
            start = false;
            gameObject.SetActive(false);
        } else if (c.tag == "stairs" || c.tag == "ground") {
            canMove = false;
        }
    }
}

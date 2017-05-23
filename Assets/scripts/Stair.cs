using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    Player player;
    bool leftUp;
    bool playerComesFromLeft;
    Rigidbody2D rb;
    Vector2 playerPos;
    Vector2 myPos;

    public bool forceStair;

    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
        if (tag == "leftUp") leftUp = true;
        if (tag == "rightUp") leftUp = false;
        rb = GameObject.Find("player").GetComponent<Rigidbody2D>();
        myPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            if (player.playerComesFromAbove) {
                player.GetOnStair(leftUp);
            } else if (leftUp && playerComesFromLeft && player.horizontalAxis > 0 && player.verticalAxis < 0) {
                player.GetOnStair(leftUp);
            } else if (leftUp && !playerComesFromLeft && player.horizontalAxis < 0 && player.verticalAxis > 0) {
                player.GetOnStair(leftUp);
            } else if (!leftUp && playerComesFromLeft && player.horizontalAxis > 0 && player.verticalAxis > 0) {
                player.GetOnStair(leftUp);
            } else if (!leftUp && !playerComesFromLeft && player.horizontalAxis < 0 && player.verticalAxis < 0) {
                player.GetOnStair(leftUp);
            } else if (forceStair) {
                player.GetOnStair(leftUp);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            player.GetOffStair();
        }
    }

    private void Update() {
        playerPos = GameObject.Find("player").transform.position;
        if (playerPos.x > myPos.x) {
            playerComesFromLeft = false;
        } else {
            playerComesFromLeft = true;
        }
    }
}

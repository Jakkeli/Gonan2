using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    Player player;
    public bool leftUp;
    public bool playerComesFromLeft;
    Vector2 playerPos;
    Vector2 myPos;

    public bool forceStair;
    public bool canDropDown;

    public float minY;
    public float maxY;


    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
        if (tag == "leftUp") leftUp = true;
        if (tag == "rightUp") leftUp = false;
        myPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            print("collider hit");
            if (player.playerComesFromAbove && player.verticalAxis > 0) {
                player.GetOnStair(leftUp, canDropDown);
            } else if (leftUp && playerComesFromLeft && player.horizontalAxis > 0) {
                player.GetOnStair(leftUp, canDropDown);
            } else if (leftUp && !playerComesFromLeft && player.horizontalAxis < 0 && player.verticalAxis > 0) {
                player.GetOnStair(leftUp, canDropDown);
            } else if (!leftUp && playerComesFromLeft && player.horizontalAxis > 0 && player.verticalAxis > 0) {
                player.GetOnStair(leftUp, canDropDown);
            } else if (!leftUp && !playerComesFromLeft && player.horizontalAxis < 0) {
                player.GetOnStair(leftUp, canDropDown);
            } else if (forceStair) {
                player.GetOnStair(leftUp, canDropDown);
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

        if (player.currentState == PlayerState.OnStair) {
            if (playerPos.y <= minY) player.GetOffStair();
        }
    }
}

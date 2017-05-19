using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    Player player;
    bool leftUp;

    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
        if (tag == "leftUp") leftUp = true;
        if (tag == "rightUp") leftUp = false;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            player.GetOnStair(leftUp);
        }
    }

    private void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            player.GetOffStair();
        }
    }
}

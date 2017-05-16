using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour {

    Player player;

	void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
	}

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            player.Death();
        }
    }
}

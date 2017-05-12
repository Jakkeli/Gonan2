using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelScript : MonoBehaviour {

    public float hSpeed;
    public float vSpeed;
    float hDir;
    float vDir = 1;
    public float maxY;
    public float minY;


    public bool goRight;

    GameObject player;

	void Start () {
        player = GameObject.Find("player");
	}
	
	void Update () {
        if (transform.position.y >= maxY) {
            vDir = -1;
        } else if (transform.position.y <= minY) {
            vDir = 1;
        }

        if (goRight) hDir = 1;
        if (!goRight) hDir = -1;
        transform.Translate(hSpeed * Time.deltaTime * hDir, vSpeed * Time.deltaTime * vDir, 0);
	}

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            int dir;
            if (c.transform.position.x < transform.position.x) {
                dir = -1;
            }
            else {
                dir = 1;
            }
            player.GetComponent<Player>().EnemyHitPlayer(dir);
        }
    }
}

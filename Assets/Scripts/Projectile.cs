using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    Player ps;
    Vector3 dir;
    bool wasShot;

	void Start () {
		ps = GameObject.Find("player").GetComponent<Player>();
	}

    public void ShootThis(Vector3 direction) {
        dir = direction;
        wasShot = true;
        Destroy(gameObject, 10);
    }
	
	void Update () {
		if (wasShot) {
            transform.position += dir * Time.deltaTime * speed;
        }
	}

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player") {
            int dir;
            if (c.transform.position.x < transform.position.x) {
                dir = -1;
            } else {
                dir = 1;
            }
            if (ps.currentState != PlayerState.Dead && ps.currentState != PlayerState.KnockedBack) {
                ps.EnemyHitPlayer(dir);
            }
        }
    }
}

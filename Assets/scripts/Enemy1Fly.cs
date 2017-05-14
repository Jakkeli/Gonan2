using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1State { Inactive, Activated, Dead };

public class Enemy1Fly : MonoBehaviour, IReaction {

    public Enemy1State currentState;
    public float hSpeed;
    public float vSpeed;
    float hDir;
    float vDir = 1;
    public float maxY;
    public float minY;

    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;

    GameObject player;
    FabricCtrl fabCtrl;

	void Start () {
        player = GameObject.Find("player");
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
	}

    public void React() {
        //take damage
        hp--;
        if (hp == 0) {
            Death();
        } else {
            fabCtrl.PlaySoundEnemy1Hit();
        }
    }

    public void Death() {
        //die
        print("enemy dieded");
        currentState = Enemy1State.Dead;
        fabCtrl.PlaySoundEnemy1Destroyed();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        //animation + effects?
    }

    public void Activate() {
        if (currentState == Enemy1State.Inactive) currentState = Enemy1State.Activated;
    }
	
	void Update () {
        if (currentState != Enemy1State.Activated) {
            return;
        }
        if (transform.position.y >= maxY) {
            vDir = -1;
        } else if (transform.position.y <= minY) {
            vDir = 1;
        }

        if (goRight) hDir = 1;
        if (!goRight) hDir = -1;
        transform.Translate(hSpeed * Time.deltaTime * hDir, vSpeed * Time.deltaTime * vDir, 0);
        if (hDir < 0) GetComponent<SpriteRenderer>().flipX = false;
        if (hDir > 0) GetComponent<SpriteRenderer>().flipX = true;
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

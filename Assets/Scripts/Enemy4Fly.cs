using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy4State { Inactive, Activated, Chase, Dead };

public class Enemy4Fly : MonoBehaviour, IReaction {

    public Enemy4State currentState;
    public float speed;
    public float diveSpeed;
    float hDir;

    Vector3 originalPos;
    Vector3 playerPos;
    Vector3 targetPos;

    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;

    GameObject player;
    FabricCtrl fabCtrl;

    public float triggerDistance;

    void Start() {
        player = GameObject.Find("player");
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        originalPos = transform.position;
        hDir = goRight ? 1 : -1;
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
        currentState = Enemy4State.Dead;
        fabCtrl.PlaySoundEnemy1Destroyed();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        //animation + effects?
    }

    public void Activate() {
        if (currentState == Enemy4State.Inactive) {
            currentState = Enemy4State.Activated;
        }
    }

    void Update() {
        if (currentState != Enemy4State.Activated) {
            return;
        }
        playerPos = player.transform.position;

        if (hDir < 0) GetComponent<SpriteRenderer>().flipX = false;
        if (hDir > 0) GetComponent<SpriteRenderer>().flipX = true;

        if (Mathf.Abs(transform.position.x - playerPos.x) <= triggerDistance && currentState == Enemy4State.Activated) {
            currentState = Enemy4State.Chase;
        }


        
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            int dir;
            if (c.transform.position.x < transform.position.x) {
                dir = -1;
            } else {
                dir = 1;
            }
            player.GetComponent<Player>().EnemyHitPlayer(dir);
        }
    }
}

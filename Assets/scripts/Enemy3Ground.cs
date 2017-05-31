using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy3State { Inactive, Activated, Dead };
enum OtherState { NotDropped, Dropping, Landed };

public class Enemy3Ground : MonoBehaviour, IReaction {

    public Enemy3State currentState;
    OtherState otherState;
    public float hSpeed;
    float hDir;
    public float targetY;
    public float dropSpeed;
    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;
    public float minX;
    public float maxX;
    GameObject player;
    FabricCtrl fabCtrl;
    Vector3 targetPos;

    void Start () {
        player = GameObject.Find("player");
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        hDir = goRight ? 1 : -1;
    }

    public void Activate() {
        if (currentState == Enemy3State.Inactive) {
            currentState = Enemy3State.Activated;
        }
    }

    public void React() {
        //take damage
        hp--;
        if (hp == 0) {
            Death();
        } else {
            fabCtrl.PlaySoundEnemy3Hit();
        }
    }

    public void Drop() {
        otherState = OtherState.Dropping;
    }

    public void Death() {
        //die
        print("enemy dieded");
        currentState = Enemy3State.Dead;
        fabCtrl.PlaySoundEnemy3Destroyed();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //animation + effects?
    }

    private void OnTriggerEnter2D(Collider2D c) {
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

    void Update () {
        if (currentState != Enemy3State.Activated) return;
        var pos = transform.position;
        
        if (otherState == OtherState.Dropping) {
            if (pos.y >= targetY) {
                targetPos = pos;
                targetPos.y = pos.y - dropSpeed * Time.deltaTime;
                transform.position = targetPos;
            } else {
                otherState = OtherState.Landed;
                transform.position = new Vector3(transform.position.x, targetY, 0);
            }
        } else if (otherState == OtherState.Landed) {
            if (transform.position.x >= maxX) {
                goRight = false;
            } else if (transform.position.x <= minX) {
                goRight = true;
            }
            hDir = goRight ? 1 : -1;
            targetPos = pos;
            targetPos.x = hDir * hSpeed * Time.deltaTime;
            transform.Translate(hDir * hSpeed * Time.deltaTime, 0, 0);
            if (hDir < 0) GetComponent<SpriteRenderer>().flipX = false;
            if (hDir > 0) GetComponent<SpriteRenderer>().flipX = true;
        }        
    }
}

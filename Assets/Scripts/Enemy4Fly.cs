using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy4State { Inactive, Activated, Awake, Dive, Recover, Dead };

public class Enemy4Fly : MonoBehaviour, IReaction {

    public Enemy4State currentState;
    public float speed;
    public float diveSpeed;
    public float hDir;
    float prevHDir;

    //Vector3 originalPos;
    Vector3 playerPos;
    Vector3 targetPos;
    Vector3 backOffPos;

    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;

    GameObject player;
    FabricCtrl fabCtrl;

    public float triggerDistance;

    public bool targetPosSet;
    public bool diveComplete;
    public bool backOffPosSet;

    void Start() {
        player = GameObject.Find("player");
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        hDir = goRight ? 1 : -1;
        prevHDir = hDir;
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
        if (currentState == Enemy4State.Inactive || currentState == Enemy4State.Dead) {
            return;
        }
        playerPos = player.transform.position;
        var pos = transform.position;
        Vector3 targetDir;

        if (GetComponent<Rigidbody2D>().velocity.x < 0) {
            GetComponent<SpriteRenderer>().flipX = false;
            hDir = -1;
        } else if (GetComponent<Rigidbody2D>().velocity.x > 0) {
            GetComponent<SpriteRenderer>().flipX = true;
            hDir = 1;
        }

        if (Mathf.Abs(pos.x - playerPos.x) <= triggerDistance && currentState == Enemy4State.Activated && currentState != Enemy4State.Awake) {
            currentState = Enemy4State.Awake;
        }

        if (currentState == Enemy4State.Awake) {
            if ((targetPos - pos).magnitude < 0.2f) {
                currentState = Enemy4State.Dive;
                return;
            }
            if (!targetPosSet) {
                targetPos = pos + new Vector3((pos.x + 2) * hDir, pos.y + 2, 0);
                targetPosSet = true;
            }
            targetDir = targetPos - pos;
            transform.position += targetDir * Time.deltaTime * speed;
        }

        if (currentState == Enemy4State.Dive) {

        }

        targetDir = targetPos - pos;
        transform.position += targetDir * Time.deltaTime * speed;
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

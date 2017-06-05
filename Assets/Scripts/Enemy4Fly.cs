using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy4State { Inactive, Activated, Chase, Dead };

public class Enemy4Fly : MonoBehaviour, IReaction {

    public Enemy4State currentState;
    public float speed;
    public float diveSpeed;
    float hDir;
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

    bool targetPosSet;
    bool diveComplete;
    bool backOffPosSet;

    void Start() {
        player = GameObject.Find("player");
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        //originalPos = transform.position;
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
        if (currentState != Enemy4State.Activated) {
            return;
        }
        playerPos = player.transform.position;
        var pos = transform.position;

        if (hDir < 0) GetComponent<SpriteRenderer>().flipX = false;
        if (hDir > 0) GetComponent<SpriteRenderer>().flipX = true;

        if (Mathf.Abs(pos.x - playerPos.x) <= triggerDistance && currentState == Enemy4State.Activated) {
            currentState = Enemy4State.Chase;
        }

        if (currentState == Enemy4State.Chase) {
            if (!targetPosSet) {
                targetPos = playerPos;
                targetPosSet = true;
            }
            if (!diveComplete) {
                targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * diveSpeed);
                if (pos != targetPos) {
                    pos = targetPos;
                } else {
                    diveComplete = true;
                }
            } else if (diveComplete && targetPosSet) {
                if (prevHDir == 1) {
                    if (!backOffPosSet) {
                        backOffPos = transform.position + new Vector3(pos.x + 4, pos.y + 4, 0);
                        backOffPosSet = true;
                        targetPos = backOffPos;
                    }                  
                } else {
                    if (!backOffPosSet) {
                        backOffPos = transform.position + new Vector3(pos.x - 4, pos.y + 4, 0);
                        backOffPosSet = true;
                        targetPos = backOffPos;
                    }
                }

                if (targetPos != pos) {
                    targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * speed);
                } else {
                    diveComplete = false;

                }
                

            }            
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

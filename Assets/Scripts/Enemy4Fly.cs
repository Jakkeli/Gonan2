using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy4State { Inactive, Activated, Awake, Dive, Recover, Dead };

public class Enemy4Fly : MonoBehaviour, IReaction {

    public Enemy4State currentState;
    public float speed;
    public float diveSpeed;
    public float backOffSpeed;
    float usedSpeed;
    float hDir;
    //float prevHDir;
    Vector3 playerPos;
    Vector3 targetPos;
    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;
    public int scoreWorth = 500;
    GameObject player;
    FabricCtrl fabCtrl;
    public float triggerDistance;
    bool targetPosSet;
    bool diveComplete;
    bool backOffPosSet;
    Vector3 pos;
    Vector3 targetDir;
    SpriteRenderer spriteRenderer;
    GameManager gm;
    Player ps;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = player.GetComponent<Player>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        hDir = goRight ? 1 : -1;
        //prevHDir = hDir;
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
        gm.score += scoreWorth;
        gm.UpdateScore();
        currentState = Enemy4State.Dead;
        fabCtrl.PlaySoundEnemy1Destroyed();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        gameObject.SetActive(false);
        //animation + effects?
    }

    public void Activate() {
        if (currentState == Enemy4State.Inactive) {
            currentState = Enemy4State.Activated;
        }
    }

    void Move() {        
        targetDir = targetPos - pos;
        targetDir.Normalize();
        transform.position += targetDir * Time.deltaTime * usedSpeed;
    }

    void Update() {
        if (currentState == Enemy4State.Inactive || currentState == Enemy4State.Dead) return;
        if (gm.currentState != GameState.Running) return;
        if (ps.currentState == PlayerState.Dead) return;

        playerPos = player.transform.position;
        pos = transform.position;

        if (GetComponent<Rigidbody2D>().velocity.x < 0) {
            hDir = -1;
        } else if (GetComponent<Rigidbody2D>().velocity.x > 0) {
            hDir = 1;
        }

        if (playerPos.x < pos.x) spriteRenderer.flipX = true;
        if (playerPos.x > pos.x) spriteRenderer.flipX = false;

        if (Input.GetKeyDown(KeyCode.F)) {
            GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX ? false : true;
        }

        if (Mathf.Abs(pos.x - playerPos.x) <= triggerDistance && currentState == Enemy4State.Activated && currentState != Enemy4State.Awake) {
            currentState = Enemy4State.Awake;
        }

        if (currentState == Enemy4State.Awake) {            
            if (!targetPosSet) {
                usedSpeed = speed;
                targetPos = new Vector3((pos.x + 2 * hDir), pos.y + 2, 0);
                targetPosSet = true;
            }
            if (targetPosSet) {
                Move();
            }
            if ((targetPos - pos).magnitude < 0.1f) {
                targetPosSet = false;
                currentState = Enemy4State.Dive;
            }
        }

        if (currentState == Enemy4State.Dive) {
            if (!targetPosSet) {
                usedSpeed = diveSpeed;
                targetPos = playerPos;
                targetPosSet = true;
            }
            if (targetPosSet) {
                Move();
            }
            if ((targetPos - pos).magnitude < 0.1f) {
                targetPosSet = false;
                currentState = Enemy4State.Recover;
            }            
        }

        if (currentState == Enemy4State.Recover) {
            if (!targetPosSet) {
                usedSpeed = backOffSpeed;
                targetPos = new Vector3((pos.x + 4 * hDir), pos.y + 4, 0);
                targetPosSet = true;
                if (hDir == -1) {
                    hDir = 1;
                } else {
                    hDir = -1;
                }
            }
            if (targetPosSet) {
                Move();
            }
            if ((targetPos - pos).magnitude < 0.1f) {
                targetPosSet = false;
                currentState = Enemy4State.Dive;
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
            if (ps.canTakeDamage) {
                ps.EnemyHitPlayer(dir);
                targetPosSet = false;
                currentState = Enemy4State.Recover;
            }
        }
    }
}

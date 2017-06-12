using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { Inactive, Triggered, Fighting, Dead}

public class BossOne : MonoBehaviour {

    public BossState currentState;
    public int hp;
    GameObject player;
    Player ps;
    GameManager gm;
    Vector3 playerPos;
    Vector3 startPos;
    int resetHp;
    bool inPlace;
    bool triggered;
    public float transitionSpeed = 1;
    float targetX = 289;
    public GameObject projectilePrefab;
    public Vector3 projectileStartPos;
    float tickTime;
    public float shootIntervalNormal = 1;
    public float shootIntervalBarrage = 0.25f;
    GameObject currentProjectile;
    FabricCtrl fabCtrl;
    bool firstLaughStarted;
    bool firstLaughComplete;
    int shotsFired;
    bool shootModeNormal = true;

	void Start () {
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        player = GameObject.Find("player");
        ps = player.GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        resetHp = hp;
        startPos = transform.position;
	}

    public void TriggerFight() {
        //move to fightPos
        currentState = BossState.Triggered;
    }

    public void Reset() {
        transform.position = startPos;
    }

    public void TakeDamage() {
        if (!firstLaughComplete) return;
        print("boss took a hit");
        hp--;
        if (hp == 0) {
            Death();
        }
    }

    void Death() {
        currentState = BossState.Dead;
        print("boss dieded. now u the boss");
    }

    void ShootNormal() {
        //Vector3 dir = player.transform.position - transform.position;
        Vector3 dir = new Vector3(player.transform.position.x, player.transform.position.y +1, 0) - transform.position;
        dir.Normalize();
        currentProjectile = Instantiate(projectilePrefab, projectileStartPos, Quaternion.identity);
        if (currentProjectile.GetComponent<Projectile>() != null) currentProjectile.GetComponent<Projectile>().ShootThis(dir);
    }

    void Laugh() {
        fabCtrl.PlaySoundBossLaugh1();
    }
	
	void Update () {
		if (currentState == BossState.Triggered) {
            transform.position += new Vector3(-transitionSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= targetX) currentState = BossState.Fighting;
        }

        if (currentState == BossState.Fighting) {
            if (Input.GetKeyDown(KeyCode.L) && gm.editorMode) {
                ShootNormal();
            }

            if (!firstLaughStarted) {
                fabCtrl.PlaySoundBossLaugh1();
                firstLaughStarted = true;
                tickTime = 0;
            } else if (!firstLaughComplete) {
                tickTime += Time.deltaTime;
                if (tickTime > 3) {
                    tickTime = 0;
                    firstLaughComplete = true;
                }
            }
            if (firstLaughComplete) {
                //shoot
                tickTime += Time.deltaTime;
                if (tickTime > shootIntervalNormal) {
                    ShootNormal();
                    shotsFired++;
                    tickTime -= shootIntervalNormal;
                }
            }
        }
	}
}

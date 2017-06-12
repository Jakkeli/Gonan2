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
    float targetX = 310;
    public GameObject projectilePrefab;
    public Vector3 projectileStartPos;
    float tickTime;
    public float shootIntervalNormal = 1;
    public float shootIntervalBarrage = 0.25f;
    GameObject currentProjectile;
    FabricCtrl fabCtrl;
    bool laughStarted;
    public bool laughComplete;
    public int shotsFired;
    public bool shootModeNormal = true;
    public int normalCycle = 10;
    public int barrageCycle = 10;

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
        if (!laughComplete) return;
        print("boss took a hit");
        hp--;
        gm.enemyHealth = hp;
        if (hp == 0) {
            Death();
            return;
        }
        gm.UpdatePlayerEnemyHealth(gm.playerHealth, hp);
    }

    void Death() {
        currentState = BossState.Dead;
        print("boss dieded. now u the boss");
        gm.GameFinished();
    }

    void Shoot() {
        Vector3 dir = new Vector3(player.transform.position.x, player.transform.position.y +1, 0) - transform.position;
        dir.Normalize();
        currentProjectile = Instantiate(projectilePrefab, projectileStartPos, Quaternion.identity);
        if (currentProjectile.GetComponent<Projectile>() != null) currentProjectile.GetComponent<Projectile>().ShootThis(dir);
    }

    void Laugh() {
        fabCtrl.PlaySoundBossLaugh1();
        GetComponentInChildren<JawRotator>().Laugh(4);
        laughStarted = true;
        tickTime = 0;
    }
	
	void Update () {

        if (gm.currentState != GameState.Running) return;
        if (ps.currentState == PlayerState.Dead) return;

		if (currentState == BossState.Triggered) {
            transform.position += new Vector3(-transitionSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= targetX) currentState = BossState.Fighting;
        }

        if (currentState == BossState.Fighting) {
            if (Input.GetKeyDown(KeyCode.L) && gm.editorMode) {
                Shoot();
            }

            if (!laughStarted) {
                Laugh();
                laughStarted = true;
            }

            if (shootModeNormal) {
                if (laughComplete) {
                    tickTime += Time.deltaTime;
                    if (tickTime > shootIntervalNormal) {
                        Shoot();
                        shotsFired++;
                        tickTime -= shootIntervalNormal;
                    }
                }
                if (shotsFired >= normalCycle) {
                    shootModeNormal = false;
                    laughComplete = false;
                    shotsFired = 0;
                    tickTime = 0;
                    print("normal cyle over");
                }
            } else if (!shootModeNormal) {
                tickTime += Time.deltaTime;
                if (tickTime > shootIntervalBarrage) {
                    Shoot();
                    shotsFired++;
                    tickTime -= shootIntervalBarrage;
                }
                if (shotsFired >= barrageCycle) {
                    shootModeNormal = true;
                    shotsFired = 0;
                    tickTime = 0;
                    Laugh();
                    print("barrage cycle over, laugh");
                }
            }
            
        }
	}
}

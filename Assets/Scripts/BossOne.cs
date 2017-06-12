﻿using System.Collections;
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
    public float shootInterval = 2;
    GameObject currentProjectile;

	void Start () {
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
        if (currentState != BossState.Fighting) return;
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

    }
	
	void Update () {
		if (currentState == BossState.Triggered) {
            transform.position += new Vector3(-transitionSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= targetX) currentState = BossState.Fighting;
        }

        if (currentState == BossState.Fighting) {
            if (Input.GetKeyDown(KeyCode.L)) {
                ShootNormal();
            }
        }
	}
}

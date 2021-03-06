﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Enemy2Ground : MonoBehaviour, IReaction {

    public Enemy1State currentState;
    public float hSpeed;
    float hDir;

    public bool activateOnStart;
    public bool goRight;
    public int hp = 1;
    public int scoreWorth = 100;

    public float maxX;
    public float minX;

    GameObject player;
    Player ps;
    GameManager gm;

    FabricCtrl fabCtrl;
    DeathParticles dp;
    UnityArmatureComponent uac;
    MeshRenderer[] mr;


    void Start() {
        uac = GetComponentInChildren<UnityArmatureComponent>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("player");
        ps = player.GetComponent<Player>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        dp = GetComponentInChildren<DeathParticles>();
        mr = GetComponentsInChildren<MeshRenderer>();
    }

    public void React() {
        //take damage
        hp--;
        if (hp == 0) {
            Death();
        } else {
            fabCtrl.PlaySoundEnemy2Hit();
        }
    }

    public void Death() {
        gm.score += scoreWorth;
        gm.UpdateScore();
        currentState = Enemy1State.Dead;
        fabCtrl.PlaySoundEnemy1Destroyed();
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (MeshRenderer mesh in mr) {
            mesh.enabled = false;
        }
        dp.DeathFX();
    }

    public void Activate() {
        if (currentState == Enemy1State.Inactive) currentState = Enemy1State.Activated;
    }

    void Update() {
        if (gm.currentState != GameState.Running) return;
        if (currentState != Enemy1State.Activated) {
            return;
        }

        if (ps.currentState == PlayerState.Dead || ps.currentState == PlayerState.KnockedBack) return;

        if (transform.position.x >= maxX) {
            goRight = false;
        } else if (transform.position.x <= minX) {
            goRight = true;
        }

        if (goRight) hDir = 1;
        if (!goRight) hDir = -1;
        transform.Translate(hSpeed * Time.deltaTime * hDir, 0, 0);
        //if (hDir < 0) GetComponent<SpriteRenderer>().flipX = true;
        //if (hDir > 0) GetComponent<SpriteRenderer>().flipX = false;
        if (hDir < 0) uac.armature.flipX = true;
        if (hDir > 0) uac.armature.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy1State { Inactive, Activated, Dead };

public class Enemy1Fly : MonoBehaviour, IReaction {

    public Enemy1State currentState;
    public float hSpeed;
    public float vSpeed;
    float hDir;
    public float maxY;
    public float minY;

    public float speed = 1;
    public float wavelength = 5;
    float startTime;

    Vector2 originalPos;

    public bool activateOnStart;
    public bool goRight;
    public bool startVDirDown;
    public int hp = 1;

    GameObject player;
    Player ps;
    FabricCtrl fabCtrl;
    int svdir;

	void Start () {
        player = GameObject.Find("player");
        ps = player.GetComponent<Player>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        if (activateOnStart) {
            Activate();
        }
        originalPos = transform.position;
        hDir = goRight? 1:-1;
        svdir = startVDirDown ? 1 : 3; 
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
        //print("enemy dieded");
        currentState = Enemy1State.Dead;
        fabCtrl.PlaySoundEnemy1Destroyed();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        gameObject.SetActive(false);
        //animation + effects?
    }

    public void Activate() {
        if (currentState == Enemy1State.Inactive) {
            currentState = Enemy1State.Activated;
            startTime = Time.time;
        }
    }
	
	void Update () {
        if (currentState != Enemy1State.Activated) {
               return;
        }

        if (ps.currentState == PlayerState.Dead || ps.currentState == PlayerState.KnockedBack) return;

        if (hDir < 0) GetComponent<SpriteRenderer>().flipX = false;
        if (hDir > 0) GetComponent<SpriteRenderer>().flipX = true;
        var t = Time.time - startTime;
        var dx = speed * hDir * t;

        //transform.position = originalPos + new Vector2(dx, maxY * Mathf.Sin(dx * 2 * Mathf.PI / wavelength));
        transform.position = originalPos + new Vector2(dx, maxY * (Mathf.Abs((Mathf.Abs(dx * 4 / wavelength) + svdir)%4 - 2) - 1));
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            int dir;
            if (c.transform.position.x < transform.position.x) {
                dir = -1;
            }
            else {
                dir = 1;
            }
            if (ps.currentState != PlayerState.Dead && ps.currentState != PlayerState.KnockedBack) {
                ps.EnemyHitPlayer(dir);
            }            
        }
    }
}

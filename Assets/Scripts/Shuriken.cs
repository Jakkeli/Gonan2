using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {

    public float speed = 1;
    int dir = 1;

    public float flyDist;
    public bool wasThrown;
    bool hasTurnedBack;
    float startX;
    public float rotateSpeed;
    public float goneDist;
    GameObject player;
    Player p;
    SpriteRenderer sr;
    public float yOffset;

    public void Throw(int d, bool crouch) {
        if (!wasThrown) {
            yOffset = crouch ? -0.25f : 0.5f;
            transform.position = new Vector2(transform.position.x, player.transform.position.y + yOffset);
            wasThrown = true;
            startX = transform.position.x;
            sr.enabled = true;
            dir = d;
        }       
    }

    public void Destroy() {
        p.currentShurikenCount--;
        wasThrown = false;
        hasTurnedBack = false;
        sr.enabled = false;
    }

    void Awake() {
        player = GameObject.Find("player");
        p = player.GetComponent<Player>();
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) {
            print("wtf");
        }
	}

    void Move() {
        transform.Translate(speed * Time.deltaTime * dir, 0, 0);
        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * dir);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * dir);
    }

    void Update () {
		if (wasThrown) {
            if (dir == 1 && !hasTurnedBack) {
                if (transform.position.x >= startX + flyDist) {
                    hasTurnedBack = true;
                    dir = -1;
                }
                Move();
            } else if (dir == -1 && hasTurnedBack) {
                if (startX - transform.position.x > goneDist) {
                    Destroy();
                }
                Move();
            } else if (dir == -1 && !hasTurnedBack) {
                if (transform.position.x <= startX - flyDist) {
                    hasTurnedBack = true;
                    dir = 1;
                }
                Move();
            } else if (dir == 1 && hasTurnedBack) {
                if (Vector2.Distance(transform.position, player.transform.position) > goneDist) {
                    Destroy();
                }
                Move();
            }
        } else {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            Throw(1, false);
        }
	}

    void OnTriggerEnter2D(Collider2D c) {
        if (c.GetComponent<IReaction>() != null && c.tag == "enemy") {
            c.GetComponent<IReaction>().React();
        } else if (c.GetComponent<IReaction>() != null && c.tag == "item") {
            c.GetComponent<IReaction>().React();
        } else if (c.tag == "Player" && hasTurnedBack) {
            Destroy();
        }
    }
}

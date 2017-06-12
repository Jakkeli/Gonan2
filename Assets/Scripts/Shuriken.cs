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
    public float startYfix;
    float yOffset;
    CircleCollider2D circCol;

    public void Throw(int d, bool crouch) {
        if (!wasThrown) {
            yOffset = crouch ? -0.25f : 0.5f;
            transform.position = new Vector2(transform.position.x, player.transform.position.y + yOffset + startYfix);
            wasThrown = true;
            startX = transform.position.x;
            hasTurnedBack = false;
            sr.enabled = true;
            dir = d;
            circCol.enabled = true;
        }       
    }

    public void Destroy() {
        p.currentShurikenCount--;
        wasThrown = false;
        hasTurnedBack = false;
        sr.enabled = false;
        circCol.enabled = false;
    }

    void Awake() {
        //sprite = GetComponentInChildren<Transform>();
        player = GameObject.Find("player");
        p = player.GetComponent<Player>();
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) {
            print("wtf");
        }
        circCol = GetComponent<CircleCollider2D>();
        if (circCol.enabled) circCol.enabled = false;
	}

    void Move() {
        //transform.Translate(speed * Time.deltaTime * dir, 0, 0);
        transform.position += new Vector3(speed * Time.deltaTime * dir, 0, 0);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * -dir);
    }

    void Update () {
		if (wasThrown) {
            if (dir == 1 && !hasTurnedBack) {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) > flyDist) {
                    hasTurnedBack = true;
                    dir = -1;
                }
                Move();
            } else if (dir == -1 && hasTurnedBack) {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) > goneDist) {
                    Destroy();
                }
                Move();
            } else if (dir == -1 && !hasTurnedBack) {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) > flyDist) {
                    hasTurnedBack = true;
                    dir = 1;
                }
                Move();
            } else if (dir == 1 && hasTurnedBack) {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) > goneDist) {
                    Destroy();
                }
                Move();
            }
        } else {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
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

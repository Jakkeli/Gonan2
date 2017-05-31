using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {

    public float speed = 1;
    int dir = 1;

    public float flyDist;
    bool wasThrown;
    bool hasTurnedBack;
    float startX;
    public float rotateSpeed;
    public float goneDist;
    GameObject player;
    Player p;
    SpriteRenderer sr;

    public void Throw(int dir) {
        if (!wasThrown) {
            wasThrown = true;
            startX = transform.position.x;
            print("thrown called");
        }       
    }

    public void Destroy() {
        p.currentShurikenCount--;
        wasThrown = false;
        hasTurnedBack = false;
        print("destroy called");
        Destroy(gameObject);        
    }

    void Awake() {
        player = GameObject.Find("player");
        p = player.GetComponent<Player>();
	}
	
	void Update () {
		if (wasThrown) {
            if (dir == 1 && !hasTurnedBack) {
                if (transform.position.x >= startX + flyDist) {
                    hasTurnedBack = true;
                    dir = -1;
                }
                transform.Translate(speed * Time.deltaTime * dir, 0, 0);
                //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            } else if (dir == -1 && hasTurnedBack) {
                if (Vector2.Distance(transform.position, player.transform.position) > goneDist) {
                    Destroy();
                }
                transform.Translate(speed * Time.deltaTime * dir, 0, 0);
                //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            } else if (dir == -1 && !hasTurnedBack) {
                if (transform.position.x <= startX - flyDist) {
                    hasTurnedBack = true;
                    dir = 1;
                }
                transform.Translate(speed * Time.deltaTime * dir, 0, 0);
                //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            } else if (dir == 1 && hasTurnedBack) {
                if (Vector2.Distance(transform.position, player.transform.position) > goneDist) {
                    Destroy();
                }
                transform.Translate(speed * Time.deltaTime * dir, 0, 0);
                //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            Throw(1);
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

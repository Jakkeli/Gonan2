using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Idle, Chase, Patrol, CaughtPlayer, Dead };
public enum EnemyType { Walker, Flyer };

public class Enemy : MonoBehaviour, IReaction {

    public EnemyType myType;
    public EnemyState currentState;
    bool activated;
    //bool hasReacted;
    public float speed = 1;
    GameObject player;
    public int hp = 1;
    Vector3 dir;
    bool atEdge;
    bool rightEdge;
    float patrolDir;
    public bool activateOnStart;
    bool canAttack = true;

    void Awake() {
        player = GameObject.Find("player");
    }

    void Start () {
        patrolDir = 1;
        if (activateOnStart) {
            Activate();
        }
    }

    public void TakeDamage() {
        hp--;
        if (hp == 0) {
            Death();
        }
    }
	
    public void React() {
        if (activated) {
            TakeDamage();
        }
    }

    public void Activate() {
        if (!activated) {
            activated = true;
            print("enemy activated");
            //player = GameObject.Find("player");
            currentState = EnemyState.Chase;
        }
    }

    public void DeActivate() {
        currentState = EnemyState.Idle;
    }

    void Death() {
        //animation
        //score++?
        currentState = EnemyState.Dead;
        gameObject.SetActive(false);
    }

    public void EdgeStop() {
        atEdge = true;
        
        if (dir.x < 0) {
            //was going left
            rightEdge = false;
            if (currentState == EnemyState.Patrol) {
                patrolDir = 1;
            }
        } else {
            //was going right
            rightEdge = true;
            if (currentState == EnemyState.Patrol) {
                patrolDir = -1;
            }
        }
    }

    public void EdgeLeft() {
        atEdge = false;
    }

    void PlayerCaught() {
        currentState = EnemyState.CaughtPlayer;
    }

	// Update is called once per frame
	void Update () {
		if (currentState == EnemyState.Chase && player != null) {
            
            dir = player.transform.position - transform.position;
            if (dir.magnitude > 30) {
                currentState = EnemyState.Patrol;
            }
            dir.Normalize();

            if (!atEdge) {
                transform.position += new Vector3(dir.x, 0, 0) * Time.deltaTime * speed;

            } else if (atEdge && rightEdge && dir.x < 0) {
                transform.position += new Vector3(dir.x, 0, 0) * Time.deltaTime * speed;
            } else if (atEdge && !rightEdge && dir.x > 0) {
                transform.position += new Vector3(dir.x, 0, 0) * Time.deltaTime * speed;
            }
                  
        }
        
        if (currentState == EnemyState.Patrol) {
            dir = player.transform.position - transform.position;
            if (dir.magnitude > 60) {
                DeActivate();
            }
            if (dir.magnitude < 28) {
                currentState = EnemyState.Chase;
            }
            if (!atEdge) {
                transform.position += new Vector3(patrolDir, 0, 0) * Time.deltaTime * speed;
            } else if (atEdge && rightEdge && dir.x < 0) {
                transform.position += new Vector3(patrolDir, 0, 0) * Time.deltaTime * speed;
            } else if (atEdge && !rightEdge && dir.x > 0) {
                transform.position += new Vector3(patrolDir, 0, 0) * Time.deltaTime * speed;
            }

        }

        if (currentState == EnemyState.CaughtPlayer) {
            if ((player.transform.position - transform.position).magnitude > 2) {
                currentState = EnemyState.Chase;
            }
        }
	}



    void OnCollisionEnter(Collision c) {
        
        if (c.gameObject.tag == "Player" && canAttack) {
            int dir;
            if (c.transform.position.x < transform.position.x) {
                dir = -1;
            } else {
                dir = 1;
            }
            
            player.GetComponent<Player>().TakeDamage(dir);
            canAttack = false;
            Invoke("CanAttack", 2.1f);
        }
    }

    void CanAttack() {
        canAttack = true;
    }
}

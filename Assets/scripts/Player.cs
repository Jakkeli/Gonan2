using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Moving, InAir, Crouch, KnockedBack, IndianaJones };
public enum AimState { Right, Left, Up, Down, DiagUpRight, DiagUpLeft, DiagDownRight, DiagDownLeft };

public class Player : MonoBehaviour {       // gonan 2d 05/11//17

    public PlayerState currentState;
    public AimState currentAimState;
    public AimState lastHorizontalState;

    Rigidbody2D rb;
    // BoxCollider2D boxCol;
    CapsuleCollider2D capCol;
    public float hSpeed = 4;
    public float vSpeed = 8;
    public float swingSpeed = 1;

    float horizontalAxis;
    float verticalAxis;

    public float groundCheckWidth = 0.5f;
    public float groundCheckHeight = 0.5f;
    public float stairCheckDist = 1.1f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask stairsOnly;

    public int hp;

    bool facingRight;
    bool canMove;
    bool jump;

    public bool onStair;
    public bool whipping;
    public bool canWhip = true;
    bool crouchWhip;

    Vector2 v;

    public Animator animator;

    public GameObject whip;

    public GameObject whipRight;
    public GameObject whipLeft;
    public GameObject whipUp;
    public GameObject whipDown;
    public GameObject whipDiagUpRight;
    public GameObject whipDiagUpLeft;
    public GameObject whipDiagDownRight;
    public GameObject whipDiagDownLeft;
    public GameObject shuriken;
    LineRenderer line;

    FabricCtrl fabCtrl;

    DistanceJoint2D joint;

    GameObject currentHookPoint;

    void Start() {
        joint = GetComponent<DistanceJoint2D>();
        line = GameObject.Find("line").GetComponent<LineRenderer>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        facingRight = true;
        currentAimState = AimState.Right;
        lastHorizontalState = AimState.Right;
        canMove = true;
        joint.enabled = false;
        line.enabled = false;
    }

    public void EnemyHitPlayer(int dir) {
        print("enemy hit player");
        hp--;
        if (hp == 0) {
            Death();
        } else {
            fabCtrl.PlaySoundPlayerHit1();
        }
    }

    void Death() {
        fabCtrl.PlaySoundPlayerDeath();
    }

    public void IndianaJones(GameObject go) {
        joint.enabled = true;
        line.enabled = true;
        joint.connectedBody = go.GetComponent<Rigidbody2D>();
        currentHookPoint = go;
        currentState = PlayerState.IndianaJones;
        capCol.isTrigger = true;
    }

    void LetGoOfHook() {
        currentState = PlayerState.Idle;
        joint.connectedBody = null;
        joint.enabled = false;
        line.enabled = false;
        print("do we happen?!?!");
        capCol.isTrigger = false;
    }

    public void CrouchEnd() {
        if (crouchWhip) {
            whip.transform.position += new Vector3(0, 0.5f, 0);
            crouchWhip = false;
        }
        //print("crouchend called");
        //animation
        capCol.size = new Vector2(0.63f, 2);
        capCol.offset = new Vector2(0, 0);
    }

    public void KnockBack(int dir) {

    }

    void FixedUpdate() {

        // ground-check

        float colliderLowerEdge = transform.position.y + capCol.offset.y - capCol.size.y / 2;

        if (!Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, whatIsGround) && currentState != PlayerState.IndianaJones) {
            if (currentState == PlayerState.Crouch) {
                CrouchEnd();
            }
            currentState = PlayerState.InAir;
        }
        else if (currentState == PlayerState.InAir) {
            currentState = PlayerState.Idle;
        }

        // stair check
        if (Physics.Raycast(transform.position, Vector3.down, stairCheckDist, stairsOnly) && currentState != PlayerState.IndianaJones) {
            onStair = true;
            currentState = PlayerState.Idle;
        } else {
            onStair = false;
        }

        v = rb.velocity;    //rigidbody velocity

        if (v.x > 0) facingRight = true;
        if (v.x < 0) facingRight = false;

        if (facingRight) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones) {

            if (v.x != 0f && currentState != PlayerState.Crouch) {
                currentState = PlayerState.Moving;
            }
            else {
                currentState = PlayerState.Idle;
            }
        }

        // movement

        if (canMove && currentState != PlayerState.IndianaJones) {
            if (currentState == PlayerState.Crouch) {
                rb.velocity = new Vector3(horizontalAxis * (hSpeed / 2), rb.velocity.y, 0);
            }
            else {
                rb.velocity = new Vector3(horizontalAxis * hSpeed, rb.velocity.y, 0);
            }
        }
        else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // jump & dropdown

        if (jump && currentState != PlayerState.Crouch && !onStair) {
            rb.velocity = new Vector3(rb.velocity.x, vSpeed, 0);
            jump = false;
        }
        else if (jump && currentState == PlayerState.Crouch && onStair) {
            //dropdown
            print("dropdown");
        }

        if (onStair) {
            rb.gravityScale = 0;
        }
        else {
            rb.gravityScale = 1;
        }
        // indiana jones
        if (currentState == PlayerState.IndianaJones) {
            Vector2 hookPos = currentHookPoint.transform.position;
            joint.distance = Vector2.Distance(transform.position, hookPos);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookPos);
            rb.velocity = new Vector3(horizontalAxis * swingSpeed, rb.velocity.y, 0);
        }
    }

    void Update() {

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump")) {
            if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones) {
                CrouchEnd();
                jump = true;
                //print("jump called");
            }
        }

        // crouch
        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones) {
            if (verticalAxis < 0) {
                currentState = PlayerState.Crouch;
                capCol.size = new Vector2(capCol.size.x, 1);
                capCol.offset = new Vector2(0, -0.5f);
                if (!crouchWhip) {
                    whip.transform.position += new Vector3(0, -0.5f, 0);
                    crouchWhip = true;
                }
                //animation
            }
            else if (verticalAxis >= 0) {
                CrouchEnd();
            }
        }
        if(v.x != 0) {
            animator.SetBool("walk", true);
        }
        if(v.x == 0) {
            animator.SetBool("walk", false);
        }
        // aiming

        if (horizontalAxis > 0) {
            currentAimState = AimState.Right;
            lastHorizontalState = AimState.Right;
            if (verticalAxis > 0f) {
                currentAimState = AimState.DiagUpRight;
            }
            else if (verticalAxis < 0f && currentState == PlayerState.InAir) {
                currentAimState = AimState.DiagDownRight;
            }
        }
        else if (horizontalAxis < 0f) {
            currentAimState = AimState.Left;
            lastHorizontalState = AimState.Left;
            if (verticalAxis > 0f) {
                currentAimState = AimState.DiagUpLeft;
            }
            else if (verticalAxis < 0f && currentState == PlayerState.InAir) {
                currentAimState = AimState.DiagDownLeft;
            }
        }
        else if (horizontalAxis == 0) {
            if (verticalAxis < 0 && currentState == PlayerState.InAir) {
                currentAimState = AimState.Down;
            }
            else if (verticalAxis > 0) {
                currentAimState = AimState.Up;
            }
            else {
                currentAimState = lastHorizontalState;
            }
        }

        // shooting
        if (Input.GetButtonDown("Fire1") && canWhip) {
            // shooting forward
            
            if (currentAimState == AimState.Right) {
                // whip right
                whipRight.SetActive(true);
                whipRight.GetComponent<Whip>().DoIt();
            }
            else if (currentAimState == AimState.Left) {
                // whip left
                whipLeft.SetActive(true);
                whipLeft.GetComponent<Whip>().DoIt();
            }

            // shooting forward while crouched
            if (currentState == PlayerState.Crouch) {
                if (currentAimState == AimState.Right) {
                    // whip right crouched
                    whipRight.SetActive(true);
                    whipRight.GetComponent<Whip>().DoIt();
                }

                else if (currentAimState == AimState.Left) {
                    // whip left crouched
                    whipLeft.SetActive(true);
                    whipLeft.GetComponent<Whip>().DoIt();
                }
            }

            // shooting upwards
            if (currentAimState == AimState.DiagUpRight) {
                //diagupright
                whipDiagUpRight.SetActive(true);
                whipDiagUpRight.GetComponent<Whip>().DoIt();
            }
            if (currentAimState == AimState.DiagUpLeft) {
                //diagupleft
                whipDiagUpLeft.SetActive(true);
                whipDiagUpLeft.GetComponent<Whip>().DoIt();
            }
            if (currentAimState == AimState.Up) {
                //up
                whipUp.SetActive(true);
                whipUp.GetComponent<Whip>().DoIt();
            }

            // shooting downwards
            if (currentAimState == AimState.DiagDownRight && currentState == PlayerState.InAir) {
                //diagdownright
                whipDiagDownRight.SetActive(true);
                whipDiagDownRight.GetComponent<Whip>().DoIt();
            }
            if (currentAimState == AimState.DiagDownLeft && currentState == PlayerState.InAir) {
                //diagdownleft
                whipDiagDownLeft.SetActive(true);
                whipDiagDownLeft.GetComponent<Whip>().DoIt();
            }
            if (currentState == PlayerState.InAir && currentAimState == AimState.Down) {
                //down
                whipDown.SetActive(true);
                whipDown.GetComponent<Whip>().DoIt();
            }
            canWhip = false;
        }

        if (Input.GetButtonUp("Fire1") && currentState == PlayerState.IndianaJones) {
            LetGoOfHook();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Moving, InAir, Crouch };
public enum AimState { Forward, Up, Down, DiagUp, DiagDown };

public class Player : MonoBehaviour {

    public PlayerState currentState;
    public AimState currentAimState;

    Rigidbody2D rb;
    BoxCollider2D boxCol;
    public float hSpeed = 4;
    public float vSpeed = 8;

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
    bool canJump;
    public bool onStair;
    public bool whipping;
    public bool canWhip;

    Vector2 v;

    public Animator animator;

    void Start() {
        facingRight = true;
        canMove = true;
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int dir) {

    }

    public void CrouchEnd() {
        //if (crouchWhip) {
        //    whip.transform.position += new Vector3(0, 0.5f, 0);
        //    crouchWhip = false;
        //}
        //print("crouchend called");
        currentState = PlayerState.Idle;
        //animation
        boxCol.size = new Vector2(1, 2);
        boxCol.offset = new Vector2(0, 0);
    }

    void FixedUpdate() {
        // ground-check
        float colliderLowerEdge = transform.position.y + boxCol.offset.y - boxCol.size.y / 2;
        var checkBoxCenter = new Vector2(transform.position.x, colliderLowerEdge + 0.05f);
        Debug.DrawLine(
            new Vector2(transform.position.x - groundCheckWidth / 2, colliderLowerEdge),
            new Vector3(transform.position.x + groundCheckWidth / 2, colliderLowerEdge - groundCheckHeight));

        if (!Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, whatIsGround)) {
            if (currentState == PlayerState.Crouch) {
                CrouchEnd();
            }
            currentState = PlayerState.InAir;
        }
        else if (currentState == PlayerState.InAir) {
            currentState = PlayerState.Idle;
            canJump = true;
        }
        else {
            canJump = false;
        }
        // stair check
        if (Physics.Raycast(transform.position, Vector3.down, stairCheckDist, stairsOnly)) {
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

        if (currentState != PlayerState.InAir) {
            if (horizontalAxis != 0f && currentState != PlayerState.Crouch) {
                currentState = PlayerState.Moving;
            }
            else {
                currentState = PlayerState.Idle;
            }
        }

        // movement

        if (canMove) {
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

        if (jump && currentState != PlayerState.Crouch) {
            rb.velocity = new Vector3(rb.velocity.x, vSpeed, 0);
            canJump = false;
            jump = false;
        }
        else if (jump && currentState == PlayerState.Crouch && onStair) {

        }
    }

    void Update() {

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")) {
            if (currentState != PlayerState.InAir) {
                CrouchEnd();
                jump = true;
                //print("jump called");
            }
        }

        // crouch
        if (currentState != PlayerState.InAir) {
            if (verticalAxis < 0) {
                currentState = PlayerState.Crouch;
                boxCol.size = new Vector2(boxCol.size.x, 1f);
                boxCol.offset = new Vector2(0, -0.5f);
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
    }
}

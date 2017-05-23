using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { Idle, Moving, InAir, Crouch, KnockedBack, IndianaJones, OnStair, Dead };
public enum AimState { Right, Left, Up, Down, DiagUpRight, DiagUpLeft, DiagDownRight, DiagDownLeft };

public class Player : MonoBehaviour {       // gonan 2d actual

    public PlayerState currentState;
    public AimState currentAimState;
    public AimState lastHorizontalState;

    Rigidbody2D rb;
    CapsuleCollider2D capCol;
    public float hSpeed = 4;
    public float vSpeed = 8;
    public float swingSpeed = 1;

    public float strSpeed = 1;
    public float stairY = 1;
    public float stairX = 1;

    public float knockBack;

    public float horizontalAxis;
    public float verticalAxis;

    public float groundCheckWidth = 0.5f;
    public float groundCheckHeight = 0.5f;
    public float stairCheckHeight = 1.1f;
    public float stairCheckWidth = 0.7f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask stairsOnly;

    public int hp;
    public int playerLives = 3;

    bool facingRight;
    bool canMove;
    bool jump;
    bool knockBackDone;

    public bool onStair;
    public bool whipping;
    public bool canWhip = true;
    bool crouchWhip;
    public bool stairLeftUp;
    public bool canStopCrouch = true;

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

    int knockBackDir;

    public bool playerComesFromAbove;
    public float jointStep = 0.25f;
    public float jointMaxDist = 4;
    public float jointMinDist = 0.5f;

    public Slider playerHealthBar;
    bool canDropDown;
    PolygonCollider2D polCollider;

    public SpriteRenderer spriteRenderer;

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
        playerHealthBar.value = hp;
    }

    public void FallTrigger() {
        Death();
    }

    public void GetOnStair(bool leftUp, bool canDD) {
        if (onStair) {
            canDropDown = canDD;
            stairLeftUp = leftUp;
            currentState = PlayerState.OnStair;
            rb.gravityScale = 0;
            capCol.isTrigger = true;
        }        
    }

    public void GetOffStair() {
        if (currentState == PlayerState.OnStair) {
            currentState = PlayerState.Idle;
            rb.gravityScale = 1;
            capCol.isTrigger = false;
        }        
    }

    public void EnemyHitPlayer(int dir) {
        print("enemy hit player");
        hp--;
        playerHealthBar.value = hp;
        if (hp == 0) {
            Death();
        } else {
            fabCtrl.PlaySoundPlayerHit1();
            KnockBack(dir);
        }
    }

    public void Death() {
        fabCtrl.PlaySoundPlayerDeath();
        if (playerLives == 0) {
            //gameover
        }
        playerLives--;
        currentState = PlayerState.Dead;
        // stop animations
        print("u dieded");
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
        capCol.isTrigger = false;
    }

    public void CrouchEnd() {
        if (Physics2D.OverlapBox(groundCheck.position + new Vector3(0, 1.5f, 0), new Vector2(0.6f, 1), 0, whatIsGround)) {
            canStopCrouch = false;
        } else {
            canStopCrouch = true;
        }
        if (!canStopCrouch) return;
        if (crouchWhip) {
            whip.transform.position += new Vector3(0, 0.5f, 0);
            crouchWhip = false;
        }
        //animation
        capCol.size = new Vector2(0.63f, 2);
        capCol.offset = new Vector2(0, 0);
        currentState = PlayerState.Idle;
    }

    public void KnockBack(int dir) {
        capCol.isTrigger = true;
        print("knockback?");
        knockBackDir = dir;
        currentState = PlayerState.KnockedBack;
    }

    void FixedUpdate() {

        if (currentState == PlayerState.Dead) return;

        v = rb.velocity;    //rigidbody velocity

        if (currentState == PlayerState.KnockedBack) {
            if (!knockBackDone) {
                rb.velocity = new Vector3(knockBack * knockBackDir, knockBack, 0);
                knockBackDone = true;
                print("knockback2?");
            }
            else {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            }
            if (v.y < 0) capCol.isTrigger = false;
        }

        // ground-check

        float colliderLowerEdge = transform.position.y + capCol.offset.y - capCol.size.y / 2;
        if (currentState != PlayerState.OnStair) {
            if (!Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, whatIsGround) && currentState != PlayerState.IndianaJones) {
                if (currentState == PlayerState.KnockedBack) {
                    print("knocked back and in the air");
                }
                if (currentState == PlayerState.Crouch) {
                    CrouchEnd();
                    currentState = PlayerState.InAir;
                } else {
                    currentState = PlayerState.InAir;
                }
            } else if (currentState == PlayerState.InAir) {
                currentState = PlayerState.Idle;
            } else if (currentState == PlayerState.KnockedBack && rb.velocity.x == 0 && rb.velocity.y == 0) {
                currentState = PlayerState.Idle;
                knockBackDone = false;
            }
        }

        //stair check (are we on top of a stair?)
        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(stairCheckWidth, stairCheckHeight), 0, stairsOnly)) {
            onStair = true;
            if (currentState == PlayerState.InAir) {
                playerComesFromAbove = true;
            } else {
                playerComesFromAbove = false;
            }
        } else {
            onStair = false;
        }             

        if (horizontalAxis > 0) facingRight = true;
        if (horizontalAxis < 0) facingRight = false;

        spriteRenderer.flipX = !facingRight;


        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {

            if (v.x != 0f && currentState != PlayerState.Crouch) {
                currentState = PlayerState.Moving;
            } else if (currentState != PlayerState.Crouch) {
                currentState = PlayerState.Idle;
            }
        }

        // movement

        if (canMove && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            if (currentState == PlayerState.Crouch) {
                rb.velocity = new Vector3(horizontalAxis * (hSpeed / 2), rb.velocity.y, 0);
            }
            else {
                rb.velocity = new Vector3(horizontalAxis * hSpeed, rb.velocity.y, 0);
            }
        } else if (!canMove) {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        } else {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        

        // jump & dropdown

        if (jump && currentState != PlayerState.OnStair) {
            rb.velocity = new Vector3(rb.velocity.x, vSpeed, 0);
            jump = false;
        }
        else if (jump && currentState == PlayerState.OnStair) {
            DropDown();
            
        }

        // indiana jones
        if (currentState == PlayerState.IndianaJones) {
            Vector2 hookPos = currentHookPoint.transform.position;
            //joint.distance = Vector2.Distance(transform.position, hookPos);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookPos);
            if (transform.position.y < hookPos.y) {
                rb.velocity = new Vector3(horizontalAxis * swingSpeed, rb.velocity.y, 0);
            } else {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            }

            float dist;
            dist = joint.distance;
            if (verticalAxis > 0) {
                if (joint.distance < jointMaxDist - jointStep) {
                    joint.distance += jointStep;
                } else if (joint.distance < jointMaxDist) {
                    joint.distance = jointMaxDist;
                }
            } else if (verticalAxis < 0) {
                if (joint.distance > jointMinDist + jointStep) {
                    joint.distance -= jointStep;
                } else if (joint.distance > jointMinDist) {
                    joint.distance = jointMinDist;
                }
            } else {
                joint.distance = dist;
            }
        }        
    }

    void DropDown() {
        print("dropdown");
        GetOffStair();
    }

    void Update() {

        if (currentState == PlayerState.Dead) return;

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        // crouch
        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair) {
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

        // on stairs

        if (currentState == PlayerState.OnStair) {
            rb.velocity = new Vector3(0, 0, 0);
            float stairSpeed = strSpeed * Time.deltaTime;
            if (stairLeftUp) {
                if (verticalAxis < 0) {
                    transform.Translate(stairSpeed, -stairSpeed, 0);
                } else if (verticalAxis > 0) {
                    transform.Translate(-stairSpeed, stairSpeed, 0);
                } else if (horizontalAxis < 0) {
                    transform.Translate(-stairSpeed, stairSpeed, 0);
                } else if (horizontalAxis > 0) {
                    transform.Translate(stairSpeed, -stairSpeed, 0);
                }
            } else if (!stairLeftUp) {
                if (verticalAxis < 0) {
                    transform.Translate(-stairSpeed, -stairSpeed, 0);
                } else if (verticalAxis > 0) {
                    transform.Translate(stairSpeed, stairSpeed, 0);
                } else if (horizontalAxis < 0) {
                    transform.Translate(-stairSpeed, -stairSpeed, 0);
                } else if (horizontalAxis > 0) {
                    transform.Translate(stairSpeed, stairSpeed, 0);
                }
            }

            if (horizontalAxis < 0) facingRight = false;
            if (horizontalAxis > 0) facingRight = true;
        }

        if (Input.GetButtonDown("Jump")) {
            if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair) {
                if (currentState == PlayerState.Crouch) {
                    CrouchEnd();
                    jump = true;
                } else {
                    jump = true;
                }             
            }
            else if (currentState == PlayerState.OnStair && verticalAxis < 0) {
                DropDown();
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

        if (!Input.GetButton("Fire1") && currentState == PlayerState.IndianaJones) {
            LetGoOfHook();
        }        
    }
}

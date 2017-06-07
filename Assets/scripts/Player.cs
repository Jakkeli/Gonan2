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

    public bool disableWhipBox;

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
    bool crouchWhip;
    public bool onStair;
    public bool whipping;
    public bool canWhip = true;    
    public bool stairLeftUp;
    public bool canStopCrouch = true;

    Vector2 v;

    public GameObject whip;
    public GameObject whipRight;
    public GameObject whipLeft;
    public GameObject whipUp;
    public GameObject whipDown;
    public GameObject whipDiagUpRight;
    public GameObject whipDiagUpLeft;
    public GameObject whipDiagDownRight;
    public GameObject whipDiagDownLeft;

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
    //bool canDropDown;
    PolygonCollider2D polCollider;

    public SpriteRenderer spriteRenderer;

    GameManager gm;

    public float crouchWhipDrop = 0.7f;
    public float whipSpeed = 1;

    public int maxShurikenCount;
    public int currentShurikenCount;

    public GameObject shurikenPrefab;
    public GameObject[] shurikens;

    public int secondaryAmmo = 99;

    DBController dbc;

    public float gravity = 1;

    void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dbc = GetComponentInChildren<DBController>();
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
            //canDropDown = canDD;
            stairLeftUp = leftUp;
            currentState = PlayerState.OnStair;
            rb.gravityScale = 0;
            capCol.isTrigger = true;
        }        
    }

    public void GetOffStair() {
        //print("getoffstair");
        if (currentState == PlayerState.OnStair) {
            currentState = PlayerState.Idle;
            rb.gravityScale = gravity;
            capCol.isTrigger = false;
        }        
    }

    public void EnemyHitPlayer(int dir) {
        //print("enemy hit player");
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
        if (currentState != PlayerState.Dead) {
            currentState = PlayerState.Dead;
            dbc.PlayerDeath();
            fabCtrl.PlaySoundPlayerDeath();
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            if (playerLives == 0) {
                //gameover
                gm.GameOver();
                return;
            }
            playerLives--;
            // stop animations
            print("u dieded");
            gm.UpdateLevelLivesAmmo();
            Respawn();
        }
    }

    void Respawn() {
        print("respawn?!?!?");
    }

    public void IndianaJones(GameObject go) {
        joint.enabled = true;
        line.enabled = true;
        joint.connectedBody = go.GetComponent<Rigidbody2D>();
        currentHookPoint = go;
        currentState = PlayerState.IndianaJones;
        capCol.isTrigger = true;
        dbc.IndianaJones();
    }

    void LetGoOfHook() {
        //print("letgoofhook");
        currentState = PlayerState.Idle;
        joint.connectedBody = null;
        joint.enabled = false;
        line.enabled = false;
        capCol.isTrigger = false;
        dbc.PlayerIdle();
    }

    public void CrouchEnd() {
        if (Physics2D.OverlapBox(groundCheck.position + new Vector3(0, 1.5f, 0), new Vector2(0.6f, 1), 0, whatIsGround)) {
            canStopCrouch = false;
        } else {
            canStopCrouch = true;
        }
        if (!canStopCrouch) return;
        if (crouchWhip) {
            whip.transform.position += new Vector3(0, crouchWhipDrop, 0);
            crouchWhip = false;
        }
        capCol.size = new Vector2(0.63f, 2);
        capCol.offset = new Vector2(0, 0);
        currentState = PlayerState.Idle;
        //print("crouchend");
    }

    public void KnockBack(int dir) {
        if (currentState == PlayerState.KnockedBack) return;        
        canWhip = false;
        knockBackDir = dir;
        if (currentState != PlayerState.Dead && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair) {
            currentState = PlayerState.KnockedBack;
            rb.velocity = new Vector3(knockBack * knockBackDir, knockBack + 2, 0);            
        } else if (currentState == PlayerState.OnStair) {
            canMove = false;
            canWhip = false;
        } else if (currentState == PlayerState.IndianaJones) {
            LetGoOfHook();
            currentState = PlayerState.KnockedBack;
            canWhip = false;
            canMove = false;
        }
        dbc.Knockback();
    }

    void EndKnockback() {
        if (currentState == PlayerState.KnockedBack) {
            currentState = PlayerState.Idle;
            dbc.PlayerIdle();
            canWhip = true;
        } else if (currentState == PlayerState.OnStair) {
            //dbc.Stair_Player_Idle();
            canMove = true;
            canWhip = true;
        }
        
    }

    public void StopWhip() {
        canMove = true;
        //animator.SetBool("whip", false);   ANIMATOR ANIMATOR ANIMATOR ANIMATOR
        whipping = false;
        AnimationCheck();
    }

    void AnimationCheck() {
        if (currentState == PlayerState.Crouch) {
            dbc.PlayerCrouchIdle();
        } else if (currentState == PlayerState.InAir) {
            dbc.PlayerInAir();
        } else if (currentState == PlayerState.OnStair) {
            dbc.PlayerIdle();
        } else if (currentState == PlayerState.Moving) {
            dbc.PlayerWalk();
        } else if (currentState == PlayerState.KnockedBack) {
            dbc.Knockback();
        } else if (currentState == PlayerState.Dead) {
            dbc.Knockback();
        } else if (currentState == PlayerState.Idle) {
            dbc.PlayerIdle();
        } else if (currentState == PlayerState.IndianaJones) {
            dbc.IndianaJones();
        }
    }

    void FixedUpdate() {

        if (currentState == PlayerState.Dead) {
            return;
        }

        v = rb.velocity;    //rigidbody velocity

        // ground-check

        //float colliderLowerEdge = transform.position.y + capCol.offset.y - capCol.size.y / 2;
        if (currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            if (!Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, whatIsGround) && currentState != PlayerState.IndianaJones) {
                if (currentState == PlayerState.Crouch) {
                    CrouchEnd();
                    currentState = PlayerState.InAir;
                    if (!whipping) dbc.PlayerInAir();
                } else {
                    currentState = PlayerState.InAir;
                    if (!whipping) dbc.PlayerInAir();
                }
            } else if (currentState == PlayerState.InAir) {
                currentState = PlayerState.Idle;
                //print("groundcheckforceidle");
            }
        }
        // knockbackend check
        if (currentState == PlayerState.KnockedBack && rb.velocity.y == 0) {
            EndKnockback();
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
        // moving or idle? that is the question
        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            if (v.x != 0f && currentState != PlayerState.Crouch) {
                currentState = PlayerState.Moving;
            } else if (v.x == 0 && currentState != PlayerState.Crouch) {
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
        } else if (!canMove && currentState != PlayerState.KnockedBack) {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        } else if (currentState != PlayerState.KnockedBack) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        

        // jump & dropdown

        if (jump && currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            rb.velocity = new Vector3(rb.velocity.x, vSpeed, 0);
            jump = false;
            dbc.PlayerInAir();
        }
        else if (jump && currentState == PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            DropDown();            
        }

        // indiana jones
        if (currentState == PlayerState.IndianaJones) {
            dbc.IndianaJones();
            Vector2 hookPos = currentHookPoint.transform.position;

            line.SetPosition(0, transform.position + new Vector3(0, 1.4f, 0));
            line.SetPosition(1, hookPos);
            if (transform.position.y < hookPos.y - 1) {
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
        dbc.PlayerInAir();
        GetOffStair();
    }

    void Update() {

        if (currentState == PlayerState.Dead) return;

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        // crouch
        if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair && currentState != PlayerState.KnockedBack) {
            if (verticalAxis < 0) {
                currentState = PlayerState.Crouch;
                capCol.size = new Vector2(capCol.size.x, 1);
                capCol.offset = new Vector2(0, -0.5f);
                if (!crouchWhip) {
                    whip.transform.position += new Vector3(0, -crouchWhipDrop, 0);
                    crouchWhip = true;
                }
                //animation
            } else if (verticalAxis >= 0 && currentState == PlayerState.Crouch) {
                CrouchEnd();
            }
        }

        // on stairs

        if (currentState == PlayerState.OnStair) {
            rb.velocity = new Vector3(0, 0, 0);
            float stairSpeed = strSpeed * Time.deltaTime;
            if (stairLeftUp && !whipping) {
                if (verticalAxis < 0) {
                    transform.Translate(stairSpeed, -stairSpeed, 0);
                } else if (verticalAxis > 0) {
                    transform.Translate(-stairSpeed, stairSpeed, 0);
                } else if (horizontalAxis < 0) {
                    transform.Translate(-stairSpeed, stairSpeed, 0);
                } else if (horizontalAxis > 0) {
                    transform.Translate(stairSpeed, -stairSpeed, 0);
                }
            } else if (!stairLeftUp && !whipping) {
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
        }

        // jump input and checks
        if (Input.GetButtonDown("Jump")) {
            if (currentState != PlayerState.InAir && currentState != PlayerState.IndianaJones && currentState != PlayerState.OnStair && canMove && currentState != PlayerState.KnockedBack) {
                if (currentState == PlayerState.Crouch) {
                    CrouchEnd();
                    jump = true;
                } else {
                    jump = true;
                }
            } else if (currentState == PlayerState.OnStair && verticalAxis < 0 && currentState != PlayerState.KnockedBack) {
                DropDown();
            }
        }

        // animation set
        if (!whipping) {
            if (v.x != 0) {         
                if (currentState == PlayerState.Crouch) {
                    dbc.PlayerCrouchWalk();
                } else if (currentState == PlayerState.InAir) {
                    dbc.PlayerInAir();
                } else if (currentState == PlayerState.Moving) {
                    dbc.PlayerWalk();
                } else if (currentState == PlayerState.KnockedBack) {
                    dbc.Knockback();
                }
            }
            if (v.x == 0) {
                if (currentState == PlayerState.Crouch) {
                    dbc.PlayerCrouchIdle();
                } else if (currentState == PlayerState.Idle) {
                    dbc.PlayerIdle();
                } else if (currentState == PlayerState.InAir) {
                    dbc.PlayerInAir();
                }
            }
        }
        
        // aiming
        if (whipping) {
            //nope
        } else {
            if (horizontalAxis > 0) {
                currentAimState = AimState.Right;
                lastHorizontalState = AimState.Right;
                if (verticalAxis > 0f) {
                    currentAimState = AimState.DiagUpRight;
                } else if (verticalAxis < 0f && currentState == PlayerState.InAir) {
                    currentAimState = AimState.DiagDownRight;
                }
            } else if (horizontalAxis < 0f) {
                currentAimState = AimState.Left;
                lastHorizontalState = AimState.Left;
                if (verticalAxis > 0f) {
                    currentAimState = AimState.DiagUpLeft;
                } else if (verticalAxis < 0f && currentState == PlayerState.InAir) {
                    currentAimState = AimState.DiagDownLeft;
                }
            } else if (horizontalAxis == 0) {
                if (verticalAxis < 0 && currentState == PlayerState.InAir) {
                    currentAimState = AimState.Down;
                } else if (verticalAxis > 0) {
                    currentAimState = AimState.Up;
                } else {
                    currentAimState = lastHorizontalState;
                }
            }
        }
        

        // shooting
        if (Input.GetButtonDown("Fire1") && canWhip) {
            whipping = true;

            // shooting forward

            if (currentAimState == AimState.Right) {
                // whip right
                whipRight.SetActive(true);
                whipRight.GetComponent<Whip>().DoIt();
                if (currentState == PlayerState.Crouch) {
                    dbc.CrouchWhip();
                } else if (currentState == PlayerState.InAir) {
                    dbc.JumpWhip();
                } else {
                    dbc.Whip();
                }
            } else if (currentAimState == AimState.Left) {
                // whip left
                whipLeft.SetActive(true);
                whipLeft.GetComponent<Whip>().DoIt();
                if (currentState == PlayerState.Crouch) {
                    dbc.CrouchWhip();
                } else if (currentState == PlayerState.InAir) {
                    dbc.JumpWhip();
                } else {
                    dbc.Whip();
                }
            }

            // shooting upwards
            if (currentAimState == AimState.DiagUpRight) {
                //diagupright
                whipDiagUpRight.SetActive(true);
                whipDiagUpRight.GetComponent<Whip>().DoIt();
                if (currentState == PlayerState.InAir) dbc.JumpWhipDiagUp();
                if (currentState != PlayerState.InAir) dbc.WhipDiag();
            }
            if (currentAimState == AimState.DiagUpLeft) {
                //diagupleft
                whipDiagUpLeft.SetActive(true);
                whipDiagUpLeft.GetComponent<Whip>().DoIt();
                if (currentState == PlayerState.InAir) dbc.JumpWhipDiagUp();
                if (currentState != PlayerState.InAir) dbc.WhipDiag();
            }
            if (currentAimState == AimState.Up) {
                //up
                whipUp.SetActive(true);
                whipUp.GetComponent<Whip>().DoIt();
                if (currentState == PlayerState.InAir) dbc.JumpWhipUp();
                if (currentState != PlayerState.InAir) dbc.WhipUp();
            }

            // shooting downwards
            if (currentAimState == AimState.DiagDownRight && currentState == PlayerState.InAir) {
                //diagdownright
                whipDiagDownRight.SetActive(true);
                whipDiagDownRight.GetComponent<Whip>().DoIt();
                dbc.JumpWhipDiagDown();
            }
            if (currentAimState == AimState.DiagDownLeft && currentState == PlayerState.InAir) {
                //diagdownleft
                whipDiagDownLeft.SetActive(true);
                whipDiagDownLeft.GetComponent<Whip>().DoIt();
                dbc.JumpWhipDiagDown();
            }
            if (currentState == PlayerState.InAir && currentAimState == AimState.Down) {
                //down
                whipDown.SetActive(true);
                whipDown.GetComponent<Whip>().DoIt();
                dbc.JumpWhipDown();
            }
            canWhip = false;
            if (currentState != PlayerState.InAir) canMove = false;
        }

        // secondary fire

        if (Input.GetButtonDown("Fire2") && currentState != PlayerState.IndianaJones && currentState != PlayerState.KnockedBack) {
            if (currentShurikenCount < maxShurikenCount && secondaryAmmo > 0) {
                GameObject shrkn = null;
                for (int i = 0; i < maxShurikenCount; i++) {
                    if (!shurikens[i].GetComponent<Shuriken>().wasThrown) {
                        shrkn = shurikens[i];
                    }
                }
                if (shrkn == null) return;
                bool crouch;
                if (currentState == PlayerState.Crouch) {
                    crouch = true;
                } else {
                    crouch = false;
                }
                shrkn.GetComponent<Shuriken>().Throw(facingRight ? 1 : -1, crouch);
                currentShurikenCount++;
                secondaryAmmo--;
                gm.UpdateLevelLivesAmmo();
            }
        }

        if (Input.GetButtonUp("Fire1") && currentState == PlayerState.IndianaJones) {
            LetGoOfHook();
        }

        if (!Input.GetButton("Fire1") && currentState == PlayerState.IndianaJones) {
            LetGoOfHook();
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            Death();
        }

        // flipX

        if (horizontalAxis < 0 && facingRight && canMove && !whipping && currentState != PlayerState.IndianaJones) {
            facingRight = false;
            dbc.FaceLeft();
        } else if (horizontalAxis > 0 && !facingRight && canMove && !whipping && currentState != PlayerState.IndianaJones) {
            facingRight = true;
            dbc.FaceRight();
        }

        //onstair animation

        if (currentState == PlayerState.OnStair && !whipping) {
            if (horizontalAxis != 0) dbc.PlayerWalk();
            if (horizontalAxis == 0) dbc.PlayerIdle();
        }
    }
}

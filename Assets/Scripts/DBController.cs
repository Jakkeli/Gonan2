using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DBController : MonoBehaviour {

    public UnityArmatureComponent uac;

	void Start () {
        uac.animation.Play("Standing_Idle");
	}

    public void FaceRight() {
        transform.localScale = new Vector3(0.38f, 0.38f, 038f);
    }

    public void FaceLeft() {
        transform.localScale = new Vector3(-0.38f, 0.38f, 038f);
        uac.flipX = true;
    }

    public void PlayerIdle() {
        if (uac.animation.lastAnimationName != "Standing_Idle") uac.animation.Play("Standing_Idle");
    }

    public void PlayerCrouchIdle() {
        if (uac.animation.lastAnimationName != "Crouch_Idle") uac.animation.Play("Crouch_Idle");
    }

    public void PlayerWalk() {
        //uac.animation.Play("Standing_Walk", 1);
        if (uac.animation.lastAnimationName != "Standing_Walk") uac.animation.Play("Standing_Walk");
    }

    public void PlayerCrouchWalk() {
        if (uac.animation.lastAnimationName != "Crouch_walk") uac.animation.Play("Crouch_walk");
    }

    public void PlayerInAir() {
        //if (uac.animation.lastAnimationName != "InAir") uac.animation.Play("InAir");
        if (uac.animation.lastAnimationName != "Crouch_Idle") uac.animation.Play("Crouch_Idle");
    }

    public void Whip() {
        if (uac.animation.lastAnimationName != "Standing_Whip") uac.animation.Play("Standing_Whip");
    }

    public void CrouchWhip() {
        if (uac.animation.lastAnimationName != "Crouch_Whip") uac.animation.Play("Crouch_Whip");
    }

    public void ThrowShuriken() {
        //if (uac.animation.lastAnimationName != "ThrowShuriken") uac.animation.Play("ThrowShuriken");
    }

    public void PlayerJump() {
        //if (uac.animation.lastAnimationName != "PlayerJump") uac.animation.Play("PlayerJump");
    }

    public void Knockback() {
        //if (uac.animation.lastAnimationName != "Knockback") uac.animation.Play("Knockback");
        if (uac.animation.lastAnimationName != "Crouch_Idle") uac.animation.Play("Crouch_Idle");
    }

    public void PlayerDeath() {
        //if (uac.animation.lastAnimationName != "PlayerDeath") uac.animation.Play("PlayerDeath");
    }
}

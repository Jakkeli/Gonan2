using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DBController : MonoBehaviour {

    UnityArmatureComponent uac;
    //Player player;

    bool stopWhip;

	void Start () {
        //player = GameObject.Find("player").GetComponent<Player>();
        uac = GetComponent<UnityArmatureComponent>();
        uac.animation.Play("Standing_Idle");
        transform.localScale = new Vector3(0.38f, 0.38f, 1);
        transform.position -= new Vector3(0, -0.24f, 0);
	}

    public void FaceRight() {
        uac.armature.flipX = false;
    }

    public void FaceLeft() {
        uac.armature.flipX = true;
    }

    public void PlayerIdle() {
        if (uac.animation.lastAnimationName != "Standing_Idle") uac.animation.Play("Standing_Idle");
    }

    public void PlayerCrouchIdle() {
        if (uac.animation.lastAnimationName != "Crouch_Idle") uac.animation.Play("Crouch_Idle");
    }

    public void PlayerWalk() {
        if (uac.animation.lastAnimationName != "Standing_Walk") uac.animation.Play("Standing_Walk");
    }

    public void PlayerCrouchWalk() {
        if (uac.animation.lastAnimationName != "Crouch_Walk") uac.animation.Play("Crouch_Walk");
    }

    public void PlayerInAir() {
        if (uac.animation.lastAnimationName != "Jump_Idle") uac.animation.Play("Jump_Idle");
    }

    public void Whip() {
        if (uac.animation.lastAnimationName != "Standing_Whip") uac.animation.Play("Standing_Whip");    
    }

    public void CrouchWhip() {
        if (uac.animation.lastAnimationName != "Crouch_Whip") uac.animation.Play("Crouch_Whip");
    }

    public void WhipUp() {
        if (uac.animation.lastAnimationName != "Standing_WhipUP") uac.animation.Play("Standing_WhipUP");
    }

    public void WhipDiag() {
        if (uac.animation.lastAnimationName != "Standing_WhipAngle") uac.animation.Play("Standing_WhipAngle");
    }

    public void JumpWhip() {
        if (uac.animation.lastAnimationName != "Jump_Whip") uac.animation.Play("Jump_Whip");
    }

    public void JumpWhipDiagUp() {
        if (uac.animation.lastAnimationName != "Jump_WhipAngleUP") uac.animation.Play("Jump_WhipAngleUP");
    }

    public void JumpWhipDiagDown() {
        if (uac.animation.lastAnimationName != "Jump_WhipAngleDown") uac.animation.Play("Jump_WhipAngleDown");
    }

    public void JumpWhipUp() {
        if (uac.animation.lastAnimationName != "Jump_WhipUP") uac.animation.Play("Jump_WhipUP");
    }

    public void JumpWhipDown() {
        if (uac.animation.lastAnimationName != "Jump_WhipDown") uac.animation.Play("Jump_WhipDown");
    }

    public void ThrowShurikenStanding() {
        if (uac.animation.lastAnimationName != "Standing_Throw") uac.animation.Play("Standing_Throw");
    }

    public void ThrowShurikenCrouch() {
        if (uac.animation.lastAnimationName != "Crouch_Throw") uac.animation.Play("Crouch_Throw");
    }

    public void ThrowShurikenInAir() {
        if (uac.animation.lastAnimationName != "Jump_Throw") uac.animation.Play("Jump_Throw");
    }

    public void PlayerJump() {
       if (uac.animation.lastAnimationName != "Jump_Idle") uac.animation.Play("Jump_Idle");
    }

    public void Knockback() {
        if (uac.animation.lastAnimationName != "Knockback") uac.animation.Play("Knockback");
    }

    public void PlayerDeath() {
        uac.animation.Play("Die", 1);
    }

    public void IndianaJones() {
        if (uac.animation.lastAnimationName != "NinjaRope") uac.animation.Play("NinjaRope");
    }

    public void StairsIdleUp() {
        if (uac.animation.lastAnimationName != "StairsUP_Idle") uac.animation.Play("StairsUP_Idle");
    }

    public void StairsIdleDown() {
        if (uac.animation.lastAnimationName != "StairsDown_Idle") uac.animation.Play("StairsDown_Idle");
    }

    public void StairsWalkDown() {
        if (uac.animation.lastAnimationName != "StairsDown_Walk") uac.animation.Play("StairsDown_Walk");
    }

    public void StairsWalkUp() {
        if (uac.animation.lastAnimationName != "StairsUP_Walk") uac.animation.Play("StairsUP_Walk");
    }

    public void StairsWhip(bool facingRight, bool stairLeftUp) {
        if (facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsDown_Whip") uac.animation.Play("StairsDown_Whip");
        if (!facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsUP_Whip") uac.animation.Play("StairsUP_Whip");
        if (!facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsDown_Whip") uac.animation.Play("StairsDown_Whip");
        if (facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsUP_Whip") uac.animation.Play("StairsUP_Whip");
    }

    public void StairsWhipDiag(bool facingRight, bool stairLeftUp) {
        if (facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsDown_WhipAngleUP") uac.animation.Play("StairsDown_WhipAngleUP");
        if (!facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsUP_WhipAngleUP") uac.animation.Play("StairsUP_WhipAngleUP");
        if (!facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsDown_WhipAngleUP") uac.animation.Play("StairsDown_WhipAngleUP");
        if (facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsUP_WhipAngleUP") uac.animation.Play("StairsUP_WhipAngleUP");
    }

    public void StairsWhipUp(bool facingRight, bool stairLeftUp) {
        if (facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsDown_WhipUP") uac.animation.Play("StairsDown_WhipUP");
        if (!facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsUP_WhipUP") uac.animation.Play("StairsUP_WhipUP");
        if (!facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsDown_WhipUP") uac.animation.Play("StairsDown_WhipUP");
        if (facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsUP_WhipUP") uac.animation.Play("StairsUP_WhipUP");
    }

    public void StairsThrowShuriken(bool facingRight, bool stairLeftUp) {
        if (facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsDown_Throw") uac.animation.Play("StairsDown_Throw");
        if (!facingRight && stairLeftUp && uac.animation.lastAnimationName != "StairsUP_Throw") uac.animation.Play("StairsUP_Throw");
        if (!facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsDown_Throw") uac.animation.Play("StairsDown_Throw");
        if (facingRight && !stairLeftUp && uac.animation.lastAnimationName != "StairsUP_Throw") uac.animation.Play("StairsUP_Throw");
    }

    void Update() {
    }
}

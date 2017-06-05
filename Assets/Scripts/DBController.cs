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
        //if (uac.animation.lastAnimationName != "Jump_Whip_DiagUp") uac.animation.Play("Jump_Whip_DiagUp");
        if (uac.animation.lastAnimationName != "Jump_Whip") uac.animation.Play("Jump_Whip");
        print("animation missing");
    }

    public void JumpWhipDiagDown() {
        //if (uac.animation.lastAnimationName != "Jump_Whip_DiagDown") uac.animation.Play("Jump_Whip_DiagDown");
        if (uac.animation.lastAnimationName != "Jump_Whip") uac.animation.Play("Jump_Whip");
        print("animation missing");
    }

    public void JumpWhipUp() {
        //if (uac.animation.lastAnimationName != "Jump_Whip_Up") uac.animation.Play("Jump_Whip_Up");
        if (uac.animation.lastAnimationName != "Jump_Whip") uac.animation.Play("Jump_Whip");
        print("animation missing");
    }

    public void JumpWhipDown() {
        //if (uac.animation.lastAnimationName != "Jump_Whip_Down") uac.animation.Play("Jump_Whip_Down");
        if (uac.animation.lastAnimationName != "Jump_Whip") uac.animation.Play("Jump_Whip");
        print("animation missing");
    }

    public void ThrowShuriken() {
        //if (uac.animation.lastAnimationName != "ThrowShuriken") uac.animation.Play("ThrowShuriken");
    }

    public void PlayerJump() {
       if (uac.animation.lastAnimationName != "Jump_Idle") uac.animation.Play("Jump_Idle");
    }

    public void Knockback() {
        //if (uac.animation.lastAnimationName != "Knockback") uac.animation.Play("Knockback");
        if (uac.animation.lastAnimationName != "Crouch_Idle") uac.animation.Play("Crouch_Idle");
    }

    public void PlayerDeath() {
        //if (uac.animation.lastAnimationName != "PlayerDeath") uac.animation.Play("PlayerDeath");
        print("death animation missing");
    }

    public void IndianaJones() {
        //if (uac.animation.lastAnimationName != "IndianaJones") uac.animation.Play("IndianaJones");
        if (uac.animation.lastAnimationName != "Jump_Idle") uac.animation.Play("Jump_Idle");
        print("ij animation missing");
    }

    void Update() {

    }
}

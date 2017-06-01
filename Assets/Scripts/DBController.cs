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
        uac.flipX = false;
    }

    public void FaceLeft() {
        uac.flipX = true;
    }

    public void PlayerIdle() {
        uac.animation.Play("Standing_Idle");
    }

    public void PlayerCrouchIdle() {
        uac.animation.Play("Crouch_Idle");
    }

    public void PlayerWalk() {
        uac.animation.Play("Standing_Walk");
    }

    public void PlayerCrouchWalk() {
        uac.animation.Play("Crouch_walk");
    }

    public void PlayerInAir() {
        //uac.animation.Play("InAir");
    }

    public void Whip() {
        uac.animation.Play("Standing_Whip");
    }

    public void CrouchWhip() {
        uac.animation.Play("Crouch_Whip");
    }

    public void Shuriken() {

    }

    public void PlayerJump() {

    }

    public void Knockback() {

    }

    public void PlayerDeath() {

    }
}

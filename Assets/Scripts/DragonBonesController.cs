using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DragonBonesController : MonoBehaviour {

    void Start() {
        // Load data.
        UnityFactory.factory.LoadDragonBonesData("GonanMoves/GonanMoves_ske"); // DragonBones file path (without suffix)

        UnityFactory.factory.LoadTextureAtlasData("GonanMoves/GonanMoves_tex"); //Texture atlas file path (without suffix) 
        // Create armature.
        //var armatureComponent = UnityFactory.factory.BuildArmatureComponent("ubbie"); // Input armature name
                                                                                      // Play animation.
        //armatureComponent.animation.Play("walk");

        // Change armatureposition.
        //armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        //playerArmature = UnityFactory.factory.BuildArmatureComponent("ubbie");
    }

    public void PlayerIdle() {

    }

    public void PlayerWalk() {

    }

    public void Whip() {

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

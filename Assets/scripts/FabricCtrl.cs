using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricCtrl : MonoBehaviour {
    public string whipPlay;
    public string enemy1Destroy;
    public string pickUp;

    public Fabric.GroupComponent music;
    public Fabric.GroupComponent sfx;

    void MuteMusic() {
        if (music.Mute) {
            music.Mute = false;
        }
        else {
            music.Mute = true;
        }
    }

    void MuteSFX() {
        if (sfx.Mute) {
            sfx.Mute = false;
        }
        else {
            sfx.Mute = true;
        }
    }

}

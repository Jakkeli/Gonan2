using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricCtrl : MonoBehaviour {
    public string whipPlay;
    public string enemy1Destroy;
    public string pickUp;

    public Fabric.GroupComponent music;
    public Fabric.GroupComponent sfx;

    public GameObject soundPos;

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

    public void PlaySoundWhip1() {
        Fabric.EventManager.Instance.PostEvent(whipPlay, soundPos);
    }

    public void PlaySoundEnemy1Destroyed() {
        Fabric.EventManager.Instance.PostEvent(enemy1Destroy, soundPos);
    }

    public void PlaySoundPickup() {
        Fabric.EventManager.Instance.PostEvent(pickUp, soundPos);
    }
}

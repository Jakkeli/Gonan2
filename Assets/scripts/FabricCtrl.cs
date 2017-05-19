using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricCtrl : MonoBehaviour {
    public string whipPlay;
    public string secondaryWeapon;
    public string enemy1Destroy;
    //public string enemy2Destroy;
    public string pickUp;
    public string enemy1Hit;
    //public string enemy2Hit;
    //public string playerHit1;
    //public string playerDeath;
    public string drumBeat1;

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

    public void PlaySoundWhip2() {
        //Fabric.EventManager.Instance.PostEvent(whipPlay2, soundPos);
    }

    public void PlaySoundPlayerHit1() {
        //Fabric.EventManager.Instance.PostEvent(playerHit1, soundPos);
    }

    public void PlaySoundPlayerDeath() {
        //Fabric.EventManager.Instance.PostEvent(playerDeath, soundPos);
    }

    public void PlaySoundEnemy1Destroyed() {
        Fabric.EventManager.Instance.PostEvent(enemy1Destroy, soundPos);
    }

    public void PlaySoundEnemy2Destroyed() {
        //Fabric.EventManager.Instance.PostEvent(enemy2Destroy, soundPos);
    }

    public void PlaySoundEnemy1Hit() {
        Fabric.EventManager.Instance.PostEvent(enemy1Hit, soundPos);
    }

    public void PlaySoundEnemy2Hit() {
        //Fabric.EventManager.Instance.PostEvent(enemy2Hit, soundPos);
    }

    public void PlaySoundPickup() {
        Fabric.EventManager.Instance.PostEvent(pickUp, soundPos);
    }

    public void PlayMusicDrumBeat1() {
        Fabric.EventManager.Instance.PostEvent(drumBeat1, soundPos);
    }
}

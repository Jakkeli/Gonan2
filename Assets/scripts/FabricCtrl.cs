using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricCtrl : MonoBehaviour {
    public string whipPlay;
    public string laserWhipPlay;
    public string secondaryWeapon;
    public string enemy1Destroy;
    //public string enemy2Destroy;
    public string pickUp;
    public string enemy1Hit;
    //public string enemy2Hit;
    //public string playerHit1;
    public string playerDeath;
    public string drumBeat1;
    public string bossLaugh1;
    public string stopBossLaugh1;
    public string bossLaugh2;
    public string bossLaugh3;

    public string menuMusicPlay;
    public string menuMusicPause;
    public string menuMusicUnPause;
    public string menuMusicStop;
    public string gameMusicStop;
    public string gameMusicPause;
    public string gameMusicUnPause;
    public string gameMusicPlay;

    public Fabric.GroupComponent music;
    public Fabric.GroupComponent sfx;

    public GameObject soundPos;
    bool menuMusicStarted;
    bool gameMusicStarted;

    public void MuteMusic() {
        if (music.Mute) {
            music.Mute = false;
        }
        else {
            music.Mute = true;
        }
    }

    public void MuteSFX() {
        if (sfx.Mute) {
            sfx.Mute = false;
        }
        else {
            sfx.Mute = true;
        }
    }

    void Respawn() {
        menuMusicStarted = false;
        gameMusicStarted = false;
    }

    public void PlayMenuMusic() {
        if (!menuMusicStarted) {
            Fabric.EventManager.Instance.PostEvent(menuMusicPlay, soundPos);
            menuMusicStarted = true;
        } else {
            Fabric.EventManager.Instance.PostEvent(menuMusicUnPause, soundPos);
        }        
    }

    public void PauseMenuMusic() {
        Fabric.EventManager.Instance.PostEvent(menuMusicPause, soundPos);
    }

    public void StopMenuMusic() {
        Fabric.EventManager.Instance.PostEvent(menuMusicStop, soundPos);
    }

    public void StopGameMusic() {
        Fabric.EventManager.Instance.PostEvent(gameMusicStop, soundPos);
    }

    public void PauseGameMusic() {
        Fabric.EventManager.Instance.PostEvent(gameMusicPause, soundPos);
    }

    public void PlayGameMusic() {
        if (!gameMusicStarted) {
            Fabric.EventManager.Instance.PostEvent(gameMusicPlay, soundPos);
            gameMusicStarted = true;
        } else {
            Fabric.EventManager.Instance.PostEvent(gameMusicUnPause, soundPos);
        }        
    }

    public void PlaySoundBossLaugh1() {
        Fabric.EventManager.Instance.PostEvent(bossLaugh1, soundPos);
    }

    public void StopSoundBossLaugh1() {
        Fabric.EventManager.Instance.PostEvent(stopBossLaugh1, soundPos);
    }

    public void PlaySoundBossLaugh2() {
        Fabric.EventManager.Instance.PostEvent(bossLaugh2, soundPos);
    }

    public void PlaySoundBossLaugh3() {
        Fabric.EventManager.Instance.PostEvent(bossLaugh3, soundPos);
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

    public void PlaySoundEnemy3Destroyed() {
        //Fabric.EventManager.Instance.PostEvent(enemy3Destroy, soundPos);
    }

    public void PlaySoundEnemy1Hit() {
        Fabric.EventManager.Instance.PostEvent(enemy1Hit, soundPos);
    }

    public void PlaySoundEnemy2Hit() {
        //Fabric.EventManager.Instance.PostEvent(enemy2Hit, soundPos);
    }

    public void PlaySoundEnemy3Hit() {
        //Fabric.EventManager.Instance.PostEvent(enemy3Hit, soundPos);
    }

    public void PlaySoundPickup() {
        Fabric.EventManager.Instance.PostEvent(pickUp, soundPos);
    }

    public void PlayMusicDrumBeat1() {
        Fabric.EventManager.Instance.PostEvent(drumBeat1, soundPos);
    }
}

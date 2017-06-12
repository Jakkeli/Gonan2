using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionOnClick : MonoBehaviour {

    bool playClicked;
    Text playContinue;
    Image bgBlack;
    GameManager gm;
    bool fadingIn;
    bool fadingOut;
    bool fadingDone;
    Fader fader;

    void Awake() {
        playContinue = GameObject.Find("playContinueText").GetComponent<Text>();
        bgBlack = GameObject.Find("bg_black").GetComponent<Image>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        fader = GameObject.Find("GameManager").GetComponent<Fader>();
    }

    public void PlayContinueButton() {
        print("start game");
        if (!playClicked) {            
            playContinue.text = "CONTINUE";
            playClicked = true;
        }
        bgBlack.enabled = false;
        gm.StartContinueGame();
    }

    public void ExitButton() {
        print("quit");
    }

    public void AudioSettingsMenuButton() {
        print("audiosettings");
    }

    public void HelpMenuButton() {
        print("helpmenu");
    }

    public void BackButton() {
        print("back");
    }

    void FadeInOut() {
        fadingDone = false;
        fadingOut = true;
        fader.Fade(false, 0.5f);
    }

    void Update() {
        if (fadingDone) return;
        if (fadingOut) {
            if (!fader.inTransition) {
                fadingOut = false;
                fader.Fade(true, 0.5f);
                fadingIn = true;
            }
        }
        if (fadingIn) {
            if (!fader.inTransition) {
                fadingIn = false;
                fadingDone = true;
            }
        }
    }
}

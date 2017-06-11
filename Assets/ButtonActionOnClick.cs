using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionOnClick : MonoBehaviour {

    bool playClicked;
    Text playContinue;
    Image bgBlack;

    void Awake() {
        playContinue = GameObject.Find("playContinueText").GetComponent<Text>();
        bgBlack = GameObject.Find("bg_black").GetComponent<Image>();
    }

    public void PlayContinueButton() {
        print("start game");
        if (!playClicked) {
            playContinue.text = "CONTINUE";
            playClicked = true;
        }
        bgBlack.enabled = false;
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
}

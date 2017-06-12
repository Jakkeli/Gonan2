using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    public Image fadeImage;
    public float fadeTime;
    public bool inTransition;
    public bool isShowing;
    float fadeTarget;

    public void Fade(bool fadeIn, float duration) { // fadeIn true = from black, false = to black
        inTransition = true;
        fadeTime = duration;
        fadeTarget = fadeIn ? 0 : 1;
        isShowing = fadeIn;
    }

    void Start() {

    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.M)) {// from black
            Fade(true, 3);
        }
        if (Input.GetKeyDown(KeyCode.N)) {// to black
            Fade(false, 3);
        }

        if (!inTransition) return;

        fadeTarget += isShowing ? Time.deltaTime * 1 / fadeTime : -Time.deltaTime * 1 / fadeTime;
        fadeImage.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), fadeTarget);

        if (fadeTarget < 0 || fadeTarget > 1) inTransition = false;
	}
}

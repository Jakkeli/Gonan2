using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LavaLampState { Inactive, Activated, Destroyed };

public class LavaLamp : MonoBehaviour, IReaction {

    public LavaLampState currentState;
    public bool seenAtStart;

    void Start () {
        if (seenAtStart) {
            Activate();
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}

    public void React() {

    }

    public void Activate() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

	void Update () {
		
	}
}

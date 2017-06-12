using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LavaLampState { Inactive, Activated, Destroyed };
public enum ItemDropType { Blob, Shuriken };

public class LavaLamp : MonoBehaviour, IReaction {

    public LavaLampState currentState;
    public ItemDropType itemDropType;
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
        if (itemDropType == ItemDropType.Blob) {
            GetComponentInChildren<BobTheBlob>().DropBlob();
        } else if (itemDropType == ItemDropType.Shuriken) {
            //GetComponentInChildren<ShurikenDrop>().DropShuriken();
        }
        Deactivate();
    }

    public void Activate() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Deactivate() {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

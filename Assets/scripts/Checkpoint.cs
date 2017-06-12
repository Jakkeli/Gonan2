using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameBlock thisCheckpoint;
    GameManager gm;
    public bool isFirstCheckpoint;
    public int checkPointIndex;

    void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.tag != "Player" || isFirstCheckpoint) return;
        if (checkPointIndex <= gm.checkPointIndex) return;
        gm.currentBlock = thisCheckpoint;
        gm.currentCheckpoint = gameObject;
        gm.checkPointIndex = checkPointIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour {

    float tickTime;
    float timer = 0.07f;
    bool called;
    Player player;

    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    void Activate() {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        called = false;
    }

    public void DoIt() {
        tickTime = 0;
        called = true;
        Invoke("PutBack", 0.3f);
        player.whipping = true;
    }

    void PutBack() {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        player.GetComponent<Player>().canWhip = true;
        player.whipping = false;
    }

    void Update() {
        if (called) {
            tickTime += Time.deltaTime;
            if (tickTime >= timer) {
                Activate();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<IReaction>() != null && col.tag == "enemy") {
            col.GetComponent<IReaction>().React();
        }
        else if (col.GetComponent<IReaction>() != null && col.tag == "item") {
            col.GetComponent<IReaction>().React();
            //print("i should cause a reaction");
        }
        else if (col.GetComponent<IReaction>() == null) {
            //print("ireaction is null");
        }
    }
}

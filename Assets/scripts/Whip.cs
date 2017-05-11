using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour {

    float tickTime;
    float timer = 0.07f;
    bool called;
    Player player;
    public AudioClip whipSound;
    

    private void Start() {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    void Activate() {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        called = false;
    }

    public void DoIt() {
        tickTime = 0;
        called = true;
        Invoke("PutBack", 0.3f);
        //player.GetComponent<Player>().canMove = false;
        player.whipping = true;
    }
	
    void PutBack() {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        //gameObject.SetActive(false);
        player.GetComponent<Player>().canWhip = true;
        //player.GetComponent<Player>().canMove = true;
        player.whipping = false;
    }

	void Update () {
		if (called) {
            tickTime += Time.deltaTime;
            if (tickTime >= timer) {
                Activate();
            }
        }
	}

    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<IReaction>() != null && col.tag == "enemy") {
            col.GetComponent<IReaction>().React();
        } else if (col.GetComponent<IReaction>() != null && col.tag == "item") {
            col.GetComponent<IReaction>().React();
        }
    }

}

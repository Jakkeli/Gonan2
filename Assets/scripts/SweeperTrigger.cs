using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperTrigger : MonoBehaviour {

    public GameObject mySweeper;
    Enemy3Ground e3g;

    bool wasTriggered;

	void Start () {
        e3g = mySweeper.GetComponent<Enemy3Ground>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && !wasTriggered) {
            e3g.Drop();
            wasTriggered = true;
        }
    }
}

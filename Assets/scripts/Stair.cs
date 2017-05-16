using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    public bool stairLeftUp;

	void Start () {
		
	}

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            if (stairLeftUp) col.gameObject.GetComponent<Player>().stairLeftUp = true;
            if (!stairLeftUp) col.gameObject.GetComponent<Player>().stairLeftUp = false;
        }
    }
}

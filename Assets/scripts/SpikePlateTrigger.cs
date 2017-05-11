using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlateTrigger : MonoBehaviour {

    SpikePlate sp;
    bool used;

	void Start () {
        sp = GetComponentInParent<SpikePlate>();
	}

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player" && !used) {
            sp.TriggerDrop();
            used = true;
        }
    }
}

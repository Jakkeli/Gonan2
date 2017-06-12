using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerFight : MonoBehaviour {

    bool used;

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && !used) {
            GetComponentInParent<BossOne>().TriggerFight();
            used = true;
        }
    }
}

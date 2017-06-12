using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerFight : MonoBehaviour {

    bool used;
    public GameObject boss;

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player" && !used) {
            boss.GetComponent<BossOne>().TriggerFight();
            used = true;
        }
    }
}

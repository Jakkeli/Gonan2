using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerFight : MonoBehaviour {

    public bool used;
    public GameObject boss;

    void OnTriggerExit2D(Collider2D c) {
        if (c.tag == "Player" && !used && GameObject.Find("player").transform.position.x > transform.position.x) {
            boss.GetComponent<BossOne>().TriggerFight();
            used = true;
        }
    }
}

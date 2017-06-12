using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, IReaction {

    float invunerabilityTime = 0.5f;
    bool godMode;
    float tickTime;

	void Start () {
		
	}

    public void Activate() {

    }

    public void React() {
        if (!godMode) {
            GetComponentInParent<BossOne>().TakeDamage();
            print("reacted");
            tickTime = 0;
            godMode = true;
        }
        
    }

	void Update () {
		if (godMode) {
            tickTime += Time.deltaTime;
            if (tickTime > invunerabilityTime) {
                godMode = false;
            }
        }
	}

    
}

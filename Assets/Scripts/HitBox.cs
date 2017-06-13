using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, IReaction {

    float invunerabilityTime = 0.5f;
    bool godMode;
    float tickTime;
    ParticleSystem[] ps;
    public bool editorMode;

    void Start () {
        ps = GetComponentsInChildren<ParticleSystem>();
    }

    public void Activate() {

    }

    public void React() {
        if (!godMode) {
            GetComponentInParent<BossOne>().TakeDamage();
            print("reacted");
            tickTime = 0;
            godMode = true;
            foreach (ParticleSystem parSys in ps)
            {
                parSys.Play();
            }
        }
        
    }

	void Update () {
		if (godMode) {
            tickTime += Time.deltaTime;
            if (tickTime > invunerabilityTime) {
                godMode = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && editorMode)
        {
            React();
        }
	}

    
}

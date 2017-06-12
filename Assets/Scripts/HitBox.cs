using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, IReaction {

	

	void Start () {
		
	}

    public void Activate() {

    }

    public void React() {
        GetComponentInParent<BossOne>().TakeDamage();
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    
}

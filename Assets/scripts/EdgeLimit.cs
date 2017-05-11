using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLimit : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider c) {
        if (c.GetComponent<Enemy>() != null) {
            if (c.GetComponent<Enemy>().myType == EnemyType.Walker) {
                c.GetComponent<Enemy>().EdgeStop();
            }
        } 
    }

    void OnTriggerExit(Collider c) {
        if (c.GetComponent<Enemy>() != null) {
            if (c.GetComponent<Enemy>().myType == EnemyType.Walker) {
                c.GetComponent<Enemy>().EdgeLeft();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlate : MonoBehaviour {

    bool triggered;
    public float dropSpeed;
    public float dropMinY;
    bool used;

    
	
	public void TriggerDrop() {
        triggered = true;
    }
	
	
	void Update () {
		if (triggered && !used) {
            dropSpeed *= 1.05f;
            transform.position += new Vector3(0, dropSpeed * -1, 0) * Time.deltaTime;
        }

        if (transform.position.y <= dropMinY) {
            used = true;
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            triggered = true;
        }
	}
}

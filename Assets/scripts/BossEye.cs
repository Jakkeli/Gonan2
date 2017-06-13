using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour {

    public GameObject target;
    Vector3 targetPos;
    

	void Start () {
        
	}
	
	void Update () {
        targetPos = target.transform.position;
        Vector3 targetDir = target.transform.position - transform.position;

        float angle = Vector3.Angle(targetDir, Vector3.left);
        if (targetPos.y < transform.position.y) {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
        
    }
}

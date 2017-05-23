using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraAreaTriggerType { LeftBottom, LeftTop, RightBottom, RightTop };

public class CameraAreaTrigger : MonoBehaviour {

    public CameraAreaTriggerType myType;
    Vector3 playerPos;
	
	void Start () {
		
	}
	
	void Update () {
        playerPos = GameObject.Find("Main Camera").GetComponent<CameraController>().playerPos;
	}

    void OnTriggerExit2D(Collider2D c) {
        if (myType == CameraAreaTriggerType.LeftBottom) {

        } else if (myType == CameraAreaTriggerType.LeftTop) {

        } else if (myType == CameraAreaTriggerType.RightBottom) {

        } else if (myType == CameraAreaTriggerType.RightTop) {

        }
    }
}

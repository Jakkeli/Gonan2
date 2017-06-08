using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CameraAreaTriggerPos { LeftBottom, LeftTop, RightBottom, RightTop };


public enum PlayerApproachDirection { Left, Right, Up, Down }; // player approached the trigger from this direction (relative to the trigger)

public class CameraAreaTrigger : MonoBehaviour {

    //public CameraAreaTriggerPos myType;
    public CameraArea myPrimary;
    public CameraArea mySecondary;
    public PlayerApproachDirection myPAD;
    public float primaryYarea;
    public float secondaryYarea;
    GameObject playerObj;
    Vector3 playerPos;
    Vector3 myPos;
    GameObject cam;
    CameraController cc;

    public float myBossX;
    public float myBossY;


    void Start () {
        cam = GameObject.Find("Main Camera");
        playerObj = GameObject.Find("player");
        cc = cam.GetComponent<CameraController>();
	}
	
	void Update () {
        playerPos = playerObj.transform.position;
        myPos = transform.position;
	}

    void ChangeCameraArea(CameraArea ca) {
        cc.ChangeCameraArea(ca);
    }

    void OnTriggerExit2D(Collider2D c) {

        if (myPAD == PlayerApproachDirection.Left) {
            if (playerPos.x > myPos.x) {
                ChangeCameraArea(mySecondary);
                if (primaryYarea != secondaryYarea) cc.lockedY = secondaryYarea;
            } else if (playerPos.x < myPos.x) {
                ChangeCameraArea(myPrimary);
                if (primaryYarea != secondaryYarea) cc.lockedY = primaryYarea;
            }
        } else if (myPAD == PlayerApproachDirection.Right) {
            if (playerPos.x > myPos.x) {
                ChangeCameraArea(myPrimary);
                if (primaryYarea != secondaryYarea) cc.lockedY = primaryYarea;
            } else if (playerPos.x < myPos.x) {
                ChangeCameraArea(mySecondary);
                if (primaryYarea != secondaryYarea) cc.lockedY = secondaryYarea;
            }
        } else if (myPAD == PlayerApproachDirection.Down) {
            if (playerPos.y > myPos.y) {
                ChangeCameraArea(mySecondary);
                if (primaryYarea != secondaryYarea) cc.lockedY = secondaryYarea;
            } else if (playerPos.y < myPos.y) {
                ChangeCameraArea(myPrimary);
                if (primaryYarea != secondaryYarea) cc.lockedY = primaryYarea;
            }
        } else if (myPAD == PlayerApproachDirection.Up) {
            if (playerPos.y < myPos.y) {
                ChangeCameraArea(mySecondary);
                if (primaryYarea != secondaryYarea) cc.lockedY = secondaryYarea;
            } else if (playerPos.y > myPos.y) {
                ChangeCameraArea(myPrimary);
                if (primaryYarea != secondaryYarea) cc.lockedY = primaryYarea;
            }
        }

        if (mySecondary == CameraArea.Boss) {
            cc.bossX = myBossX;
            cc.bossY = myBossY;
        }

        //if (myType == CameraAreaTriggerPos.LeftBottom) {

        //} else if (myType == CameraAreaTriggerPos.LeftTop) {

        //} else if (myType == CameraAreaTriggerPos.RightBottom) {

        //} else if (myType == CameraAreaTriggerPos.RightTop) {

        //}
    }
}

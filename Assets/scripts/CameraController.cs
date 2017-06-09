using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState { Normal, CutScene, Pause, Gameover };
public enum CameraMode { Lerp, AverageSmooth, AllLocked, XOnlyLocked, Jaakko };
public enum CameraArea { Normal, Climb, Boss, ClimbLockedX };
public enum YLevel { Normal, Plus1, Minus1 };

public class CameraController : MonoBehaviour {

    public CameraState currentState;
    public CameraMode currentMode;
    public CameraArea currentArea;
    //public YLevel currentLevel;
    public GameObject player;
    public Vector3 playerPos;
    Vector3 targetPos;
    Vector3 currentVelocity = Vector3.zero;
    public float speedLookaheadFactor;
    public float smoothTime;    
    public float lerpOffset;
    public float avgSmoothOffset;
    public float cameraZ;
    public float lerpFactor = 3;
    public float goDownAdd;
    public float lockedY;
    public float bossX;
    public float bossY;
    //public float[] yLevels;

    public bool gizmosOn;

    public float cameraXLimitLeft;
    public float cameraXLimitRight;
    public bool transition;
    public float transitionSmoother;

    public void ChangeCameraArea(CameraArea ca) {
        currentArea = ca;
        transition = true;
    }

    void Update() {
        var pos = transform.position;
        targetPos = pos;
        playerPos = player.transform.position;
        targetPos = player.transform.position;
        targetPos.z = cameraZ;

        if (currentMode == CameraMode.Jaakko) {
            
            if (currentArea == CameraArea.Normal) {
                if (transition) {
                    if (pos.y != lockedY) {
                        targetPos.y = lockedY;
                        targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * transitionSmoother);
                    } else {
                        transition = false;
                        targetPos.y = lockedY;
                    }
                } else {
                    targetPos.y = lockedY;
                }
                                
            } else if (currentArea == CameraArea.Climb) {
                targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * transitionSmoother);
            } else if (currentArea == CameraArea.Boss) {
                if (transition) {
                    if (pos.y != bossY || pos.x != bossX) {
                        targetPos.y = bossY;
                        targetPos.x = bossX;
                        targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * transitionSmoother);
                    } else {
                        transition = false;
                        targetPos.y = bossY;
                        targetPos.x = bossX;
                    }
                } else {
                    targetPos.y = bossY;
                    targetPos.x = bossX;
                }
            }

            if (playerPos.x > cameraXLimitLeft && playerPos.x < cameraXLimitRight && currentArea != CameraArea.Boss) {
                targetPos.x = playerPos.x;
            } else if (currentArea != CameraArea.Boss) {
                targetPos.x = pos.x;
            }

            if (transform.position != targetPos) transform.position = targetPos;

        } else if (currentMode == CameraMode.AverageSmooth) {
            //targetPos.y += speedLookaheadFactor * player.GetComponent<Player>().smoothedVerticalSpeed + avgSmoothOffset;
            targetPos.x = playerPos.x;
            targetPos.z = cameraZ;
            transform.position = Vector3.SmoothDamp(pos, targetPos, ref currentVelocity, smoothTime);
        } else if (currentMode == CameraMode.AllLocked) {
            targetPos.y = playerPos.y + avgSmoothOffset;
            transform.position = targetPos;
        } else if (currentMode == CameraMode.XOnlyLocked) {
            targetPos.x = playerPos.x;
            targetPos.y = lockedY;
            transform.position = targetPos;
        } else if (currentMode == CameraMode.Lerp) {
            targetPos = Vector3.Lerp(pos, targetPos, Time.deltaTime * lerpFactor);
            targetPos.y += lerpOffset;
            targetPos.x = playerPos.x;
            transform.position = targetPos;
        }

    }

    void OnDrawGizmos() {
        if (gizmosOn) Gizmos.DrawWireSphere(targetPos, 1f);
    }
}

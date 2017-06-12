using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawRotator : MonoBehaviour {

    public float rotSpeed;
    float maxAngle = 0.15f;
    bool laugh;
    bool down = true;
    bool lowTurn;
    bool highTurn = true;
    int laughCount = 0;
    public int maxLaughs = 1;
    BossOne bossOne;

    private void Start() {
        bossOne = GetComponentInParent<BossOne>();
    }

    public void Laugh(int laughycles) {
        maxLaughs = laughycles;
        laugh = true;
        print("laughcalled");
    }

    void Turn() {
        if (!lowTurn) {
            down = false;
            lowTurn = true;
            highTurn = false;
            laughCount++;
            return;
        }
        if (!highTurn) {
            if (laughCount == maxLaughs) {                
                EndLaugh();
                return;
            }
            down = true;
            highTurn = true;
            lowTurn = false;
        }
    }

    void EndLaugh() {
        laugh = false;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        bossOne.laughComplete = true;
        laughCount = 0;
    }

	void Update () {
		if (laugh) {
            if (transform.rotation.z > maxAngle) Turn();
            if (transform.rotation.z < 0) Turn();

            if (down) {
                transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
            } else if (!down) {
                transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckStairs : MonoBehaviour {

    float horizontalAxis;
    float verticalAxis;

    public float speed = 1;
    public bool leftUp;

	void Start () {
		
	}
	
	void Update () {

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        float strSpeed = speed * Time.deltaTime;

        if (leftUp) {
            if (verticalAxis < 0) {
                transform.Translate(strSpeed, -strSpeed, 0);
            } else if (verticalAxis > 0) {
                transform.Translate(-strSpeed, strSpeed, 0);
            } else if (horizontalAxis < 0) {
                transform.Translate(-strSpeed, strSpeed, 0);
            } else if (horizontalAxis > 0) {
                transform.Translate(strSpeed, -strSpeed, 0);
            }
        } else if (!leftUp) {
            if (verticalAxis < 0) {
                transform.Translate(-strSpeed, -strSpeed, 0);
            } else if (verticalAxis > 0) {
                transform.Translate(strSpeed, strSpeed, 0);
            } else if (horizontalAxis < 0) {
                transform.Translate(-strSpeed, -strSpeed, 0);
            } else if (horizontalAxis > 0) {
                transform.Translate(strSpeed, strSpeed, 0);
            }
        }
    }
}

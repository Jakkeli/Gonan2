﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    public bool leftUp;
    public bool forceStair;
    public bool canDropDown;

    public Vector3 stairPos;

    void Start() {
        stairPos = transform.position;
    }

}

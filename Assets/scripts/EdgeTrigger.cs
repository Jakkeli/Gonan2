﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D c) {
        if (c.GetComponent<IReaction>() != null && c.tag == "enemy") {
            c.GetComponent<IReaction>().Activate();
        }
    }
}

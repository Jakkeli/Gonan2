using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public void StopFire()
    {
        GetComponent<ParticleSystem>().enableEmission = false;
    }
}

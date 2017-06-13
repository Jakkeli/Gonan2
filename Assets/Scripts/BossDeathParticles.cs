using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticles : MonoBehaviour {

    ParticleSystem[] partSys;

    void Start() {
        partSys = GetComponentsInChildren<ParticleSystem>();
    }

    public void DeathFX() {
        print("kutsutaanko?");
        foreach (ParticleSystem ps in partSys) {
            ps.enableEmission = true;
            ps.Emit(1);
        }
    }
}

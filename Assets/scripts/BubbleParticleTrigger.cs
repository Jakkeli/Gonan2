using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleParticleTrigger : MonoBehaviour {
    ParticleSystem ps;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();


    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        print ("osuma");
    }
}

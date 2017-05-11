using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Candle, Other };

public class Item : MonoBehaviour, IReaction {

    public ItemType myType;
    public bool seenAtStart;
    public GameObject candle;
    public GameObject heart;
    public GameObject[] partSys;
    ParticleSystem ps;
    bool hasReacted;


    void Start () {
        for (int i = 0; i < partSys.Length; i++)  {
            ps = partSys[i].GetComponent<ParticleSystem>();
            if (!seenAtStart) {
                candle.GetComponent<SpriteRenderer>().enabled = false;
                heart.GetComponent<SpriteRenderer>().enabled = false;
                ps.Pause(true);
            }
            if (ps == null) {
                print("particlesystem null!!!!!");
                return;
            }
        }
        //ps = partSys.GetComponent<ParticleSystem>();       
    }
	
    public void Activate() {
        if (!seenAtStart) {
            candle.GetComponent<SpriteRenderer>().enabled = true;
            
            
            for (int i = 0; i < partSys.Length; i++) {
                ps = partSys[i].GetComponent<ParticleSystem>();
                ps.Pause(false);
            }
        }
    }

    public void React() {
        if (!hasReacted) {
            heart.GetComponent<SpriteRenderer>().enabled = true;
            candle.GetComponent<SpriteRenderer>().enabled = false;
            
            for (int i = 0; i < partSys.Length; i++) {
                ps = partSys[i].GetComponent<ParticleSystem>();
                ps.Pause(true);
                partSys[i].SetActive(false);
            }
            heart.GetComponent<FloatDown>().DoIt();
            print("you've done it now buster!");
            hasReacted = true;
        }
        
    }

    private void Update() {

    }
}

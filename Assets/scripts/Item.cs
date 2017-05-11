using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Candle, Other };

public class Item : MonoBehaviour, IReaction {

    public ItemType myType;
    public bool seenAtStart;
    public GameObject candle;
    public GameObject heart;
    public GameObject partSys;
    ParticleSystem ps;
    bool hasReacted;


    void Start () {
        ps = partSys.GetComponent<ParticleSystem>();
        if (ps == null) {
            print("particlesystem null!!!!!");
            return;
        }
        if (!seenAtStart) {
            candle.GetComponent<SpriteRenderer>().enabled = false;
            heart.GetComponent<SpriteRenderer>().enabled = false;
            ps.Pause(true);
        }     
    }
	
    public void Activate() {
        if (!seenAtStart) {
            candle.GetComponent<SpriteRenderer>().enabled = true;
            
            ps.Pause(false);
        }
    }

    public void React() {
        if (!hasReacted) {
            heart.GetComponent<SpriteRenderer>().enabled = true;
            candle.GetComponent<SpriteRenderer>().enabled = false;
            ps.Pause(true);
            partSys.SetActive(false);
            heart.GetComponent<FloatDown>().DoIt();
            print("you've done it now buster!");
            hasReacted = true;
        }
        
    }

    private void Update() {

    }
}

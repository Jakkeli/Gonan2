using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairs : MonoBehaviour {

    public Player player;

    //public float minY;

    //void OnTriggerEnter2D(Collider2D c) {
    //    if (c.gameObject.tag == "Player") {
    //        print("collider hit");
    //        if (player.playerComesFromAbove && player.verticalAxis > 0) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        } else if (leftUp && playerComesFromLeft && player.horizontalAxis > 0) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        } else if (leftUp && !playerComesFromLeft && player.horizontalAxis < 0 && player.verticalAxis > 0) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        } else if (!leftUp && playerComesFromLeft && player.horizontalAxis > 0 && player.verticalAxis > 0) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        } else if (!leftUp && !playerComesFromLeft && player.horizontalAxis < 0) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        } else if (forceStair) {
    //            player.GetOnStair(leftUp, canDropDown);
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D c) {
    //    if (c.gameObject.tag == "Player") {
    //        player.GetOffStair();
    //    }
    //}

    private void Update() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("stair"));
        if (hit) {
            var stair = hit.collider.GetComponent<Stair>();
            transform.position = hit.point + Vector2.up;
            player.GetOnStair(stair.leftUp, stair.canDropDown);
        } else if (player.currentState == PlayerState.OnStair) {
            RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("ground"));
            if (hitGround) transform.position = hitGround.point + Vector2.up;
            player.GetOffStair();
        }


        //playerPos = GameObject.Find("player").transform.position;
        //if (playerPos.x > myPos.x) {
        //    playerComesFromLeft = false;
        //} else {
        //    playerComesFromLeft = true;
        //}

        //if (player.currentState == PlayerState.OnStair) {
        //    if (playerPos.y <= minY) player.GetOffStair();
        //}
    }
}

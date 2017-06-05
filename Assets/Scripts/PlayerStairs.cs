using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairs : MonoBehaviour {

    public Player player;
    Stair stair;

    private void Update() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 1, 0), Vector2.down, 0.1f, LayerMask.GetMask("stair"));
        if (hit) {
            stair = hit.collider.GetComponent<Stair>();            
            if (stair.leftUp && player.currentState != PlayerState.InAir) {
                if (player.horizontalAxis < 0 && player.verticalAxis > 0 && transform.position.y < stair.stairPos.y) {
                    PutPlayerOnStair(hit);
                } else if (player.horizontalAxis > 0 && transform.position.y > stair.stairPos.y) {
                    PutPlayerOnStair(hit);
                } else if (player.horizontalAxis < 0 && stair.forceStair) {
                    PutPlayerOnStair(hit);
                }
            } else if (player.currentState != PlayerState.InAir) {
                if (player.horizontalAxis > 0 && player.verticalAxis > 0 && transform.position.y < stair.stairPos.y) {
                    PutPlayerOnStair(hit);
                } else if (player.horizontalAxis < 0 && transform.position.y > stair.stairPos.y) {
                    PutPlayerOnStair(hit);
                } else if (player.horizontalAxis > 0 && stair.forceStair) {
                    PutPlayerOnStair(hit);
                }
            } else if (player.currentState == PlayerState.InAir && player.verticalAxis > 0) {
                PutPlayerOnStair(hit);
            } else if (player.currentState == PlayerState.InAir && !stair.canDropDown) {
                PutPlayerOnStair(hit);
            }
        } else if (player.currentState == PlayerState.OnStair) {
            RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("ground"));
            if (hitGround) transform.position = hitGround.point + Vector2.up;
            player.GetOffStair();
        }
    }

    void PutPlayerOnStair(RaycastHit2D hit) {
        stair = hit.collider.GetComponent<Stair>();
        transform.position = hit.point + Vector2.up;
        player.GetOnStair(stair.leftUp, stair.canDropDown);
    }
}

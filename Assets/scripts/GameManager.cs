using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Running, Paused, Start, GameOver, Menu };
public enum GameLevel { None, LevelOne, LevelTwo, LevelThree };

public class GameManager : MonoBehaviour {

    public GameState currentState;
    public GameLevel currentLevel;
    public Text levelLivesAmmo;
    string level = "LEVEL 1-1";
    string playerLives = "LIVES 3";
    string secondaryAmmo = "PLAYER 00";


    Player player;


    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        UpdateLevelLivesAmmo();
	}

    public void UpdateLevelLivesAmmo() {
        playerLives = "LIVES " + player.playerLives;
        if (player.secondaryAmmo < 10) {
            secondaryAmmo = "PLAYER 0" + player.secondaryAmmo;
        } else {
            secondaryAmmo = "PLAYER " + player.secondaryAmmo;
        }        
        //levelLivesAmmo.text = level + "\n" + playerLives + "\n" + secondaryAmmo;
    }

    public void GameOver() {
        print("Game Over N00b!1! git gud.");
        currentState = GameState.GameOver;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            GameObject.Find("player").GetComponent<Player>().StopWhip();
        }
    }
}

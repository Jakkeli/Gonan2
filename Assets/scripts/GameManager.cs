using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Running, Paused, Start, GameOver, Menu };
public enum GameLevel { None, LevelOne, LevelTwo, LevelThree };

public class GameManager : MonoBehaviour {

    public GameState currentState;
    public GameLevel currentLevel;
    public Text scoreText;
    public Text blockLivesAmmoTimerText;

    string level = "LEVEL 1-1";
    string playerLives = "LIVES 3";
    string secondaryAmmo = "PLAYER 00";
    string scoreNumbers;


    Player player;
    public int score;

    public Transform[] playerHealthBars;
    public Transform[] enemyHealthBars;

    public Sprite emptyBar;
    public Sprite FilledBar;
    public int playerHealth;
    public int enemyHealth;

    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        UpdateLevelLivesAmmo();
        scoreText.text = "SCORE    " + score + "\n" + "    ";
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

    public void UpdateScore() {

    }

    public void UpdatePlayerEnemyHealth(int ph, int eh) {
        for (int i = 0; i < ph; i++) {
            playerHealthBars[i].GetComponent<Image>().sprite = FilledBar;
        }
        for (int i = ph; i < 16; i++) {
            playerHealthBars[i].GetComponent<Image>().sprite = emptyBar;
        }
        for (int i = 0; i < eh; i++) {
            enemyHealthBars[i].GetComponent<Image>().sprite = FilledBar;
        }
        for (int i = eh; i < 16; i++) {
            enemyHealthBars[i].GetComponent<Image>().sprite = emptyBar;
        }
    }

    public void GameOver() {
        print("Game Over N00b!1! git gud.");
        currentState = GameState.GameOver;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            UpdatePlayerEnemyHealth(playerHealth, enemyHealth);
        }
    }
}

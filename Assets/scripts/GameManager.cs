using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Running, Paused, Start, GameOver, Menu };
public enum GameBlock { None, BlockOneOne, BlockOneTwo, BlockOneThree };

public class GameManager : MonoBehaviour {

    public GameState currentState;
    public GameBlock currentBlock;
    public Text scoreText;
    public Text blockLivesAmmoTimerText;
    public Text scoreTextShadow;

    string level = "BLOCK  1-1";
    string playerLives = "LIVES 3";
    string secondaryAmmo = "PLAYER 00";
    string scoreNumbers = "000000000000";
    string timeText = "450";

    Player player;
    public int score;
    int time = 450;
    float tickTime;

    public Transform[] playerHealthBars;
    public Transform[] enemyHealthBars;

    public Sprite emptyBar;
    public Sprite FilledBar;
    public int playerHealth;
    public int enemyHealth;

    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        UpdateLevelLivesAmmo();
        scoreText.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
        scoreTextShadow.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
    }

    void ChangeBlock() {
        if (currentBlock == GameBlock.None) {
            currentBlock = GameBlock.BlockOneOne;
            level = "BLOCK  1-1";
            UpdateLevelLivesAmmo();
        } else if (currentBlock == GameBlock.BlockOneOne) {
            currentBlock = GameBlock.BlockOneTwo;
            level = "BLOCK  1-2";
            UpdateLevelLivesAmmo();
        } else if (currentBlock == GameBlock.BlockOneTwo) {
            currentBlock = GameBlock.BlockOneThree;
            level = "BLOCK  1-3";
            UpdateLevelLivesAmmo();
        }        
    }

    public void UpdateLevelLivesAmmo() {
        playerLives = "H=" + player.playerLives;
        if (player.secondaryAmmo < 10) {
            secondaryAmmo = "P=0" + player.secondaryAmmo;
        } else {
            secondaryAmmo = "P=" + player.secondaryAmmo;
        }
        if (time > 99) {
            timeText = "TIME  " + time;
        } else if (time > 9) {
            timeText = "TIME  0" + time;
        } else {
            timeText = "TIME  00" + time;
        }
        blockLivesAmmoTimerText.text = level + "\n" + secondaryAmmo + " " + playerLives + "\n" + timeText;
    }

    public void UpdateScore() {
        if (score == 0) {
            scoreNumbers = "000000000000";
        } else if (score > 999999999) {
            print("u cheating bastard!!!11!!");
        } else if (score > 99999999) {
            scoreNumbers = "" + score;
        } else if (score > 9999999) {
            scoreNumbers = "0000" + score;
        } else if (score > 999999) {
            scoreNumbers = "00000" + score;
        } else if (score > 99999) {
            scoreNumbers = "000000" + score;
        } else if (score > 9999) {
            scoreNumbers = "0000000" + score;
        } else if (score > 999) {
            scoreNumbers = "00000000" + score;
        } else if (score > 99) {
            scoreNumbers = "000000000" + score;
        } else if (score > 9) {
            scoreNumbers = "0000000000" + score;
        } else {
            scoreNumbers = "00000000000" + score;
        }
        scoreText.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
        scoreTextShadow.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
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
        
        if (currentState == GameState.Running) {
            tickTime += Time.deltaTime;
            if (tickTime >= 1) {
                time--;
                tickTime -= 1;
                UpdateLevelLivesAmmo();
            }
        }

        if (time <= 0) {
            player.Death();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Running, Paused, GameOver, Menu };
public enum GameBlock { None, BlockOneOne, BlockOneTwo, BlockOneThree, BlockOneFour, BlockOneFive };

public class GameManager : MonoBehaviour {

    public GameState currentState;
    public GameBlock currentBlock;
    public GameObject currentCheckpoint;
    public Text scoreText;
    public Text blockAmmoTimerText;
    public Text scoreTextShadow;
    public Text livesText;
    public Text blockAmmoTimerTextShadow;
    public Text livesTextShadow;

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

    public GameObject menu;
    Image bgBlack;

    public bool startInMenu;
    public bool editorMode;
    bool gameStarted;

    public GameObject pauseText;
    public Text pause;
    public Text pauseShadow;
    public Transform shurikenImage;
    int deaths;
    public GameObject firstCheckpoint;
    int resetLives;
    int resetHealth;
    int resetAmmo;
    public GameObject menuLogo;

    public int checkPointIndex;
    public float currentCheckPointCameraY;
    FabricCtrl fabCtrl;

    void Awake() {
        if (startInMenu) bgBlack = GameObject.Find("bg_black").GetComponent<Image>();
        if (startInMenu) currentState = GameState.Menu;
        if (currentCheckpoint == null) currentCheckpoint = firstCheckpoint;
    }

    public void GameFinished() {
        Invoke("Finisher", 2);
        Invoke("Reset", 10);
    }

    void Reset() {
        pause.text = "PAUSED";
        pauseShadow.text = "PAUSED";
        pauseText.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void GatherResetData() {
        resetLives = player.playerLives;
        resetHealth = player.hp;
        resetAmmo = player.secondaryAmmo;
    }

    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        fabCtrl = GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>();
        fabCtrl.PlayMenuMusic();
        if (GameObject.Find("FabricCtrl").GetComponent<FabricCtrl>() == null) print("fug");
        UpdateLevelLivesAmmo();
        scoreText.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
        scoreTextShadow.text = "SCORE           " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
    }

    public void StartContinueGame() {
        if (!gameStarted) {
            ChangeBlock();
            currentState = GameState.Running;
            gameStarted = true;
        } else {
            currentState = GameState.Running;
        }
        fabCtrl.PauseMenuMusic();
        fabCtrl.PlayGameMusic();
    }

    void ChangeBlock() {
        if (currentBlock == GameBlock.None) {
            currentBlock = GameBlock.BlockOneOne;
            checkPointIndex = 1;
            level = "BLOCK  1-1";
            UpdateLevelLivesAmmo();
        } else if (currentBlock == GameBlock.BlockOneOne) {
            currentBlock = GameBlock.BlockOneTwo;
            checkPointIndex = 2;
            level = "BLOCK  1-2";
            UpdateLevelLivesAmmo();
        } else if (currentBlock == GameBlock.BlockOneTwo) {
            currentBlock = GameBlock.BlockOneThree;
            checkPointIndex = 3;
            level = "BLOCK  1-3";
            UpdateLevelLivesAmmo();
        } else if (currentBlock == GameBlock.BlockOneThree) {
            currentBlock = GameBlock.BlockOneFour;
            checkPointIndex = 4;
            level = "BLOCK  1-4";
            UpdateLevelLivesAmmo();
        }
    }

    public void UpdateLevelLivesAmmo() {
        playerLives = "P=" + player.playerLives;
        if (player.secondaryAmmo < 10) {
            secondaryAmmo = "H=0" + player.secondaryAmmo;
        } else {
            secondaryAmmo = "H=" + player.secondaryAmmo;
        }
        if (time > 99) {
            timeText = "TIME  " + time;
        } else if (time > 9) {
            timeText = "TIME  0" + time;
        } else {
            timeText = "TIME  00" + time;
        }
        blockAmmoTimerText.text = level + "\n" + secondaryAmmo + " " + "\n" + timeText;
        blockAmmoTimerTextShadow.text = level + "\n" + secondaryAmmo + " " + "\n" + timeText;
        livesText.text = playerLives;
        livesTextShadow.text = playerLives;
    }

    public void UpdateScore() {
        if (score == 0) {
            scoreNumbers = "000000000000";
        } else if (score > 999999999) {
            //print("u cheating bastard!!!11!!");
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
        scoreText.text = "SCORE                " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
        scoreTextShadow.text = "SCORE                " + scoreNumbers + "\n" + "    PLAYER\n" + "        ENEMY";
    }

    public void UpdatePlayerEnemyHealth(int ph, int eh) {
        if (ph >= 0) {
            for (int i = 0; i < ph; i++) {
                playerHealthBars[i].GetComponent<Image>().sprite = FilledBar;
            }
            for (int i = ph; i < 16; i++) {
                playerHealthBars[i].GetComponent<Image>().sprite = emptyBar;
            }
        }
        
        if (eh >= 0) {
            for (int i = 0; i < eh; i++) {
                enemyHealthBars[i].GetComponent<Image>().sprite = FilledBar;
            }
            for (int i = eh; i < 16; i++) {
                enemyHealthBars[i].GetComponent<Image>().sprite = emptyBar;
            }
        }
        
    }

    public void GameOver() {
        //print("Game Over N00b!1! git gud.");
        currentState = GameState.GameOver;
        Invoke("GitGud", 2);
        Invoke("Reset", 5);
    }

    void GitGud() {
        pauseText.SetActive(true);
        pause.text = "GIT GUD";
        pauseShadow.text = "GIT GUD";
    }

    void Finisher() {        
        pauseText.SetActive(true);
        pause.text = "GAME FINISHED!!!";
        pauseShadow.text = "GAME FINISHED!!!";
    }

    void GoToMenu() {
        pause.text = "PAUSED";
        pauseShadow.text = "PAUSED";
        pauseText.SetActive(false);
        currentState = GameState.Menu;
        bgBlack.enabled = true;
        menu.SetActive(true);
    }

    void Pause() {
        if (currentState == GameState.Paused) {
            currentState = GameState.Running;
            pauseText.SetActive(false);
        } else if (currentState == GameState.Running) {
            currentState = GameState.Paused;
            pauseText.SetActive(true);
        }
    }

    public void Respawn() {
        if (time < 350) time = 450;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && editorMode) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
        if (currentState == GameState.Running && player.currentState != PlayerState.Dead) {
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

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Esc")) {
            if (currentState != GameState.Running) return;
            fabCtrl.PauseGameMusic();
            fabCtrl.PlayMenuMusic();
            currentState = GameState.Menu;
            bgBlack.enabled = true;
            menu.SetActive(true);
            menuLogo.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause")) {
            if (currentState == GameState.GameOver || currentState == GameState.Menu) return;
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            playerHealth--;
            UpdatePlayerEnemyHealth(playerHealth, enemyHealth);            
        }
    }
}

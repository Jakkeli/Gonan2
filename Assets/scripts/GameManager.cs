﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Running, Paused, Start, GameOver, Menu };
public enum GameLevel { None, LevelOne, LevelTwo, LevelThree };

public class GameManager : MonoBehaviour {

    public GameState currentState;
    public GameLevel currentLevel;

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}

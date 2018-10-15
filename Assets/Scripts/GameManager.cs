using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // score of the player
    public int score = 0;

    // high score of the game
    public int highScore = 0;

    // current level
    public int currentLevel = 1;

    // how many levels there are
    public int highestLevel = 2;

    // HUD manager
    private HUDManager hudManager;

    // static instance of the Game Manager
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            instance.hudManager = FindObjectOfType<HUDManager>();

            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        hudManager = FindObjectOfType<HUDManager>();
    }

    public void IncreaseScore(int amount) {
        score += amount;

        // update the HUD
        if (hudManager != null) {
            hudManager.ResetHUD();
        }

        // have we surpassed our high score
        if (score > highScore) {
            highScore = score;
        }
    }

    public void ResetGame() {
        // reset our score
        score = 0;

        // update the HUD
        if (hudManager != null) {
            hudManager.ResetHUD();
        }

        // set the current level to 1
        currentLevel = 1;

        // load the level 1
        SceneManager.LoadScene("Level1");

    }

    public void IncreaseLevel() {
        // if there are more levels
        if (currentLevel < highestLevel) {
            currentLevel++;
        } else {
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}

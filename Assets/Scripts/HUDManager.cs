using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    // score text label
    public Text scoreLabel;

	// Use this for initialization
	void Start () {
        ResetHUD();
	}

    // Show player score
    public void ResetHUD() {
        scoreLabel.text = "SCORE: " + GameManager.instance.score;
    }

}

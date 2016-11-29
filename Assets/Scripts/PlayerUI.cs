using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {
    
    public Text score;

    public void Start() {
        UpdateScore();
    }

    public void Update() {
        UpdateScore();
    }

    void UpdateScore() {
        score.text = PlayerScore.GetPlayerScore().ToString();
    }
}
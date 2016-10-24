using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private int PlayerMaxHealthInitial = 100;
    private int PlayerShotDamageInitial = 50;

	// Use this for initialization
	void Start () {

        // on the end_screen scene, update texts
        if (SceneManager.GetActiveScene().name == "end_screen") {
            GameObject scoretextobj = GameObject.Find("ScoreText");
            Text scoretext = scoretextobj.GetComponent<Text>();
            scoretext.text += PlayerPrefs.GetInt("score");

            GameObject leveltextobj = GameObject.Find("ReachedLevelText");
            Text leveltext = leveltextobj.GetComponent<Text>();
            leveltext.text += PlayerPrefs.GetInt("currentlevel");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame() {
        setupGame();

		goToScene("alpha");
	}

    public void endGame() {
        setupGame();

        goToScene("main_menu");
    }

    // Reset key values in the game.
    public void setupGame() {
        // set level to 1
        PlayerPrefs.SetInt("currentlevel", 1);

        // set player max health
        PlayerPrefs.SetInt("playermaxhealth", PlayerMaxHealthInitial);

        // set player current health
        PlayerPrefs.SetInt("playerhealth", PlayerMaxHealthInitial);

        // set initial damage of player
        PlayerPrefs.SetInt("playershotdamage", PlayerShotDamageInitial);
    }

    public void goToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void increaseLevel() {
    	// get current level, increase it, and save it back
    	int curlvl = PlayerPrefs.GetInt("currentlevel");
    	// set playerprefs
    	PlayerPrefs.SetInt("currentlevel", curlvl+1);
    	goToScene("shop_menu");
    }
}

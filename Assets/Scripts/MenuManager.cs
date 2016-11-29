using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // on the end_screen scene, update texts
        if (SceneManager.GetActiveScene().name == "end_screen") {
            GameObject scoretextobj = GameObject.Find("ScoreText");
            Text scoretext = scoretextobj.GetComponent<Text>();
            scoretext.text += PlayerPrefs.GetInt("totalscore");

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
        // ensure the game is unpaused
        Time.timeScale = 1.0f;
        PlayerScore.SetupPlayerScore();

        // set the current ship
        PlayerPrefs.SetInt("selectedShip", 0); //default player ship
        // set the unlocks back
        PlayerPrefs.SetInt("podPlayer", 1); //default player ship
        PlayerPrefs.SetInt("tankPlayer", 0);
        PlayerPrefs.SetInt("assaultPlayer", 0);
    }

    public void goToScene(string scene) {
        if (LevelFour() == 4) {
            SceneManager.LoadScene("boss_scene");
        } else {
            SceneManager.LoadScene(scene);
        }
    }

    public void increaseLevel() {
    	// get current level, increase it, and save it back
    	int curlvl = PlayerPrefs.GetInt("currentlevel");
    	// set playerprefs
    	PlayerPrefs.SetInt("currentlevel", curlvl+1);
    	goToScene("shop_menu");
    }

    int LevelFour() {
        return PlayerPrefs.GetInt("currentlevel") % 4;
    }
}

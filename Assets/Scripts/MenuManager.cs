using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private int PlayerMaxHealthInitial = 100;

	// Use this for initialization
	void Start () {
		
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

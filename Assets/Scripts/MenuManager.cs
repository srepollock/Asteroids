using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame() {
		// set level to 1
		PlayerPrefs.SetInt("currentlevel", 1);

		goToScene("alpha");
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

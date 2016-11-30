using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuManager : MonoBehaviour {

    String HIGHSCORE_FILE = "/highscore.dat";

	// Use this for initialization
	void Start () {
        // on the end_screen scene, update texts
        if (SceneManager.GetActiveScene().name == "end_screen") {
            GameObject highscoretextobj = GameObject.Find("HighScoreText");
            Text highscoretext = highscoretextobj.GetComponent<Text>();
            int highscore = LoadHighScore();
            if (highscore > PlayerPrefs.GetInt("totalscore")){
                highscoretext.text += highscore;
            } else {
                highscoretext.text += PlayerPrefs.GetInt("totalscore");
                SaveHighScore();
            }

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
        if (scene != "shop_menu") {
            if (LevelFour() == 0) {
                Debug.Log("Loading boss");
                SceneManager.LoadScene("boss_fight");
            } else {
                Debug.Log("Loading: " + scene);
                SceneManager.LoadScene(scene);

            }
        } else {
            SceneManager.LoadScene(scene);
        }
    }

    public void increaseLevel() {
    	// get current level, increase it, and save it back
    	int curlvl = PlayerPrefs.GetInt("currentlevel");
        Debug.Log("CurLevel: " + curlvl);
    	// set playerprefs
        curlvl++;
    	PlayerPrefs.SetInt("currentlevel", curlvl);
        Debug.Log("CurLevel2: " + curlvl);

    	goToScene("shop_menu");
    }

    int LevelFour() {
        return PlayerPrefs.GetInt("currentlevel") % 4;
    }

    public int LoadHighScore() {
        if(File.Exists(Application.persistentDataPath + HIGHSCORE_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + HIGHSCORE_FILE, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();
            return data.highscore;
        } else
        {
            Debug.Log("Highscore file not found or doesn't exist yet.");
            return 0;
        }
    }

    public void SaveHighScore() {
        GameData data = new GameData();
        data.highscore = PlayerPrefs.GetInt("totalscore");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + HIGHSCORE_FILE, FileMode.OpenOrCreate);
        bf.Serialize(fs, data);
        fs.Close();

        Debug.Log("Saving Highscore. Score: " + data.highscore);
    }

    public void DeleteGameState()
    {
        String filename = Application.persistentDataPath + HIGHSCORE_FILE;
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        else
        {
            Debug.Log("File doesn't exist yet.");
        }
    }
}

[Serializable]
class GameData
{
    public int highscore;
};
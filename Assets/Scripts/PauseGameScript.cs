using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour {

    private bool isPaused = false;
    GameObject pauseMenuCanvas;
    private bool pausable = false;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "main_menu" || SceneManager.GetActiveScene().name == "shop_menu")
        {
            pausable = false;
        } else
        {
            pausable = true;
        }

        if (pausable)
        {
            pauseMenuCanvas = GameObject.Find("PauseMenu");
            pauseMenuCanvas.SetActive(false);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (pausable && Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                pauseGame();
            } else
            {
                unpauseGame();
            }
        }
	}

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseMenuCanvas.SetActive(true);
    }

    public void unpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseMenuCanvas.SetActive(false);
    }
}

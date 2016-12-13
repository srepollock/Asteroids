using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour {

    private bool isPaused = false;
    GameObject pauseMenuCanvas;

	// Use this for initialization
	void Start () {
        pauseMenuCanvas = GameObject.Find("PauseMenu");
        pauseMenuCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
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

using UnityEngine;
using System.Collections;

/// <summary>
/// Player score class
/// </summary>
public class PlayerScore : MonoBehaviour {

    /// <summary>
    /// Static totalscore string
    /// </summary>
    static string TOTALSCORE = "totalscore";
    /// <summary>
    /// Static currency string
    /// </summary>
    static string CURRENCY = "currency";
    /// <summary>
    /// Static levelscore string
    /// </summary>
    static string LEVELSCORE = "levelscore";

    void Start() {

    }

    void Update() {

    }

    /// <summary>
    /// Adds to total score and currency for the player.
    /// </summary>
    /// <param name="score">Score to add</param>
    public void AddScoreAndCurrency(int score) {
        int ps = PlayerPrefs.GetInt(TOTALSCORE);
        int cur = PlayerPrefs.GetInt(CURRENCY);
        PlayerPrefs.SetInt(TOTALSCORE, ps + score);
        PlayerPrefs.SetInt(CURRENCY, cur + score);
        Debug.Log("Score: " + GetPlayerScore());
        Debug.Log("Currency: " + GetPlayerCurrency());
    }
    /// <summary>
    /// Adds to total score of the player only.
    /// </summary>
    /// <param name="score">Score to add</param>
    public void AddScore(int score) {
        int ps = PlayerPrefs.GetInt(TOTALSCORE);
        PlayerPrefs.SetInt(TOTALSCORE, ps + score);
    }
    /// <summary>
    /// Spending score in shop menu.
    /// </summary>
    /// <param name="cost">Cost of item to buy</param>
    /// <returns>True if bought; False if cannot spend</returns>
    public bool SpendScore(int cost) {
        int cur = PlayerPrefs.GetInt(CURRENCY);
        if (cur < cost) {
            // not enough
            return false;
        } else {
            PlayerPrefs.SetInt(CURRENCY, cur - cost);
        }
        return true;
    }
    /// <summary>
    /// Setup the player prefs with player scores at the beginning of the game.
    /// Called in MenuManager.cs
    /// </summary>
    public static void SetupPlayerScore() {
        PlayerPrefs.SetInt(TOTALSCORE, 0);
        PlayerPrefs.SetInt(CURRENCY, 0);
        PlayerPrefs.SetInt(LEVELSCORE, 0);
    }
    /// <summary>
    /// Gets player score 
    /// </summary>
    /// <returns>Player total score</returns>
    public static int GetPlayerScore() {
        return PlayerPrefs.GetInt(TOTALSCORE);
    }
    /// <summary>
    /// Gets player currency
    /// </summary>
    /// <returns>Player currency</returns>
    public static int GetPlayerCurrency() {
        return PlayerPrefs.GetInt(CURRENCY);
    }
    /// <summary>
    /// Called on start of level to set the current level score.
    /// </summary>
    public static void StartLevel() {
        PlayerPrefs.SetInt(LEVELSCORE, 0);
    }
    /// <summary>
    /// Called at the end of the level to add a bonus score for completion.
    /// Time must be in seconds.
    /// </summary>
    public void EndLevel() {
        float time = Time.timeSinceLevelLoad;
        int levelScore = PlayerPrefs.GetInt(LEVELSCORE);
        int ls = (int)(levelScore * (100 / (100 + time)));
        AddScore(ls + levelScore);
    }
}
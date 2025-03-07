using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import for TextMeshPro

public class MainUIHandler : MonoBehaviour
{
    public TMP_Text playerNameText; // Displays Player: Name
    public Text scoreText; // Displays current score
    public Text bestScoreText; // Displays Best: Name : Score

    private int score = 0;
    private int highScore = 0;
    private string highScorePlayer = "None";

    private void Start()
    {
        // Load player name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        playerNameText.text = playerName;

        // Load high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScorePlayer = PlayerPrefs.GetString("HighScorePlayer", "None");

        // Display best score
        bestScoreText.text = $"Best: {highScorePlayer} : {highScore}";
        UpdateScoreUI();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    
}

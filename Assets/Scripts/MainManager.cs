using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText; // UI Text to display Best Score
    public TMP_Text PlayerNameText; // Use TMP_Text instead of Text
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points = 0;
    private bool m_GameOver = false;
    private int highScore = 0;
    private string highScorePlayer = "None";

    void Start()
    {
        // Load player name from PlayerPrefs
        string currentPlayer = PlayerPrefs.GetString("PlayerName", "Unknown");
        PlayerNameText.text = "Player: " + currentPlayer;

        // Load high score and best player
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScorePlayer = PlayerPrefs.GetString("HighScorePlayer", "None");

        // Display best score in the format: Best: Name : Score
        HighScoreText.text = $"Best: {highScorePlayer} : {highScore}";

        // Brick Spawning Logic
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Check if new high score is achieved
        if (m_Points > highScore)
        {
            highScore = m_Points;
            highScorePlayer = PlayerPrefs.GetString("PlayerName", "Unknown");

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.SetString("HighScorePlayer", highScorePlayer);
            PlayerPrefs.Save();
        }

        // Update best score format: Best: Name : Score
        HighScoreText.text = $"Best: {highScorePlayer} : {highScore}";
    }
}

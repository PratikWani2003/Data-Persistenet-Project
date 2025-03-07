using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput; // Player name input field
    public TMP_Text bestScoreText; // UI Text to display best score

    private void Start()
    {
        // Load and display the best score from previous sessions
        string bestPlayer = PlayerPrefs.GetString("HighScorePlayer", "None");
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreText.text = $"Best: {bestPlayer} : {bestScore}";
    }

    public void StartGame()
    {
        string playerName = playerNameInput.text.Trim(); // Get and trim input

        // If no name is entered, use a default name
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player"; 
        }

        PlayerPrefs.SetString("PlayerName", playerName); // Save name for session
        PlayerPrefs.Save(); // Store data

        SceneManager.LoadScene("Scenes/Main"); // Load the Main Scene
    }
   public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop Play Mode in Unity Editor
        #else
            Application.Quit(); // Quit the game in a built version
        #endif
    }

}

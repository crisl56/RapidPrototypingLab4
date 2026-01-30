using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    // TMP reference
    public TextMeshProUGUI scoreText;

    // Scriptable Object reference
    public Score scoreData;

    // Singleton reference
    public static ScoreUI Instance { get; private set; }

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // Add score with parameter
    public void AddScore(int amount)
    {
        if (scoreData != null)
        {
            scoreData.SetScore(scoreData.GetScore() + amount);
            UpdateScoreUI();
        }
    }

    // Add score with default value of 100
    public void AddScore()
    {
        AddScore(100);
    }

    // Remove score with parameter
    public void RemoveScore(int amount)
    {
        if (scoreData != null)
        {
            scoreData.SetScore(scoreData.GetScore() - amount);
            UpdateScoreUI();
        }
    }

    // Remove score with default value of 100
    public void RemoveScore()
    {
        RemoveScore(100);
    }

    // Update the UI text
    private void UpdateScoreUI()
    {
        if (scoreText != null && scoreData != null)
        {
            scoreText.text = "Score: " + scoreData.GetScore().ToString("F0");
        }
    }
}
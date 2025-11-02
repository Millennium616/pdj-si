using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int targetScore = 20;
    public int lives = 3;
    public TextMeshPro scoreText;
    public TextMeshPro livesText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        if (score >= targetScore)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void ChangeLives(int amount)
    {
        lives += amount;
        UpdateUI();
        if (lives <= 0)
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    public void UpdateUI() // <-- TORNE PÚBLICO
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    public void ResetGame()
    {
        score = 0;
        lives = 3;
        UpdateUI();
        var player = FindAnyObjectByType<Player>();
        if (player != null) player.ResetPlayer();
    }
}
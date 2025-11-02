using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pontuação")]
    public int score = 0;
    public int targetScore = 20;

    [Header("Vidas")]
    public int lives = 3;

    [Header("Referências UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        if (score >= targetScore)
        {
            if (Application.CanStreamedLevelBeLoaded("Victory"))
                SceneManager.LoadScene("Victory");
            else
                Debug.LogWarning("Cena 'Victory' não encontrada em Build Settings!");
        }
    }

    public void ChangeLives(int amount)
    {
        lives += amount;
        UpdateUI();
        if (lives <= 0)
        {
            if (Application.CanStreamedLevelBeLoaded("Defeat"))
                SceneManager.LoadScene("Defeat");
            else
                Debug.LogWarning("Cena 'Defeat' não encontrada em Build Settings!");
        }
    }

    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void ResetGame()
    {
        score = 0;
        lives = 3;
        UpdateUI();

        var player = FindFirstObjectByType<Player>();
        if (player != null) player.ResetPlayer();
    }
}
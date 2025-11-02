using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Status do Jogo")]
    public int score = 0;
    public int lives = 3;
    public bool isGameOver = false;

    [Header("UI")]
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

        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    private void Start()
    {
        UpdateUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            isGameOver = false;
        }
        
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
        Debug.Log("Score resetado para 0.");
    }

    public void ResetLives()
    {
        lives = 3;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        if (isGameOver) return;

        lives -= amount;
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! Pontuação final: " + score);
        SceneManager.LoadScene("Defeat");
    }

    public void UpdateUI()
    {
        
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score.ToString();
        }
        
        if (livesText != null)
        {
            livesText.text = "LIVES: " + lives.ToString();
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
public static GameManager Instance;


    public int score = 0;
    public int targetScore = 20;
    public int scoreToWin = 20;



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


    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }



    public void ResetGame()
    {
        score = 0;
        lives = 3;
    }

}
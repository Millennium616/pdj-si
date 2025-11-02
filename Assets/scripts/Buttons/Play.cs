using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ClickBegin()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore(); 
            GameManager.Instance.ResetLives(); 
        }
        SceneManager.LoadScene("Game");
    }
}
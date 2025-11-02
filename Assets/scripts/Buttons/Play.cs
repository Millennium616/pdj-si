using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    public void ClickBegin(){
        SceneManager.LoadScene("Game");
    }
}

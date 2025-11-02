using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void ClickBack(){
        SceneManager.LoadScene("Home");
    }
}

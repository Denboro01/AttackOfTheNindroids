using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}

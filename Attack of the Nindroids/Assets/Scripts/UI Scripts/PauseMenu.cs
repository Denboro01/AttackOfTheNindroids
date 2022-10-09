using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool _isGamePaused = false;
    [SerializeField]
    private GameObject _pauseScreenUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                // If the player presses the escape key and the game is already paused, resume the game
                Resume();
            }
            else
            {
                // If the player presses the escape key and the game is currently playing, pause the game
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        _isGamePaused = false;
    }

    void Pause()
    {
        _pauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        _isGamePaused = true;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}

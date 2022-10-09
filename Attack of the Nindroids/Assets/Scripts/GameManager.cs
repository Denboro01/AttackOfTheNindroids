using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public float _timeToComplete;

    public static Action<float> updateTimeUI;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            // Delete this instance of the game manager if another game manager already exists
            Destroy(this.gameObject);
        }
        else
        {
            // If this is the only game manager in the active scene, make this one nondestructible
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // Updates the timer and sends a unity action to the UI
            _timeToComplete += Time.deltaTime;
            updateTimeUI?.Invoke(_timeToComplete);
        }
    }

    private void OnEnable()
    {
        // Subscribe to unity actions
        PlayerMovement.playerDead += EndGame;
        PlayerMovement.playerWin += FinishGame;
        SceneManager.sceneLoaded += ResetCount;
    }

    private void OnDisable()
    {
        PlayerMovement.playerDead -= EndGame;
        PlayerMovement.playerWin -= FinishGame;
        SceneManager.sceneLoaded -= ResetCount;
    }

    void ResetCount(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            // Reset timer
            _timeToComplete = 0;
            Time.timeScale = 1;
        }
    }

    void EndGame()
    {
        // Load the game over screen
        SceneManager.LoadScene(2);
    }

    void FinishGame()
    {
        SceneManager.LoadScene(3);
    }
}

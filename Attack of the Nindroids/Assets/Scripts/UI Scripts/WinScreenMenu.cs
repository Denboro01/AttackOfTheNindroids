using UnityEngine;
using UnityEngine.UI;

public class WinScreenMenu : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private GameObject _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager managerScript = _gameManager.GetComponent<GameManager>();
        _scoreText.text = managerScript._timeToComplete.ToString("0");
    }
}

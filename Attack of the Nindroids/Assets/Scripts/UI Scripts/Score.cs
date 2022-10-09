using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text _timeText;

    private void OnEnable()
    {
        GameManager.updateTimeUI += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.updateTimeUI -= UpdateUI;
    }

    void UpdateUI(float time)
    {
        _timeText.text = time.ToString("0");
    }
}

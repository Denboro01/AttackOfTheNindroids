using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    [SerializeField]
    private Text _speedText;

    private void OnEnable()
    {
        PlayerMovement.playerSpeed += UpdateSpeed;
    }

    private void OnDisable()
    {
        PlayerMovement.playerSpeed -= UpdateSpeed;
    }

    void UpdateSpeed(float speed)
    {
        _speedText.text = speed.ToString();
    }
}

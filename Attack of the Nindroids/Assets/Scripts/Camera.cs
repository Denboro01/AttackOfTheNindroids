using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 cameraOffset;

    // Update is called once per frame
    void Update()
    {
        // Update the camera's position by referring to the player's position and adding the offset
        transform.position = player.transform.position + cameraOffset;
    }
}

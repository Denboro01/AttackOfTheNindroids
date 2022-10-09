using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiates projectile at spawn point with a set speed
            GameObject projectile = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetSpeed(GetComponent<Rigidbody>().velocity.x + 20f);
        }
    }
}

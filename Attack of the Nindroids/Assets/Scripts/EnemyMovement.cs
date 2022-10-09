using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _movementRange;
    private Transform _playerTransform;

    [Header("Animation Variables")]
    [SerializeField]
    private Animator _enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Get all reference variables
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance from player to instance of enemy
        float distFromPlayer = Vector3.Distance(_playerTransform.position, transform.position);

        if (distFromPlayer <= _movementRange)
        {
            // If the player is within the movement range of the enemy, let the enemy move
            _enemyAnimator.SetBool("canRun", true);
            _rb.velocity = Vector3.left * _speed;
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos to help visualize the range
        Gizmos.DrawWireSphere(transform.position, _movementRange);
    }
}

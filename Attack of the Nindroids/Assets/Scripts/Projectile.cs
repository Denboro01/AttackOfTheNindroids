using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private float _timeOfSpawn;

    public void SetSpeed(float speed)
    {
        _rb = GetComponent<Rigidbody>();
        _timeOfSpawn = Time.time;
        _speed = speed;
        _rb.velocity = Vector3.right * _speed;
    }

    private void Update()
    {
        if (Time.time - _timeOfSpawn >= _lifeTime)
        {
            // Destroys projectile after an amount of time passes
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // If the projectile collides with an enemy, destroy the enemy and the projectile
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

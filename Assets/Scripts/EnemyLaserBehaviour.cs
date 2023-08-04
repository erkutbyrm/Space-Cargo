using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _laserHitExplosionPrefab;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _bulletVelocity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody.velocity = transform.right * _bulletVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.transform.CompareTag(Constants.TAG_ASTEROID) ||
            collision.transform.CompareTag(Constants.TAG_SPACESHIP))
            )
        {
            return;
        }

        if (collision.CompareTag(Constants.TAG_SPACESHIP))
        {
            collision.GetComponent<PlayerShipHealthBehavior>().TakeDamage(1);
        }

        GameObject hitExplosion = Instantiate(_laserHitExplosionPrefab, transform.position, transform.rotation);
        Animator animator = hitExplosion.GetComponent<Animator>();
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(hitExplosion, delay);
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject _laserHitExplosionPrefab;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _bulletVelocity = 20f;
    private bool _isReturned = false;

    public void OnSpawnFromPool()
    {
        _isReturned = false;
        _rigidBody.velocity = transform.right * _bulletVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            GameObject hitExplosion = Instantiate(_laserHitExplosionPrefab, transform.position, transform.rotation);
            GameObject asteroid = collision.gameObject;
            AsteroidBehaviour asteroidCollisionSystem = asteroid.GetComponent<AsteroidBehaviour>();
            asteroidCollisionSystem.ExplodeAsteroid();
            Animator animator = hitExplosion.GetComponent<Animator>();
            float delay = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(hitExplosion, delay);
            if(!_isReturned)
            {
                ReturnToQueue();
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(!_isReturned)
        {
            ReturnToQueue();
        }
    }

    private void ReturnToQueue()
    {
        _isReturned = true;
        gameObject.SetActive(false);
        ObjectPooler.Instance.ReturnBackToQueue(Constants.TAG_LASER, gameObject);
    }
}

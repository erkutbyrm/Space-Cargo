using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour, IPooledObject
{
    public GameObject laserHitExplosionPrefab;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float bulletVelocity = 20f;

    // Start is called before the first frame update
    public void onSpawFromPool()
    {
        rigidBody.velocity = transform.right * bulletVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            GameObject hitExplosion = Instantiate(laserHitExplosionPrefab, transform.position, transform.rotation);
            GameObject asteroid = collision.gameObject;
            AsteroidCollisionSystem asteroidCollisionSystem = asteroid.GetComponent<AsteroidCollisionSystem>();
            asteroidCollisionSystem.ExplodeAsteroid();
            Destroy(hitExplosion, 0.25f);
            ReturnToQueue();
        }
    }

    private void OnBecameInvisible()
    {
        ReturnToQueue();
    }

    private void ReturnToQueue()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.ReturnBackToQueue(Constants.TAG_LASER, gameObject);
    }
}

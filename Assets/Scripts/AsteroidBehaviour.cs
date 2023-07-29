using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _gemPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Constants.TAG_LASER))
        {
            GameObject gem = Instantiate(_gemPrefab, transform.position, transform.rotation);
            ExplodeAsteroid();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_SPACESHIP))
        { 
            ExplodeAsteroid();
        }
    }

    public void ExplodeAsteroid()
    {
        GameObject explosion = GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float delay = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
        Destroy(gameObject);
    }
}

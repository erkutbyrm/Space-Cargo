using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionSystem : MonoBehaviour
{
    //general asteroid class
    //update tags
    public GameObject explosionPrefab;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_SPACESHIP))
        { 
            ExplodeAsteroid();
        }
    }

    public void ExplodeAsteroid()
    {
        
        GameObject explosion = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float delay = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
        Destroy(gameObject);
    }
}

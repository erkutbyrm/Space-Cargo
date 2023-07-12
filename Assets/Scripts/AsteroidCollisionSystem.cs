using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionSystem : MonoBehaviour
{
    public GameObject explosionPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpaceShip"))
        { 
            GameObject explosion = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            gameObject.SetActive(false);
        }
    }
}

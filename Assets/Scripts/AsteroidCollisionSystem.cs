using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionSystem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpaceShip"))
        { gameObject.SetActive(false); }
    }
}

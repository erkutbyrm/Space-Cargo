using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    public void Die()
    {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.DisplayEndGame();
        GameObject explosion = GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float delay = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
        Destroy(gameObject);
    }
}

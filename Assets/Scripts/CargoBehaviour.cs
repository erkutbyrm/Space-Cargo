using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBehaviour : MonoBehaviour
{
    public static event Action OnCargoCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Constants.TAG_SPACESHIP))
        {
            OnCargoCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}

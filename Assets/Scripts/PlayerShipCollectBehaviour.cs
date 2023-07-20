using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipCollectBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect();
        }
    }
}

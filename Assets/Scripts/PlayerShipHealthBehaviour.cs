using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealthBehavior : ShipHealthBehaviour
{
    private HealthUIBehaviour healthUIBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        healthUIBehaviour = GameObject.FindObjectOfType<HealthUIBehaviour>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Asteroid") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            DecreaseHealth(1);
        }

        healthUIBehaviour.UpdateHealth();
    }
}

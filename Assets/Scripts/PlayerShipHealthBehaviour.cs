using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealthBehavior : ShipHealthBehaviour
{
    private HealthUIBehaviour _healthUIBehaviour;

    void Start()
    {
        _healthUIBehaviour = GameObject.FindObjectOfType<HealthUIBehaviour>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag(Constants.TAG_ASTEROID) ||
            collision.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            DecreaseHealth(Constants.DAMAGE_ASTEROID);
        }

        _healthUIBehaviour.UpdateHealth();
    }
}

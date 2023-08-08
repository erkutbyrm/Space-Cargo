using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserBehaviour : LaserBehaviour
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.transform.CompareTag(Constants.TAG_ASTEROID) ||
            collision.transform.CompareTag(Constants.TAG_ENEMY) ||
            collision.transform.CompareTag(Constants.TAG_SPACESTATION))
            )
        {
            return;
        }

        if (collision.transform.CompareTag(Constants.TAG_ENEMY))
        {
            collision.gameObject.GetComponent<EnemyShipBehaviour>().TakeDamage(LaserDamage);
        }

        base.OnTriggerEnter2D(collision);
    }

    public void SetPlayerLaserDamage(int damage)
    {
        LaserDamage = damage;
    }
}

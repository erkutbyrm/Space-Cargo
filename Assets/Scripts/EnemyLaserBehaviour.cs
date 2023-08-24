using UnityEngine;

public class EnemyLaserBehaviour : LaserBehaviour
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.transform.CompareTag(Constants.TAG_ASTEROID) ||
            collision.transform.CompareTag(Constants.TAG_SPACESHIP) ||
            collision.transform.CompareTag(Constants.TAG_SPACESTATION))
            )
        {
            return;
        }

        if (collision.CompareTag(Constants.TAG_SPACESHIP))
        {
            collision.GetComponent<PlayerShipBehaviour>().TakeDamage(LaserDamage);
        }

        base.OnTriggerEnter2D(collision);
    }
}

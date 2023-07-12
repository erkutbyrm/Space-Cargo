using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public GameObject laserHitExplosionPrefab;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float bulletVelocity = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.right * bulletVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.transform.CompareTag("Asteroid"))
        {
            GameObject hitExplosion = Instantiate(laserHitExplosionPrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(hitExplosion, 0.25f);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

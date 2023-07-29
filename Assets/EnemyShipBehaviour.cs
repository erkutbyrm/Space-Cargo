using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehaviour : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _triggerField;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag(Constants.TAG_SPACESHIP))
        {

        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag(Constants.TAG_SPACESHIP))
        {
            GameObject spaceShip = collision.gameObject;
            FaceToSpaceShip(spaceShip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collids");
    }

    private void FaceToSpaceShip(GameObject spaceShip)
    {
        Vector2 direction = spaceShip.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }
}

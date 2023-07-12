using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBehavior : MonoBehaviour
{
    protected int currentHealth = 5;
    protected int maxHealth = 5;
    [SerializeField] private GameObject explosionPrefab;
   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //to be overriden
    }

    protected void DecreaseHealth(int damage)
    {
        currentHealth -= damage;
    }

    protected void IncreaseHealth(int damage)
    {
        currentHealth += damage;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected void Die()
    {
        GameObject explosion = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1f);
        
    }
}

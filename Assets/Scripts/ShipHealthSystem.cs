using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthSystem : MonoBehaviour
{
    public int currentHealth = 5;
    public int maxHealth = 5;
    public HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.CompareTag("Asteroid") ||
            collision.gameObject.CompareTag("Enemy") ) 
        { 
            currentHealth -= 1;
            healthController.updateHealth();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBehavior : MonoBehaviour
{
    public int currentHealth = 5;
    public int maxHealth = 5;
   

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
}

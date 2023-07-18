using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBehaviour : MonoBehaviour
{
    public int currentHealth { get; protected set; } //pascalcase property
    //public int currentHealth2 { get { return currentHealth2*2; } protected set { currentHealth2 = value; } }
    public readonly int MAX_HEALTH = 5;
    [SerializeField] protected ShipBehaviour shipBehaviour;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = MAX_HEALTH;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //to be overriden
    }

    protected void DecreaseHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            shipBehaviour.Die();
        }
    }

    protected void IncreaseHealth(int amount)
    {
        currentHealth += amount;
    }
}

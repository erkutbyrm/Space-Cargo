using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBehaviour : MonoBehaviour
{
    //[field: SerializeField]
    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; } = 5;
    [SerializeField] protected ShipBehaviour _shipBehaviour;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected void DecreaseHealth(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            _shipBehaviour.Die();
        }
    }

    protected void IncreaseHealth(int amount)
    {
        CurrentHealth += amount;
    }
}

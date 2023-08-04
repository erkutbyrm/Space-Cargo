using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    public int CurrentHealth { get; protected set; }
    public abstract int MaxHealth { get; protected set; }
    public abstract int CollisionDamage { get; protected set; }

    public virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }


    public virtual void TakeDamage(int damage)
    {
        DecreaseHealth(damage);
    }
    protected void DecreaseHealth(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected void IncreaseHealth(int amount)
    {
        CurrentHealth += amount;
    }

    public virtual void Die()
    {
        GameObject explosion = GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        float delay = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
        Destroy(gameObject);
    }
}

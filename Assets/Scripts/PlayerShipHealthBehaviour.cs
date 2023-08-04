using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealthBehavior : ShipHealthBehaviour
{
    //public static event Action<float> ShieldEnabled;

    private GameUIController _gameUIController;
    //private bool _isShieldOn = false;
    

    //private void OnEnable()
    //{
    //    PowerUpShieldBehaviour.OnPowerUpShieldCollected += EnableShield;
    //}

    void Start()
    {
        _gameUIController = GameObject.FindObjectOfType<GameUIController>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        //if(_isShieldOn)
        //{
        //    _isShieldOn = false;
        //    return;
        //}

        if (collision.gameObject.CompareTag(Constants.TAG_ASTEROID) ||
            collision.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            DecreaseHealth(1);
        }

        _gameUIController.UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        DecreaseHealth(damage);
        _gameUIController.UpdateHealth();
    }

    //    private void EnableShield(float shield, float duration)
    //    {
    //        _isShieldOn = true;
    //        ShieldEnabled?.Invoke(duration);
    //    }
}

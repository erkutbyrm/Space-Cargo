using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShieldBehaviour : MonoBehaviour, ICollectable
{
    [SerializeField] private int _shieldAmount = 1;
    [SerializeField] private float _shieldDuration = 5;

    public static event Action<int,float> OnPowerUpShieldCollected;
    public void Collect()
    {
        OnPowerUpShieldCollected?.Invoke(_shieldAmount, _shieldDuration);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShieldBehaviour : MonoBehaviour, ICollectable
{
    [SerializeField] private float _shieldAmount = 1f;
    [SerializeField] private float _shieldDuration = 5f;

    public static event Action<float,float> OnPowerUpShieldCollected;
    public void Collect()
    {
        OnPowerUpShieldCollected?.Invoke(_shieldAmount, _shieldDuration);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBehaviour : MonoBehaviour, ICollectable
{
    public static event Action<float, float> OnPowerUpSpeedCollected;

    [SerializeField] private float _powerUpSpeed = 5f;
    [SerializeField] private float _powerUpDuration = 5f;
    public void Collect()
    {
        OnPowerUpSpeedCollected?.Invoke(_powerUpSpeed, _powerUpDuration);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAmmoBehaviour : MonoBehaviour, ICollectable
{
    [SerializeField] private int _ammoIncreaseAmount = 5;

    public static event Action<int> OnAmmoCollected;
    public void Collect()
    {
        OnAmmoCollected?.Invoke(_ammoIncreaseAmount);
        Destroy(gameObject);
    }
}

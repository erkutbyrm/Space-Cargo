using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBehaviour : MonoBehaviour, ICollectable
{
    public static event Action OnCargoCollected;
    public void Collect()
    {
        OnCargoCollected?.Invoke();
        Destroy(gameObject);
    }
}

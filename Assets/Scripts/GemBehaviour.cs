using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehaviour : MonoBehaviour, ICollectable
{
    public static event Action OnGemCollected;
    public void Collect()
    {
        AudioManager.Instance.PlaySoundWithName("PickupGemSound");
        OnGemCollected?.Invoke();
        Destroy(gameObject);
    }
}

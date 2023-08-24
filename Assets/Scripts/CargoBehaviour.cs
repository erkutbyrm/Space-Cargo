using System;
using UnityEngine;

public class CargoBehaviour : MonoBehaviour, ICollectable
{
    public static event Action OnCargoCollected;
    public void Collect()
    {
        AudioManager.Instance.PlaySoundWithName(Constants.SOUND_PICKUP_CARGO);
        OnCargoCollected?.Invoke();
        Destroy(gameObject);
    }
}

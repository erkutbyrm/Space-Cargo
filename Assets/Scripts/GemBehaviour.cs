using System;
using UnityEngine;

public class GemBehaviour : MonoBehaviour, ICollectable
{
    public static event Action OnGemCollected;
    public void Collect()
    {
        AudioManager.Instance.PlaySoundWithName(Constants.SOUND_PICKUP_GEM);
        OnGemCollected?.Invoke();
        Destroy(gameObject);
    }
}

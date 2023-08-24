using System;
using UnityEngine;

public class PowerUpSpeedBehaviour : MonoBehaviour, ICollectable
{
    public static event Action<float, float> OnPowerUpSpeedCollected;

    [SerializeField] private float _powerUpSpeed = 5f;
    [SerializeField] private float _powerUpDuration = 5f;
    public void Collect()
    {
        AudioManager.Instance.PlaySoundWithName(Constants.SOUND_POWERUP_SPEED);
        OnPowerUpSpeedCollected?.Invoke(_powerUpSpeed, _powerUpDuration);
        Destroy(gameObject);
    }
}

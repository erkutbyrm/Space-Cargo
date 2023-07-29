using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public static event Action<float> OnSpeedBoost;

    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _accelPercentage = 2f;
    [SerializeField] private float _decelPercentage = 2f;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Animator _animator;
    private Vector2 _movement;
    private Vector3 mousePos;

    private float _remainingBoostInSeconds = 0;
    private bool _isBoostActive = false;

    private void Update()
    {
        _movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
            );
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
        _rigidBody.SetRotation(Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime));

        if (_movement.y > 0 && _currentSpeed < _maxSpeed)
        {
            _animator.SetBool("IsThrustStarted", true);
            _currentSpeed += _accelPercentage * Time.fixedDeltaTime;
        }
        else if (_movement.y < 0)
        {
            _animator.SetBool("IsThrustStarted", false);
            if (_currentSpeed > 0)
            {
                _currentSpeed -= _decelPercentage * Time.fixedDeltaTime;
            }
            else
            {
                _currentSpeed = 0;
            }
        }
        else
        {
            _animator.SetBool("IsThrustStarted", false);
            if (_currentSpeed <= 0)
            {
                _currentSpeed = 0;
            }
        }
        
        _rigidBody.MovePosition(_rigidBody.position + (Vector2)transform.right * _currentSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        PowerUpSpeedBehaviour.OnPowerUpSpeedCollected += BoostSpeed;
    }

    private void OnDisable()
    {
        PowerUpSpeedBehaviour.OnPowerUpSpeedCollected -= BoostSpeed;
    }

    private void BoostSpeed(float speed, float duration)
    {
        OnSpeedBoost?.Invoke(duration);
        if(_isBoostActive)
        {
            _remainingBoostInSeconds = duration;
            return;
        }
        StartCoroutine( BoostSpeedCoroutine(speed,duration) );
    }

    
    private IEnumerator BoostSpeedCoroutine(float speed, float duration)
    {
        _isBoostActive = true;
        _remainingBoostInSeconds = duration;
        float prevMaxSpeed = _maxSpeed;
        _maxSpeed += speed;
        _currentSpeed = _maxSpeed;
        for(; _remainingBoostInSeconds > 0; _remainingBoostInSeconds--)
        {
            yield return new WaitForSeconds(1f);
        }
        _maxSpeed = prevMaxSpeed;
        if(_currentSpeed  >= _maxSpeed)
        {
            _currentSpeed = _maxSpeed;
        }
        _isBoostActive = false;
    }
}

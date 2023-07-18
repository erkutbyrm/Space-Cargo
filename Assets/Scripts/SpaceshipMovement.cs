using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _accelPercentage = 2f;
    [SerializeField] private float _decelPercentage = 1f;
    [SerializeField] private Rigidbody2D _rigidBody;
    private Vector2 _movement;

    private void FixedUpdate()
    {
        _movement.y = Input.GetAxisRaw("Vertical");
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
            );
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
        _rigidBody.SetRotation(Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime));

        if (_movement.y > 0 && _currentSpeed < _maxSpeed)
        {
            _currentSpeed += _accelPercentage * Time.fixedDeltaTime;
        }
        else if (_movement.y < 0)
        {
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
            if (_currentSpeed <= 0)
            {
                _currentSpeed = 0;
            }
        }

        _rigidBody.MovePosition(_rigidBody.position + (Vector2)transform.right * _currentSpeed * Time.fixedDeltaTime);
    }
}

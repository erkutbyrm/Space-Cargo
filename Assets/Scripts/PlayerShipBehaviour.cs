using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBehaviour : ShipBehaviour
{
    public static event Action<float> OnSpeedBoost;

    [Header("Movement")]
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _accelPercentage = 2f;
    [SerializeField] private float _decelPercentage = 2f;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private List<Animator> _animators;
    private Vector2 _movement;
    private Vector3 mousePos;
    private float _remainingBoostInSeconds = 0;
    private bool _isBoostActive = false;

    [Header("Weapon")]
    [SerializeField] private Transform _laserStartPoint;
    [SerializeField] private GameObject _laserPrefab;

    //HEALTH & DAMAGE
    public override int MaxHealth { get; protected set; } = 5;
    public override int CollisionDamage { get; protected set; } = 1;
    private GameUIController _gameUIController;

    public override void Start()
    {
        PlayerDataInitialization();
        GameObject.FindObjectOfType<CameraBehaviour>().IntroduceShipToCamera(this.gameObject);
        _gameUIController = GameObject.FindObjectOfType<GameUIController>();
        _gameUIController.StartGameUIController();
        base.Start();
    }

    // HEALTH
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _gameUIController.UpdateHealth();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            collision.gameObject.GetComponent<EnemyShipBehaviour>().TakeDamage(CollisionDamage);
        }
    }

    public override void Die()
    {
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
        levelController.DisplayEndGame();
        base.Die();
    }

    // MOVEMENT
    private void Update()
    {
        if (LevelController.IsPaused == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectPooler.Instance.SpawnObjectFromPool(Constants.TAG_PLAYER_LASER, _laserStartPoint.position, _laserStartPoint.rotation);
        }

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

        bool animBool = false;
        if (_movement.y > 0 && _currentSpeed < _maxSpeed)
        {
            animBool = true;
            _currentSpeed += _accelPercentage * Time.fixedDeltaTime;
        }
        else if (_movement.y < 0)
        {
            animBool = false;
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
            animBool = false;
            if (_currentSpeed <= 0)
            {
                _currentSpeed = 0;
            }
        }

        foreach ( Animator anim in _animators )
        {
            anim.SetBool("IsThrustStarted", animBool);
        }

        _rigidBody.MovePosition(_rigidBody.position + (Vector2)transform.right * _currentSpeed * Time.fixedDeltaTime);
    }

    // BOOST SPEED
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
        if (_isBoostActive)
        {
            _remainingBoostInSeconds = duration;
            _currentSpeed = _maxSpeed;
            return;
        }
        StartCoroutine(BoostSpeedCoroutine(speed, duration));
    }

    private IEnumerator BoostSpeedCoroutine(float speed, float duration)
    {
        _isBoostActive = true;
        _remainingBoostInSeconds = duration;
        float prevMaxSpeed = _maxSpeed;
        _maxSpeed += speed;
        _currentSpeed = _maxSpeed;
        for (; _remainingBoostInSeconds > 0; _remainingBoostInSeconds--)
        {
            yield return new WaitForSeconds(1f);
        }
        _maxSpeed = prevMaxSpeed;
        if (_currentSpeed >= _maxSpeed)
        {
            _currentSpeed = _maxSpeed;
        }
        _isBoostActive = false;
    }

    // COLLECTABLE

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect();
        }
    }

    //PLAYER DATA INITIALIZE

    private void PlayerDataInitialization()
    {
        SpaceShipScriptableObject _currentShipType = DataController.Instance.PlayerData.CurrentShipData.SpaceShipScriptableObject;

        _laserPrefab.GetComponent<PlayerLaserBehaviour>().SetPlayerLaserDamage(_currentShipType.LaserDamage);
        _maxSpeed = _currentShipType.BaseSpeedLimit + DataController.Instance.PlayerData.CurrentShipData.CurrentSpeedUpgrade;
        MaxHealth = _currentShipType.BaseHealth + DataController.Instance.PlayerData.CurrentShipData.CurrentHealthUpgrade;
    }
}

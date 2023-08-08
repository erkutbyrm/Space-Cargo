using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBehaviour : ShipBehaviour
{
    [SerializeField] List<SpaceShipScriptableObject> _shipTypes;

    public static event Action<float> OnSpeedBoost;

    public override int MaxHealth { get; protected set; } = 50;
    public override int CollisionDamage { get; protected set; } = 1;
    private GameUIController _gameUIController;

    [Header("Movement")]
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

    [Header("Weapon")]
    [SerializeField] private Transform _laserStartPoint;
    [SerializeField] private GameObject _laserPrefab;

    public override void Start()
    {
        PlayerDataInitialization();
        _gameUIController = GameObject.FindObjectOfType<GameUIController>();
        base.Start();
        Debug.Log(MaxHealth);
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
        ResetSavedData();
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
        levelController.DisplayEndGame();
        base.Die();
    }

    private void ResetSavedData()
    {
        PlayerPrefs.DeleteKey(Constants.PREFS_KEY_CURRENT_QUEST);
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

    //PLAYER DATA INIT

    private void PlayerDataInitialization()
    {
        
        string jsonString = PlayerPrefs.GetString(Constants.PREFS_PLAYER_DATA, string.Empty);
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });

        SpaceShipScriptableObject _currentShipType = _shipTypes.Find((ship) =>  ship.SpaceShipName == playerData.SpaceShipName);
        
        _laserPrefab.GetComponent<PlayerLaserBehaviour>().SetPlayerLaserDamage(_currentShipType.LaserDamage);
        _maxSpeed = _currentShipType.Speed;
        MaxHealth = _currentShipType.MaxHealth;
        transform.GetComponent<SpriteRenderer>().sprite = _currentShipType.SpaceShipSprite;
    }
}

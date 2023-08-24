using System.Collections;
using UnityEngine;

public class EnemyShipBehaviour : ShipBehaviour
{
    public override int MaxHealth { get; protected set; } = 3;
    public override int CollisionDamage { get; protected set; } = 1;

    private EnemyAI _enemyAI;
    private GameObject _targetSpaceShip;
    [SerializeField] private int _triggerLength = 9;
    private float _distanceFromShip;
    private Coroutine _aiMoveCoroutine;
    private Coroutine _shootRepeatedly;

    private bool _isAICoroutinesStarted;

    private Rigidbody2D _rigidBody;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private Transform _laserStartPoint;

    public override void Start()
    {
        CurrentHealth = MaxHealth;
        _targetSpaceShip = GameObject.FindGameObjectWithTag(Constants.TAG_SPACESHIP);
        _distanceFromShip = 0;
        _isAICoroutinesStarted = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        _enemyAI = GetComponent<EnemyAI>();
        _enemyAI.Initialize(_rigidBody, _targetSpaceShip);
    }
    
    void Update()
    {
        //TODO: when spawns near spaceship, doesnt follow and continue to shoot. Solved mysteriously
        //TODO: Too laggy even with 2 ships Solved with scan after asteroid die. solved by scanning when asteroid dies
        if (_targetSpaceShip == null)
        {
            return;
        }
        _distanceFromShip = Vector2.Distance(transform.position, _targetSpaceShip.transform.position);
        if( _distanceFromShip <= _triggerLength)
        {
            FaceToSpaceShip(_targetSpaceShip);
            if ( !_isAICoroutinesStarted )
            {
                _enemyAI.UpdatePath();
                _aiMoveCoroutine = StartCoroutine( AIMoveCoroutine() );
                _shootRepeatedly = StartCoroutine( ShootRepeatedly() );
                _isAICoroutinesStarted = true;
            }
        }
        else
        {
            if( _isAICoroutinesStarted )
            {
                StopCoroutine(_aiMoveCoroutine);
                StopCoroutine(_shootRepeatedly);
                _isAICoroutinesStarted = false;
            }
        }
    }

    private IEnumerator AIMoveCoroutine()
    {
        while (true)
        {
            _enemyAI.MoveToTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(Constants.TAG_SPACESHIP))
        {
            collision.gameObject.GetComponent<PlayerShipBehaviour>().TakeDamage(CollisionDamage);
        }
    }

    private void FaceToSpaceShip(GameObject spaceShip)
    {
        Vector2 direction = spaceShip.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    IEnumerator ShootRepeatedly()
    {
        while (true)
        {
            ObjectPooler.Instance.SpawnObjectFromPool(Constants.TAG_ENEMY_LASER, _laserStartPoint.position, _laserStartPoint.rotation);
            yield return new WaitForSeconds(1f);
        }
        
    }

    //Health

    public override void Die()
    {
        GameObject gem = Instantiate(_gemPrefab, transform.position, transform.rotation);
        base.Die();
    }

}

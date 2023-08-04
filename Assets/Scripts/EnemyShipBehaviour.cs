using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehaviour : ShipBehaviour
{
    [SerializeField] private int _triggerLength = 8;
    private CapsuleCollider2D _capsuleCollider;
    private Rigidbody2D _rigidBody;
    private EnemyAI _enemyAI;
    private float _distanceFromShip;
    [SerializeField] private int _currentHealth = 3;
    [SerializeField] private int _maxHealth = 3;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _gemPrefab;

     private GameObject _targetSpaceShip;

    //private Coroutine _aiCoroutine;
    private Coroutine _aiMoveCoroutine;
    private Coroutine _shootRepeatedly;

    private bool _isCoroutinesStarted;
    // Start is called before the first frame update
    void Start()
    {
        _targetSpaceShip = GameObject.FindGameObjectWithTag(Constants.TAG_SPACESHIP);
        _currentHealth = _maxHealth;
        _distanceFromShip = 0;
        _isCoroutinesStarted = false;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: when spawns near spaceship, doesnt follow and continue to shoot

        //TODO: Too laggy even with 2 ships
        if (_targetSpaceShip == null)
        {
            return;
        }
        _distanceFromShip = Vector2.Distance(transform.position, _targetSpaceShip.transform.position);
        if( _distanceFromShip <= _triggerLength)
        {
            FaceToSpaceShip(_targetSpaceShip);
            if ( !_isCoroutinesStarted )
            {
                //_aiCoroutine = StartCoroutine("AICoroutine");
                _aiMoveCoroutine = StartCoroutine("AIMoveCoroutine");
                _shootRepeatedly = StartCoroutine("ShootRepeatedly");
                _isCoroutinesStarted = true;
            }
        }
        else
        {
            if( _isCoroutinesStarted )
            {
                //StopCoroutine(_aiCoroutine);
                StopCoroutine(_aiMoveCoroutine);
                StopCoroutine(_shootRepeatedly);
                //_aiCoroutine = null;
                _isCoroutinesStarted = false;
            }
            
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(Constants.TAG_SPACESHIP))
    //    {
    //        _aiCoroutine = StartCoroutine("AICoroutine");
    //        _aiMoveCoroutine = StartCoroutine("AIMoveCoroutine");
    //        _shootRepeatedly = StartCoroutine("ShootRepeatedly");
    //    }
    //}


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(Constants.TAG_SPACESHIP))
    //    {
    //        GameObject spaceShip = collision.gameObject;
    //        FaceToSpaceShip(spaceShip);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(Constants.TAG_SPACESHIP))
    //    {
    //        StopCoroutine(_aiCoroutine);
    //        StopCoroutine(_aiMoveCoroutine);
    //        StopCoroutine(_shootRepeatedly);
    //        _aiCoroutine = null;
    //    }
    //}


    //private IEnumerator AICoroutine()
    //{
    //    //while (true)
    //    //{
    //    //    Debug.Log("ST");
    //    //    _enemyAI.UpdatePath();
    //    //    yield return new WaitForSeconds(0.5f);
    //    //}
    //    yield return new WaitForSeconds(1);
    //}

    private IEnumerator AIMoveCoroutine()
    {
        while (true)
        {
            _enemyAI.MoveToTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.TAG_SPACESHIP) ||
            collision.gameObject.CompareTag(Constants.TAG_ASTEROID))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_LASER))
        {
            TakeDamage(1);
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
            GameObject.Instantiate(_laserPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
        
    }

    //Health

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        GameObject gem = Instantiate(_gemPrefab, transform.position, transform.rotation);
        base.Die();
    }

}

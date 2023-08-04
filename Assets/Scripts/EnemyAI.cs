using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private GameObject _target;
    
    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    private int _currentWaypoint = 0;
    public bool _isReachedTarget;
    [SerializeField] private float _nextWaypointDistance = 3f;
    private float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag(Constants.TAG_SPACESHIP);
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void UpdatePath()
    {
        AstarPath.active.Scan();
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rigidbody.position, _target.transform.position, OnPathComplete);
            
        }
        //MoveToTarget();
    }
    

    private void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    
    public void MoveToTarget()
    {
        if (_currentWaypoint == 0)
        {
            UpdatePath();
        }
        if (_path == null)
        {
            return;
        }

        if(_currentWaypoint >= _path.vectorPath.Count)
        {
            _isReachedTarget = true;
            return;
        }
        else
        {
            _isReachedTarget = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[1] - _rigidbody.position).normalized;
        Vector2 force = direction * speed;
        _rigidbody.AddForce(force);

       
       
        float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);
        if(distance < _nextWaypointDistance)
        {
            _currentWaypoint++;
        }
        else
        {
            _currentWaypoint = 0;
        }
    }
}

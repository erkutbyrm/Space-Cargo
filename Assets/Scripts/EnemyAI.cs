using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public GameObject Target;
    
    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    private bool _isNewPathNeeded;
    private int _currentWaypoint = 0;
    //[SerializeField] private float _nextWaypointDistance = 3f;
    private float speed = 100f;

    // Start is called before the first frame update

    public void Initialize(Rigidbody2D rigidbody2D, GameObject target)
    {
        AstarPath.active.Scan();
        _seeker = GetComponent<Seeker>();
        _rigidbody = rigidbody2D;
        Target = target;
        _isNewPathNeeded = true;
    }
    public void UpdatePath()
    {
        
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rigidbody.position, Target.transform.position, OnPathComplete);
            
        }
        //MoveToTarget();
    }
    

    private void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            _path = path;
            _isNewPathNeeded = false;
            _currentWaypoint = 0;
        }
    }

    
    public void MoveToTarget()
    {
        if (_isNewPathNeeded)
        {
            UpdatePath();
        }
        //if (_currentWaypoint == 0)
        //{
        //    //UpdatePath();
        //}
        if (_path == null)
        {
            return;
        }

        //Debug.Log("vector path count: " + _path.vectorPath.Count + "/ current wayponit: " + _currentWaypoint);


        //if (_currentWaypoint >= _path.vectorPath.Count-1)
        //{
        //    _isReachedTarget = true;
        //    Debug.Log("reached");
        //    return;
        //}
        //else
        //{
        //    _isReachedTarget = false;
        //}

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint +1] - _rigidbody.position).normalized;
        Vector2 force = direction * speed;
        _rigidbody.AddForce(force);

       
       
        //float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);
        //Debug.Log("distance: " + distance + " _nextWaypointDistance: " + _nextWaypointDistance + " current: " + _currentWaypoint);

        //TODO: until no more waypoint remained, increase, then  update
        if (_currentWaypoint < _path.vectorPath.Count-2)
        {
            _currentWaypoint++;
            //Debug.Log("Increased: " + _currentWaypoint);
        }
        else
        {
            _isNewPathNeeded=true;
            //_currentWaypoint = 0;
            //Debug.Log("zero: " + _currentWaypoint);
        }
    }
}

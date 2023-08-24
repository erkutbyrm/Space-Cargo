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
    private float speed = 70f;

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
        if (_path == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint +1] - _rigidbody.position).normalized;
        Vector2 force = direction * speed;
        _rigidbody.AddForce(force);

        //TODO: until no more waypoint remained, increase, then  update
        if (_currentWaypoint < _path.vectorPath.Count-2)
        {
            _currentWaypoint++;
        }
        else
        {
            _isNewPathNeeded=true;
        }
    }
}

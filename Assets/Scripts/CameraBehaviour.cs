using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 _cameraOffset = new Vector3(0,0,-10f);
    private GameObject _spaceShip;
    private SpriteRenderer _backgroundImage;
    [SerializeField] private LevelController _levelController;

    private Vector2 _lerpValues;
    private Vector2 _cameraCenter;
    private void Start()
    {
        _backgroundImage = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _cameraCenter = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        _lerpValues.x = Mathf.Floor( _backgroundImage.bounds.max.x - Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.5f) ).x );
        _lerpValues.y = Mathf.Floor( _backgroundImage.bounds.max.y - Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1f) ).y );
    }

    public void InitializeShip(GameObject ship)
    {
        _spaceShip = ship;
    }

    void Update()
    {
       
        _backgroundImage.transform.localPosition = new Vector3(
            Mathf.Lerp(_lerpValues.x , - _lerpValues.x, (transform.position.x - _cameraCenter.x) / (_levelController.mapLimits.x - 2*_cameraCenter.x) ),
            Mathf.Lerp(_lerpValues.y, - _lerpValues.y, (transform.position.y - _cameraCenter.y) / (_levelController.mapLimits.y - 2*_cameraCenter.y ) ),
            15f
            );

        if (_spaceShip != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(_spaceShip.transform.position.x + _cameraOffset.x, _cameraCenter.x, (_levelController.mapLimits.x - _cameraCenter.x)),
                Mathf.Clamp(_spaceShip.transform.position.y + _cameraOffset.y, _cameraCenter.y, (_levelController.mapLimits.y - _cameraCenter.y)),
                _spaceShip.transform.position.z + _cameraOffset.z);
        }
    }
}

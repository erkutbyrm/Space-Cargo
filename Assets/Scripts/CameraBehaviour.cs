using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 _cameraOffset = new Vector3(0,0,-10f);
    [SerializeField] private GameObject _spaceShip;
    // Update is called once per frame
    void Update()
    {
        if (_spaceShip != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(_spaceShip.transform.position.x + _cameraOffset.x, 13, 87),
                Mathf.Clamp(_spaceShip.transform.position.y + _cameraOffset.y, 6, 93),
                _spaceShip.transform.position.z + _cameraOffset.z);
        }
    }
}

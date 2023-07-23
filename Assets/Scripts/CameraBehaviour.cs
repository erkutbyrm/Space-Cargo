using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 _cameraOffset = new Vector3(0,0,-10f);
    [SerializeField] private GameObject _spaceShip;
    [SerializeField] private GameObject _saturnBackground;

    void Update()
    {
        _saturnBackground.transform.localPosition = new Vector3(
            Mathf.Lerp(9f , -9f, (transform.position.x - 13f) / (87f-13f) ),
            Mathf.Lerp(5f, -5f, (transform.position.y - 6f) / (93f - 6f)),
            15f
            );

        if (_spaceShip != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(_spaceShip.transform.position.x + _cameraOffset.x, 13, 87),
                Mathf.Clamp(_spaceShip.transform.position.y + _cameraOffset.y, 6, 93),
                _spaceShip.transform.position.z + _cameraOffset.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShipWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _laserStartPoint;
    [SerializeField] private GameObject _laserPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectPooler.Instance.SpawnObjectFromPool(Constants.TAG_LASER, _laserStartPoint.position, _laserStartPoint.rotation);
        }
    }
}

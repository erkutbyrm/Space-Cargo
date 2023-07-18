using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShipWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform laserStartPoint;
    [SerializeField] private GameObject laserPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectPooler.Instance.SpawnObjectFromPool(Constants.TAG_LASER, laserStartPoint.position, laserStartPoint.rotation);
        }
    }
}

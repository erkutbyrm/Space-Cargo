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
            Instantiate(laserPrefab, laserStartPoint.position, laserStartPoint.rotation);
        }
    }
}

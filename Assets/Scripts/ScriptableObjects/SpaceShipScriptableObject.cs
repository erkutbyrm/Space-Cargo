using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpaceShip", menuName = "ScriptableObjects/SpaceShip")]
[Serializable]
public class SpaceShipScriptableObject : ScriptableObject
{
    public int ID;
    public string SpaceShipName;
    public float BaseSpeedLimit;
    public int BaseHealth;
    public int LaserDamage;
    public int MaxSpeedUpgrade;
    public int MaxHealthUpgrade;
    public int Price;
    public GameObject Prefab; //TODO: make it ship prefab
}

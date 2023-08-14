using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipCustomizationData
{
    public int ID;
    [NonSerialized] public SpaceShipScriptableObject SpaceShipScriptableObject;
    public int CurrentSpeedUpgrade;
    public int CurrentHealthUpgrade;
}

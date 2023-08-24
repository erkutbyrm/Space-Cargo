using System;

[Serializable]
public class ShipCustomizationData
{
    public int ID;
    [NonSerialized] public SpaceShipScriptableObject SpaceShipScriptableObject;
    public int CurrentSpeedUpgrade;
    public int CurrentHealthUpgrade;
}

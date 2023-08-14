using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string LevelName; //TODO: take to route 
    public int CurrentShipID;
    [NonSerialized] public ShipCustomizationData CurrentShipData;
    public List<ShipCustomizationData> ShipCustomizationDataList;
    public int GemCount;

    public PlayerData() { }
    public PlayerData(string levelName, List<ShipCustomizationData> shipDataList, int currentShipID = 0,
        int gemCount = 0)
    {
        LevelName = levelName;
        ShipCustomizationDataList = shipDataList;
        CurrentShipID = currentShipID;
        GemCount = gemCount;
        CurrentShipData = ShipCustomizationDataList.Find( (ship) => ship.ID == currentShipID );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string LevelName;
    public string SpaceShipName;
    public int TotalSpeedUpgrade;
    public int TotalHealthUpgrade;
    public int GemCount;
    public Dictionary<string, int> Upgrades;

    public PlayerData(string levelName, string spaceShipName, int totalSpeedUpgrade = 5, int totalHealthUpggrade = 5, int gemCount = 0, Dictionary<string,int> upgradesDictionary = null)
    {
        LevelName = levelName;
        SpaceShipName = spaceShipName;
        TotalSpeedUpgrade = totalSpeedUpgrade;
        TotalHealthUpgrade = totalHealthUpggrade;
        GemCount = gemCount;

        Upgrades = upgradesDictionary ?? new Dictionary<string, int>();
    }
}

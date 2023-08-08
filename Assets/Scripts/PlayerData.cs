using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string LevelName;
    public string SpaceShipName;
    public int SpeedUpgrade;

    public PlayerData(string levelName, string spaceShipName, int speedUpgrade)
    {
        LevelName = levelName;
        SpaceShipName = spaceShipName;
        SpeedUpgrade = speedUpgrade;
    }
}

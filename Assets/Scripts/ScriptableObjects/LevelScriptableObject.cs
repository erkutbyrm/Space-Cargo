using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    public string LevelName;
    public int GemCount;
    public Color GemColour;
    public int AsteroidCount;
    public int EnemyCount;
    public int CargoCount;
    public int SpeedBoostCount;
    public GameObject BackgroundImage;
}

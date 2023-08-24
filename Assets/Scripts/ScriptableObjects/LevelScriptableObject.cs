using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel" ,menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public string LevelName;
    public TextAsset LevelDescription;
    public int GemCount;
    public Color GemColour;
    public int AsteroidCount;
    public int EnemyCount;
    public int CargoCount;
    public int SpeedBoostCount;
    public Vector2 MapLimits;
    public Sprite BackgroundSprite;
}

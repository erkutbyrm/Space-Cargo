using UnityEngine;

[CreateAssetMenu(fileName = "New SpaceShip", menuName = "ScriptableObjects/SpaceShip")]
public class SpaceShipScriptableObject : ScriptableObject
{
    public string SpaceShipName;
    public float Speed;
    public int MaxHealth;
    public int LaserDamage;
    public Sprite SpaceShipSprite;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipCatalog" ,menuName = "ScriptableObjects/ShipCatalog")]
public class ShipCatalog : ScriptableObject
{
    private static ShipCatalog _instance;
    public static ShipCatalog Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<ShipCatalog>(typeof(ShipCatalog).Name);
            }

            return _instance;
        }
    }

    [field: SerializeField] public List<SpaceShipScriptableObject> ShipDataList { get; private set; }
    [field: SerializeField] public SpaceShipScriptableObject DefaultShip { get; private set; }

    public SpaceShipScriptableObject GetShipByID(int id)
    {
        return ShipDataList.Find( ship => ship.ID == id );
    }
}

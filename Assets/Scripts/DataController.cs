using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController
{
    private static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataController();
                _instance.Initialize();
            }
            return _instance;
        }
        private set
        {
            if (_instance == null)
            {
                _instance = value;
                _instance.Initialize();
            }
        }
    }

    public PlayerData PlayerData { get; private set; }

    public void Initialize()
    {
        //Debug.Log("prefs:"+PlayerPrefs.GetString(Constants.PREFS_PLAYER_DATA));
        PlayerData = GetDataFromPrefs<PlayerData>(Constants.PREFS_PLAYER_DATA);
        if( PlayerData == null )
        {
            PlayerData = GenerateDummyData();
            WriteDataToPrefs<PlayerData>(PlayerData, Constants.PREFS_PLAYER_DATA);
        }
        for( int i = 0; i < PlayerData.ShipCustomizationDataList.Count; i++ )
        {
            for(int j = 0; j < ShipCatalog.Instance.ShipDataList.Count; j++ )
            {
                //Debug.Log("sc: "+PlayerData.ShipCustomizationDataList[i].ID);
                //Debug.Log("sd: "+ShipCatalog.Instance.ShipDataList[j].ID);
                Debug.Log(ShipCatalog.Instance.ShipDataList[j].SpaceShipName);
                if (PlayerData.ShipCustomizationDataList[i].ID == ShipCatalog.Instance.ShipDataList[j].ID)
                {
                    PlayerData.ShipCustomizationDataList[i].SpaceShipScriptableObject = ShipCatalog.Instance.ShipDataList[j];
                }
            }
        }
        //TODO: solved by adding this line!!
        PlayerData.CurrentShipData = PlayerData.ShipCustomizationDataList.Find( ship => ship.ID == PlayerData.CurrentShipID);

        Debug.Log("playerdata:" + (PlayerData!=null) );
    }

    private PlayerData GenerateDummyData()
    {
        List<ShipCustomizationData> shipCustomizationDataList = new List<ShipCustomizationData>();
        SpaceShipScriptableObject defaultShip = ShipCatalog.Instance.DefaultShip;
        //TODO: constructorless initialization

        shipCustomizationDataList.Add(new ShipCustomizationData()
        {
            ID = defaultShip.ID,
            SpaceShipScriptableObject = defaultShip,
            CurrentHealthUpgrade = 0,
            CurrentSpeedUpgrade = 0,
        });

        return new PlayerData(Constants.LEVEL_NAME_EARTH, shipCustomizationDataList, defaultShip.ID ,gemCount: 500);
    }


    public void GetDataFromFile<T>()
    {
        throw new System.NotImplementedException();
    }

    public T GetDataFromPrefs<T>(string key)
    {
        Debug.Log("get: "+PlayerPrefs.GetString(key));
        string jsonString = PlayerPrefs.GetString(key, string.Empty);
        T playerData = JsonConvert.DeserializeObject<T>(jsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });

        return playerData;
    }

    public void WriteDataToPrefs<T>(T data, string key)
    {

        string jsonString = JsonConvert.SerializeObject(data, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        Debug.Log("set: " + jsonString);

        PlayerPrefs.SetString(key, jsonString);
    }
}

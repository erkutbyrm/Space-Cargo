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
        PlayerData = GetDataFromPrefs<PlayerData>(Constants.PREFS_PLAYER_DATA);
    }

    public void GetDataFromFile<T>()
    {
        throw new System.NotImplementedException();
    }

    public T GetDataFromPrefs<T>(string key)
    {
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
        Debug.Log(jsonString);
        PlayerPrefs.SetString(key, jsonString);
    }
}

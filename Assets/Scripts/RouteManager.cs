using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    private static RouteManager _instance;
    public static RouteManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject someObject = new GameObject(nameof(RouteManager));
                _instance = someObject.AddComponent<RouteManager>();
                DontDestroyOnLoad(someObject);
            }
            return _instance;
        }
        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    private void Awake()
    {
        if( _instance != this )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        DataController.Instance.WriteDataToPrefs(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);
    }
}

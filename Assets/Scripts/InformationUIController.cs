using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationUIController : MonoBehaviour
{
    [SerializeField] private GameObject _buttonStart;
    [SerializeField] private TextMeshProUGUI _worldName;
    [SerializeField] private TextMeshProUGUI _worldDescription;

    [SerializeField] private List<LevelScriptableObject> _levels;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = PlayerPrefs.GetString(Constants.PREFS_PLAYER_DATA, string.Empty);
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });

        LevelScriptableObject currentLevel = _levels.Find((level) => playerData.LevelName == level.LevelName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

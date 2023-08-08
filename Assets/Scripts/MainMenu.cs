using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject _baseLevel;
    [SerializeField] private SpaceShipScriptableObject _baseShip;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(Constants.PREFS_PLAYER_DATA))
        {
            Debug.Log("creaated playerdata");
            PlayerData playerData = new PlayerData("Earth", "SpaceShip_1", 0);
            string jsonString = JsonConvert.SerializeObject(playerData, settings: new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            PlayerPrefs.SetString(Constants.PREFS_PLAYER_DATA, jsonString);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MapSelectionScene");
    }

    public void GoToSettingsMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

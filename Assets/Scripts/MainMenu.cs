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
            Dictionary<string, int> upgrades = new Dictionary<string, int>();
            upgrades.Add(Constants.PLAYER_UPGRADE_SPEED, 0);
            upgrades.Add(Constants.PLAYER_UPGRADE_HEALTH, 0);

            PlayerData playerData = new PlayerData("Earth", "SpaceShip_1", upgradesDictionary: upgrades, gemCount: 500);
            DataController.Instance.WriteDataToPrefs<PlayerData>(playerData, Constants.PREFS_PLAYER_DATA);
            DataController.Instance.Initialize();
            //TODO: should not call initialize here!!
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(Constants.SCENE_MAP_SELECT);
    }

    public void GoToSettingsMenu()
    {

    }

    public void GoToMarket()
    {
        SceneManager.LoadScene(Constants.SCENE_MARKET);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

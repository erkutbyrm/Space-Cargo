using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _mapSelectionMenu;
    [SerializeField] private GameObject _marketPanel;
    [SerializeField] private GameObject _mapInfoPanel;


    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.SetActive(true);
        _mapSelectionMenu.SetActive(false);
        _marketPanel.SetActive(false);
        _mapInfoPanel.SetActive(false);
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
        _mapSelectionMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void GoToSettingsMenu()
    {

    }

    public void GoToMarket()
    {
        _marketPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

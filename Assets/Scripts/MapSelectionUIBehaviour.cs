using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MapSelectionUIBehaviour : MonoBehaviour
{
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private List<LevelScriptableObject> _levelList;
    [SerializeField] private Transform _buttonMapContainer;
    [SerializeField] private Image _background;
    public GameObject _mapSelectButtonPrefab;

    private void Start()
    {
        //InitializeButtons();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MainMenu.gameObject);
    }

    public void StartLevel(string name)
    {
        SceneTypeChange(name);
        _mainMenuController.OpenMenu(_mainMenuController.MapInfoPanel.gameObject);
    }

    public void OnHover(string name)
    {
        switch (name)
        {
            case "Earth":
                _background.sprite = _levelList[0].BackgroundSprite;
                break;
            case "Saturn": 
                _background.sprite = _levelList[1].BackgroundSprite;
                break;
            default: 
                _background.sprite = _levelList[0].BackgroundSprite;
                break;
        }
    }

    private void SceneTypeChange(string levelName)
    {
        DataController.Instance.PlayerData.LevelName = levelName;
        DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);
    }

    private void InitializeButtons()
    {
        foreach (LevelScriptableObject level in _levelList)
        {
            throw new System.NotImplementedException();
            GameObject newButton = GameObject.Instantiate(_mapSelectButtonPrefab, _buttonMapContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = level.LevelName;
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            
        }
    }


}

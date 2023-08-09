using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class MapSelection : MonoBehaviour
{
    [SerializeField] private List<LevelScriptableObject> _levelList;
    [SerializeField] private Transform _buttonMapContainer;
    [SerializeField] private Image _background;

    public void StartLevel(string name)
    {
        SceneTypeChange(name);
        SceneManager.LoadScene(Constants.SCENE_GAME);
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
}

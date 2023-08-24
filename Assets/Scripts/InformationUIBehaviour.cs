using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class InformationUIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _worldName;
    [SerializeField] private TextMeshProUGUI _worldDescription;
    [SerializeField] private MainMenuController _mainMenuController;

    [SerializeField] private List<LevelScriptableObject> _levels;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    private void OnEnable()
    {
        UpdateInformationText();
    }

    public void UpdateInformationText()
    {
        LevelScriptableObject currentLevel = _levels.Find((level) => DataController.Instance.PlayerData.LevelName == level.LevelName);
        _worldName.text = currentLevel.LevelName;
        _worldDescription.text = currentLevel.LevelDescription.text;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(Constants.SCENE_GAME);
    }

    public void GoToMainMenu()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MainMenu.gameObject);
    }
}

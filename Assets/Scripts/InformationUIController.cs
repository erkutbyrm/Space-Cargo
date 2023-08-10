using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformationUIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _mapInfoPanel;
    [SerializeField] private TextMeshProUGUI _worldName;
    [SerializeField] private TextMeshProUGUI _worldDescription;

    [SerializeField] private List<LevelScriptableObject> _levels;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
        _mainMenu.SetActive(true);
        _mapInfoPanel.SetActive(false);
    }
}

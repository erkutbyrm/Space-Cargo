using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{


    [Header("UI")]
    [SerializeField] private GameUIController _gameUIController;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winScreenPanel;
    [SerializeField] private CollectableDataController _collectableDataController;

    public static bool IsPaused { get; protected set; }
    private bool _isWon;

    public Quest currentQuest { get; protected set; }
    public Vector2 mapLimits { get; protected set; }

    private void Awake()
    {
        mapLimits = new Vector2(100,100);
    }

    private void Start()
    {
        currentQuest = LoadQuestFromLocal();

        if (currentQuest == null)
        {
            currentQuest = GenerateCargoQuest();
            Debug.Log("generated");
            SaveQuestToLocal(currentQuest);
        }
        Debug.Log("quest target count: " + ((CargoQuest)currentQuest).TargetCargoCount);
        Time.timeScale = 1f;
        IsPaused = false;
        _isWon = false;
        _gameOverPanel.SetActive(false);
        _pauseMenu.SetActive(false);
        _winScreenPanel.SetActive(false);

        _gameUIController.UpdateCargoCounterText(
            ((CargoQuest)currentQuest).CollectedCargoCount,
            ((CargoQuest)currentQuest).TargetCargoCount
            );
    }

    void Update()
    {
        if (!_isWon && Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private Quest LoadQuestFromLocal()
    {
        string jsonString = PlayerPrefs.GetString(Constants.PREFS_KEY_CURRENT_QUEST, string.Empty);
        return JsonConvert.DeserializeObject<Quest>(jsonString, settings: new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });
    }

    private void SaveQuestToLocal(Quest quest)
    {
        string jsonString = JsonConvert.SerializeObject(quest, settings: new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        PlayerPrefs.SetString(Constants.PREFS_KEY_CURRENT_QUEST, jsonString);
    }

    private Quest GenerateCargoQuest()
    {
        return new CargoQuest(3);
    }

    private void OnDestroy()
    {
        if (PlayerPrefs.HasKey(Constants.PREFS_KEY_CURRENT_QUEST))
        {
            SaveQuestToLocal(currentQuest);
        }
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Constants.SCENE_GAME);
    }

    public void DisplayEndGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        _gameOverPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.SCENE_MAIN_MENU);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   public void TryWin()
    {
        Debug.Log("gemco"+PlayerPrefs.GetInt(Constants.PREFS_KEY_GEM_COUNT));
        if (currentQuest.CheckCompleted())
        {
            _isWon = true;
            Time.timeScale = 0f;
            IsPaused = true;
            _winScreenPanel.SetActive(true);
            PlayerPrefs.DeleteKey(Constants.PREFS_KEY_CURRENT_QUEST);
            if(PlayerPrefs.HasKey(Constants.PREFS_KEY_GEM_COUNT))
            {
                PlayerPrefs.SetInt(
                    Constants.PREFS_KEY_GEM_COUNT, 
                    PlayerPrefs.GetInt(Constants.PREFS_KEY_GEM_COUNT) + _collectableDataController.CollectedGemCount
                    );
            }
            else
            {
                PlayerPrefs.SetInt(
                    Constants.PREFS_KEY_GEM_COUNT,
                    _collectableDataController.CollectedGemCount
                    );
            }
            
        }
        else
        {
            Debug.Log("remaining cargo to collect: "+ (((CargoQuest)currentQuest).TargetCargoCount - ((CargoQuest)currentQuest).CollectedCargoCount) );
        }
    }
}

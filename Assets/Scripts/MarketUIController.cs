using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gemsText;
    [SerializeField] GameObject _barPrefab;
    [SerializeField] private GameObject _barContainerSpeed;
    [SerializeField] private GameObject _barContainerHealth;
    [SerializeField] private GameObject _textNotEnoughGems;

    private float _textTime = 3f;
    private bool _isTextShowing = false;

    private int _gemCount;
    private int _currentSpeedUpgrade;
    private int _currentHealthUpgrade;
    private int _totalSpeedUpgradeCount;
    private int _totalHealthUpgradeCount;
    private int _currentSpeedUpgradePrice = 10;
    private int _currentHealthUpgradePrice = 5;


    private Dictionary<string, List<GameObject>> _barDictionary = new Dictionary<string, List<GameObject>>();

    private const string GEM = "Gem";


    void Start()
    {
        Initialize();
        _barDictionary.Add(Constants.PLAYER_UPGRADE_SPEED, new List<GameObject>());
        _barDictionary.Add(Constants.PLAYER_UPGRADE_HEALTH, new List<GameObject>());
        _textNotEnoughGems.SetActive(false);
        _isTextShowing = false;
        DrawGemCount();
        DrawSpeedUpgrade();
        DrawHealthUpgrade();
    }

    private void Initialize()
    {
        _totalSpeedUpgradeCount = DataController.Instance.PlayerData.TotalSpeedUpgrade;
        _totalHealthUpgradeCount = DataController.Instance.PlayerData.TotalHealthUpgrade;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Constants.SCENE_MAIN_MENU);
        }
    }
    public void DrawGemCount()
    {
        UpdateFromLocal(GEM);
        _gemsText.text = "Total Gems: " + _gemCount;
    }
    private void UpdateFromLocal(string name)
    {
        switch (name)
        {
            case GEM:
                _gemCount = DataController.Instance.PlayerData.GemCount;
                break;
            case Constants.PLAYER_UPGRADE_SPEED:
                _currentSpeedUpgrade = DataController.Instance.PlayerData.Upgrades[Constants.PLAYER_UPGRADE_SPEED];
                break;
            case Constants.PLAYER_UPGRADE_HEALTH:
                _currentHealthUpgrade = DataController.Instance.PlayerData.Upgrades[Constants.PLAYER_UPGRADE_HEALTH];
                break;
            default:
                break;
        }
    }

    public void DrawSpeedUpgrade()
    {
        DrawBars(Constants.PLAYER_UPGRADE_SPEED, _totalSpeedUpgradeCount);
        UpdateBars(Constants.PLAYER_UPGRADE_SPEED);
    }

    public void DrawHealthUpgrade()
    {
        DrawBars(Constants.PLAYER_UPGRADE_HEALTH, _totalHealthUpgradeCount);
        UpdateBars(Constants.PLAYER_UPGRADE_HEALTH);
    }

    public void DrawBars(string upgradeName, int count)
    {
        GameObject _barContainer;
        switch (upgradeName)
        {
            case Constants.PLAYER_UPGRADE_SPEED:
                _barContainer = _barContainerSpeed;
                break;
            case Constants.PLAYER_UPGRADE_HEALTH:
                _barContainer = _barContainerHealth;
                Debug.Log("hel");
                break;
            default:
                _barContainer = _barContainerSpeed;
                break;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject newBar = Instantiate(_barPrefab, _barContainer.transform);
            newBar.transform.localScale = new Vector3(2,2,1);
            _barDictionary[upgradeName].Add(newBar);
        }
    }

    public void UpdateBars(string upgradeName)
    {
        UpdateFromLocal(upgradeName);
        int currentFullBar;
        switch (upgradeName)
        {
            case Constants.PLAYER_UPGRADE_SPEED:
                currentFullBar = _currentSpeedUpgrade;
                break;
            case Constants.PLAYER_UPGRADE_HEALTH:
                currentFullBar = _currentHealthUpgrade;
                Debug.Log("lo");
                break;
            default:
                currentFullBar = 0;
                break;
        }

        for (int i = 0; i < _barDictionary[upgradeName].Count; i++)
        {
            if (i < currentFullBar)
            {
                _barDictionary[upgradeName][i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(true);
            }
            else
            {
                _barDictionary[upgradeName][i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(false);
            }
        }
    }


    //buttons

    public void UpgradeSpeed()
    {
        if( _gemCount >= _currentSpeedUpgradePrice && _currentSpeedUpgrade < _totalSpeedUpgradeCount)//TODO: condition is met
        {
            _currentSpeedUpgrade++;
            _gemCount -= _currentSpeedUpgradePrice;

            DataController.Instance.PlayerData.Upgrades[Constants.PLAYER_UPGRADE_SPEED] = _currentSpeedUpgrade;
            DataController.Instance.PlayerData.GemCount = _gemCount;

            DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);
            
            DrawGemCount();
            UpdateBars(Constants.PLAYER_UPGRADE_SPEED);
        }
        else
        {
            if(!_isTextShowing)
            {
                StartCoroutine(NotEnoughGemsCoroutine());
            }
        }
    }

    public void UpgradeHealth()
    {
        if (_gemCount >= _currentSpeedUpgradePrice && _currentHealthUpgrade < _totalHealthUpgradeCount)//TODO: condition is met
        {
            _currentHealthUpgrade++;
            _gemCount -= _currentHealthUpgradePrice;

            DataController.Instance.PlayerData.Upgrades[Constants.PLAYER_UPGRADE_HEALTH] = _currentHealthUpgrade;
            DataController.Instance.PlayerData.GemCount = _gemCount;

            DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);

            DrawGemCount();
            UpdateBars(Constants.PLAYER_UPGRADE_HEALTH);

        }
        else
        {
            if (!_isTextShowing)
            {
                StartCoroutine(NotEnoughGemsCoroutine());
            }
        }
    }

    IEnumerator NotEnoughGemsCoroutine()
    {
        _isTextShowing = true;
        _textNotEnoughGems.SetActive(true);
        yield return new WaitForSeconds(_textTime);
        _textNotEnoughGems.SetActive(false);
        _isTextShowing = false;
    }
}

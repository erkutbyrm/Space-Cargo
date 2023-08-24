using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketUIBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gemsText;
    [SerializeField] GameObject _barPrefab;
    [SerializeField] private GameObject _barContainerSpeed;
    [SerializeField] private GameObject _barContainerHealth;
    [SerializeField] private GameObject _textWarning;
    [SerializeField] private MainMenuController _mainMenuController;

    private float _textTime = 3f;
    private bool _isTextShowing = false;

    private int _gemCount;
    private int _currentSpeedUpgrade;
    private int _currentHealthUpgrade;
    private int _totalSpeedUpgradeCount;
    private int _totalHealthUpgradeCount;
    private int _currentSpeedUpgradePrice = 10;
    private int _currentHealthUpgradePrice = 5;

    private const string TEXT_NOT_ENOUGH_GEMS = "You do not have enough gems to buy the upgrade!";
    private const string TEXT_UPGRADE_FULL = "You have already maxed this upgrade!";

    private Dictionary<string, List<GameObject>> _barDictionary = new Dictionary<string, List<GameObject>>();

    private const string GEM = "Gem";


    void Start()
    {
        Initialize();
        
    }

    public void Initialize()
    {
        _totalSpeedUpgradeCount = DataController.Instance.PlayerData.CurrentShipData.SpaceShipScriptableObject.MaxSpeedUpgrade;
        _totalHealthUpgradeCount = DataController.Instance.PlayerData.CurrentShipData.SpaceShipScriptableObject.MaxHealthUpgrade;
        _barDictionary.Clear();
        _barDictionary.Add(Constants.PLAYER_UPGRADE_SPEED, new List<GameObject>());
        _barDictionary.Add(Constants.PLAYER_UPGRADE_HEALTH, new List<GameObject>());
        _textWarning.SetActive(false);
        _isTextShowing = false;
        DrawGemCount();
        DrawSpeedUpgrade();
        DrawHealthUpgrade();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToShipMarket();
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
                _currentSpeedUpgrade = DataController.Instance.PlayerData.CurrentShipData.CurrentSpeedUpgrade;
                break;
            case Constants.PLAYER_UPGRADE_HEALTH:
                _currentHealthUpgrade = DataController.Instance.PlayerData.CurrentShipData.CurrentHealthUpgrade;
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
                break;
            default:
                _barContainer = _barContainerSpeed;
                break;
        }
        ClearChildren(_barContainer);
        for (int i = 0; i < count; i++)
        {
            GameObject newBar = Instantiate(_barPrefab, _barContainer.transform);
            newBar.transform.localScale = new Vector3(2,2,1);
            _barDictionary[upgradeName].Add(newBar);
        }
    }

    private void ClearChildren(GameObject container)
    {
        container.transform.ClearChildren();
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

            DataController.Instance.PlayerData.CurrentShipData.CurrentSpeedUpgrade = _currentSpeedUpgrade;
            DataController.Instance.PlayerData.GemCount = _gemCount;

            DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);
            
            DrawGemCount();
            UpdateBars(Constants.PLAYER_UPGRADE_SPEED);
        }
        else
        {
            if(!_isTextShowing)
            {
                if (_gemCount < _currentSpeedUpgradePrice)
                {
                    StartCoroutine(TextWarningCoroutine(TEXT_NOT_ENOUGH_GEMS));
                }
                else if (_currentSpeedUpgrade >= _totalSpeedUpgradeCount)
                {
                    StartCoroutine(TextWarningCoroutine(TEXT_UPGRADE_FULL));
                }
            }
        }
    }

    public void UpgradeHealth()
    {
        if (_gemCount >= _currentSpeedUpgradePrice && _currentHealthUpgrade < _totalHealthUpgradeCount)//TODO: condition is met
        {
            _currentHealthUpgrade++;
            _gemCount -= _currentHealthUpgradePrice;

            DataController.Instance.PlayerData.CurrentShipData.CurrentHealthUpgrade = _currentHealthUpgrade;
            DataController.Instance.PlayerData.GemCount = _gemCount;

            DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);

            DrawGemCount();
            UpdateBars(Constants.PLAYER_UPGRADE_HEALTH);

        }
        else
        {
            if (!_isTextShowing)
            {
                if (_gemCount < _currentSpeedUpgradePrice)
                {
                    StartCoroutine(TextWarningCoroutine(TEXT_NOT_ENOUGH_GEMS));
                }
                else if (_currentSpeedUpgrade >= _totalSpeedUpgradeCount)
                {
                    StartCoroutine(TextWarningCoroutine(TEXT_UPGRADE_FULL));
                }
            }
        }
    }

    public void GoBackToShipMarket()
    {
        _mainMenuController.OpenMenu(_mainMenuController.ShipSelectionPanel.gameObject);
    }

    IEnumerator TextWarningCoroutine(string text)
    {
        _isTextShowing = true;
        _textWarning.GetComponent<TextMeshProUGUI>().text = text;
        _textWarning.SetActive(true);
        yield return new WaitForSeconds(_textTime);
        _textWarning.SetActive(false);
        _isTextShowing = false;
    }
}

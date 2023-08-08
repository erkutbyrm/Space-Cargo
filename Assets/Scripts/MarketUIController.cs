using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gemsText;
    [SerializeField] GameObject _barPrefab;
    [SerializeField] private GameObject _barContainer;

    private int _gemCount;
    private int _currentSpeedUpgrade;
    private int _totalSpeedUpgradeCount = 10;
    private int _currentSpeedUpgradePrice = 2;

    private List<GameObject> _barList = new List<GameObject>();

    void Start()
    {
        DrawGemCount();
        DrawBars();
        UpdateBars();
    }

    public void DrawGemCount()
    {
        UpdateGemCountFromLocal();
        _gemsText.text = "Total Gems: " + _gemCount;
    }

    private void UpdateGemCountFromLocal()
    {
        _gemCount = PlayerPrefs.GetInt(Constants.PREFS_KEY_GEM_COUNT);
    }

    private void UpdateCurrentSpeedUpgradeFromLocal()
    {
        //TODO: make it from player data
        _currentSpeedUpgrade = PlayerPrefs.GetInt(Constants.PREFS_CURRENT_SPEED_UPGRADE);
    }

    public void DrawSpeedUpgrade()
    {
        DrawBars();
    }

    public void DrawBars()
    {
        for (int i = 0; i < _totalSpeedUpgradeCount; i++)
        {
            GameObject newBar = Instantiate(_barPrefab, _barContainer.transform);
            newBar.transform.localScale = new Vector3(2,2,1);
            _barList.Add(newBar);
        }
    }

    public void UpdateBars()
    {
        UpdateCurrentSpeedUpgradeFromLocal();
        for (int i = 0; i < _barList.Count; i++)
        {
            if (i < _currentSpeedUpgrade)
            {
                _barList[i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(true);
            }
            else
            {
                _barList[i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(false);
            }
        }
    }


    //buttons

    public void UpgradeSpeed()
    {
        if( _gemCount > _currentSpeedUpgradePrice)//TODO: condition is met
        {
            _currentSpeedUpgrade++;
            _gemCount -= _currentSpeedUpgradePrice;
            PlayerPrefs.SetInt(Constants.PREFS_KEY_GEM_COUNT, _gemCount);
            PlayerPrefs.SetInt(Constants.PREFS_CURRENT_SPEED_UPGRADE, _currentSpeedUpgrade);
            DrawGemCount();
            UpdateBars();
        }
        else
        {
            Debug.Log("You do not have enough gems to buy this upgrade!");
        }
    }
}

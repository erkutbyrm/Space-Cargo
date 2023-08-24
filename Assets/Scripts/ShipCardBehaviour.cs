using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipCardBehaviour : MonoBehaviour
{
    public static event Action<string> WarningTextShipCard;
    public static event Action OnShipSelected;
    public static event Action OnShipBought;
    private int _shipID;
    [SerializeField] private TextMeshProUGUI _shipName;
    [SerializeField] private Image _shipImage;
    [SerializeField] private Slider _speedStatSlider;
    [SerializeField] private Slider _healthStatSlider;
    [SerializeField] private GameObject _buttonBuy;
    private int _maxStatValue = 10;

    public void SetCard(int id, string shipName, Sprite shipSprite, float speedStat, int healthStat)
    {
        _shipID = id;
        _shipName.text = shipName;
        _shipImage.sprite = shipSprite;
        _speedStatSlider.value = speedStat/_maxStatValue;
        _healthStatSlider.value = ((float)healthStat)/_maxStatValue;
        int price = ShipCatalog.Instance.GetShipByID(id).Price;
        _buttonBuy.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy ({price} Gems)";
        if (DataController.Instance.PlayerData.ShipCustomizationDataList.Find(ship => ship.ID == _shipID) != null)
        {
            _buttonBuy.SetActive(false);
        }
    }

    public void SelectShip()
    {
        ShipCustomizationData shipToSelect = DataController.Instance.PlayerData.ShipCustomizationDataList.Find(ship => ship.ID == _shipID);
        if (shipToSelect != null)
        {
            DataController.Instance.PlayerData.CurrentShipData = shipToSelect;
            DataController.Instance.PlayerData.CurrentShipID = _shipID;
            
            string message = "Ship: \""+ shipToSelect.SpaceShipScriptableObject.SpaceShipName +"\" selected succesfully";
            WarningTextShipCard?.Invoke(message);
            OnShipSelected?.Invoke();
        }
        else
        {
            string message = "You need to buy the ship first";
            WarningTextShipCard?.Invoke(message);
        }
    }

    public void BuyShip()
    {
        string message;
        SpaceShipScriptableObject shipToBuy = ShipCatalog.Instance.GetShipByID(_shipID);
        if (DataController.Instance.PlayerData.GemCount < shipToBuy.Price)
        {
            message = "You do not have enough gems to buy this ship!";
            WarningTextShipCard?.Invoke(message);
            return;
        }
        DataController.Instance.PlayerData.GemCount -= shipToBuy.Price;
        DataController.Instance.PlayerData.ShipCustomizationDataList.Add(new ShipCustomizationData()
        {
            ID = _shipID,
            SpaceShipScriptableObject = shipToBuy,
            CurrentHealthUpgrade = 0,
            CurrentSpeedUpgrade = 0,
        });
        DataController.Instance.WriteDataToPrefs<PlayerData>(DataController.Instance.PlayerData, Constants.PREFS_PLAYER_DATA);
        _buttonBuy.SetActive(false);
        
        message = $"Successfully bought ship: \"{shipToBuy.SpaceShipName}\"";
        WarningTextShipCard?.Invoke(message);
        OnShipBought?.Invoke();
    }
}

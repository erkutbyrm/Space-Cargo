using System.Collections;
using TMPro;
using UnityEngine;

public class ShipSelectionUIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _shipCardContainer;
    [SerializeField] private GameObject _shipCardPrefab;
    [SerializeField] private MarketUIBehaviour _marketUIBehaviour;
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private TextMeshProUGUI _textWarning;
    [SerializeField] private TextMeshProUGUI _gemsText;

    private bool _isTextShowing = false;
    private float _textTime = 1f;
    void Start()
    {
        _isTextShowing = false;
        _textWarning.gameObject.SetActive(false);
        foreach (SpaceShipScriptableObject ship in ShipCatalog.Instance.ShipDataList)
        {
            GameObject newCard = GameObject.Instantiate(_shipCardPrefab, parent: _shipCardContainer.transform);
            newCard.GetComponent<ShipCardBehaviour>().SetCard(ship.ID, ship.SpaceShipName, ship.Prefab.GetComponent<SpriteRenderer>().sprite, ship.BaseSpeedLimit, ship.BaseHealth);
        }
        DrawGemCount();
    }

    private void OnEnable()
    {
        DrawGemCount();
        ShipCardBehaviour.WarningTextShipCard += ShowWarningText;
        ShipCardBehaviour.OnShipBought += DrawGemCount;
        ShipCardBehaviour.OnShipSelected += OnShipSelected;

    }

    private void OnDisable()
    {
        ShipCardBehaviour.WarningTextShipCard -= ShowWarningText;
        ShipCardBehaviour.OnShipBought -= DrawGemCount;
        ShipCardBehaviour.OnShipSelected -= OnShipSelected;
    }

    private void OnShipSelected()
    {
        _marketUIBehaviour.Initialize();
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

    public void GoToUpgradesMenu()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MarketPanel.gameObject);
    }

    private void ShowWarningText(string text)
    {
        if(!_isTextShowing) StartCoroutine(ShowWarningTextCoroutine(text));
        DrawGemCount();
    }

    public void DrawGemCount()
    {
        int _gemCount = DataController.Instance.PlayerData.GemCount;
        _gemsText.text = "Total Gems: " + _gemCount;
    }

    IEnumerator ShowWarningTextCoroutine(string text)
    {
        _isTextShowing = true;
        _textWarning.text = text;
        _textWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(_textTime);
        _textWarning.gameObject.SetActive(false);
        _isTextShowing = false;
    }
}

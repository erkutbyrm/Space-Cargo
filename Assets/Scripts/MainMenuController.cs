using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [field: SerializeField] public MainMenuUIBehaviour MainMenu { get; private set; }
    [field: SerializeField] public SettingsMenuBehaviour SettingsMenu { get; private set; }
    [field: SerializeField] public MapSelectionUIBehaviour MapSelectionMenu { get; private set; }
    [field: SerializeField] public MarketUIBehaviour MarketPanel {get; private set; }
    [field: SerializeField] public InformationUIBehaviour MapInfoPanel {get; private set; }
    [field: SerializeField] public ShipSelectionUIBehaviour ShipSelectionPanel {get; private set; }


    public void OpenMenu(GameObject targetMenu)
    {
        MainMenu.gameObject.SetActive(false);
        MapSelectionMenu.gameObject.SetActive(false);
        MarketPanel.gameObject.SetActive(false);
        MapInfoPanel.gameObject.SetActive(false);
        ShipSelectionPanel.gameObject.SetActive(false);
        SettingsMenu.gameObject.SetActive(false);

        targetMenu.SetActive(true);
    }

    private void Start()
    {
        OpenMenu(MainMenu.gameObject);
    }
}

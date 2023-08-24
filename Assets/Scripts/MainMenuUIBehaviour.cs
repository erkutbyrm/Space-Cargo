using UnityEngine;

public class MainMenuUIBehaviour : MonoBehaviour
{
    [SerializeField] private MainMenuController _mainMenuController;

    void Start()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MainMenu.gameObject);
    }

    public void PlayGame()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MapSelectionMenu.gameObject);
    }

    public void GoToSettingsMenu()
    {
        _mainMenuController.OpenMenu(_mainMenuController.SettingsMenu.gameObject);
    }

    public void GoToMarket()
    {
        _mainMenuController.OpenMenu(_mainMenuController.ShipSelectionPanel.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

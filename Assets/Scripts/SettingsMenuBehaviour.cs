using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuBehaviour : MonoBehaviour
{
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;

    private void OnEnable()
    {
        _audioMixer.GetFloat(Constants.MASTER_VOLUME, out float volume);
        _slider.value = volume;
        _toggle.enabled = Screen.fullScreen;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    public void SetMasterVolume(float value)
    {
        _audioMixer.SetFloat(Constants.MASTER_VOLUME, value);
    }

    public void SetFullscreenValue(bool value)
    {
        Screen.fullScreen = value;
    }

    public void GoToMainMenu()
    {
        _mainMenuController.OpenMenu(_mainMenuController.MainMenu.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject _pauseMenu;
    private bool _isPaused;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverPanel;


    void Start()
    {
        _gameOverPanel.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void DisplayEndGame()
    {
        _gameOverPanel.SetActive(true);
    }
}

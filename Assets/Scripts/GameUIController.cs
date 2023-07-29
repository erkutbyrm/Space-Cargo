using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private GameObject _barPrefab;
    [SerializeField] private GameObject barContainer;
    private PlayerShipHealthBehavior _playerShipHealthBehaviour;
    private List<GameObject> _barList = new List<GameObject>();

    [Header("Collectable Text")]
    [SerializeField] private TextMeshProUGUI _textGemsCount;
    [SerializeField] private TextMeshProUGUI _textCargosCount;

    [Header("Power Up")]
    [SerializeField] private GameObject _powerUpContainer;
    [SerializeField] private Image _speedImage;
    private bool _isSpeedBoostActive = false;
    private float _speedBoostDuration = 0;

    void Start()
    {
        _playerShipHealthBehaviour = GameObject.FindObjectOfType<PlayerShipHealthBehavior>();
        //UpdateCargoCounterText(CargoQuest.CollectedCargoCount, CargoQuest.TargetCargoCount);

        for (int i = 0; i < _playerShipHealthBehaviour.MaxHealth; i++)
        {
            GameObject newBar = Instantiate(_barPrefab, barContainer.transform);
            _barList.Add(newBar);
        }
    }

    private void OnEnable()
    {
        CollectableDataController.OnGemCountIncrease += UpdateGemCounterText;
        CargoQuest.OnCargoIncreased += UpdateCargoCounterText;

        SpaceshipMovement.OnSpeedBoost += UpdateSpeedPowerUp;
    }

    private void OnDisable()
    {
        CollectableDataController.OnGemCountIncrease -= UpdateGemCounterText;
        CargoQuest.OnCargoIncreased -= UpdateCargoCounterText;

        SpaceshipMovement.OnSpeedBoost -= UpdateSpeedPowerUp;
    }

    //HEALTH BAR

    public void UpdateHealth()
    {
        DrawBars();
    }

    private void DrawBars()
    {
        for (int i = 0; i < _barList.Count; i++)
        {
            if (i < _playerShipHealthBehaviour.CurrentHealth)
            {
                _barList[i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(true);
            }
            else
            {
                _barList[i].transform.Find(Constants.NAME_BAR_FULL).gameObject.SetActive(false);
            }
        }
    }


    //COLLECTABLE TEXT UPDATES

    private void UpdateGemCounterText(int count)
    {
        _textGemsCount.text = "Gems: " + count.ToString();
    }

    public void UpdateCargoCounterText(int count, int target)
    {
        _textCargosCount.text = "Cargos: " + count + " / " + target;
    }

    // POWER UPS
    private void UpdateSpeedPowerUp(float duration)
    {
        _speedBoostDuration = duration;
        if (_isSpeedBoostActive)
        {
            return;
        }
        StartCoroutine( SpeedPowerUpCoroutine() );
    }

    private IEnumerator SpeedPowerUpCoroutine()
    {
        _isSpeedBoostActive = true;
        Image newBoostImage = Instantiate(_speedImage, _powerUpContainer.transform);
        for (; _speedBoostDuration > 0; _speedBoostDuration--)
        {
            yield return new WaitForSeconds(1f);
        }
        Destroy(newBoostImage);
        _isSpeedBoostActive = false;
    }
}

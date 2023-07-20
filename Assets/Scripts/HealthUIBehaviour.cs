using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _barPrefab;
    [SerializeField] private GameObject barContainer;
    private PlayerShipHealthBehavior _playerShipHealthBehaviour;
    private List<GameObject> _barList = new List<GameObject>();

    void Start()
    {
        _playerShipHealthBehaviour = GameObject.FindObjectOfType<PlayerShipHealthBehavior>();
        
        for(int i = 0; i < _playerShipHealthBehaviour.MaxHealth; i++)
        {
            GameObject newBar = Instantiate(_barPrefab, barContainer.transform);
            _barList.Add(newBar);
        }
    }
    
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
}

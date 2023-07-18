using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;
    private PlayerShipHealthBehavior playerShipHealthBehaviour;
    List<GameObject> barList = new List<GameObject>();
    [SerializeField] private GameObject barContainer;

    // Start is called before the first frame update
    void Start()
    {
        playerShipHealthBehaviour = GameObject.FindObjectOfType<PlayerShipHealthBehavior>();
        
        for(int i = 0; i < playerShipHealthBehaviour.MAX_HEALTH; i++)
        {
            GameObject newBar = Instantiate(barPrefab, barContainer.transform);
            barList.Add(newBar);
        }
    }
    
    public void UpdateHealth()
    {
        DrawBars();
    }

    private void DrawBars()
    {
        for (int i = 0; i < barList.Count; i++)
        {
            if (i < playerShipHealthBehaviour.currentHealth)
            {
                barList[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                barList[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            
        }
    }
}

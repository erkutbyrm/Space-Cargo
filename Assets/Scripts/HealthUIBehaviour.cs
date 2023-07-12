using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;
    private PlayerShipHealthBehavior playerShipHealthSystem;
    List<GameObject> barList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerShipHealthSystem = GameObject.FindObjectOfType<PlayerShipHealthBehavior>();
        GameObject barContainer =  this.gameObject.transform.GetChild(1).gameObject;
        
        for(int i = 0; i < playerShipHealthSystem.GetMaxHealth(); i++)
        {
            GameObject newBar = Instantiate(barPrefab);
            newBar.transform.SetParent(barContainer.transform);
            barList.Add((GameObject)newBar);
        }
    }
    
    public void updateHealth()
    {
        drawBars();
    }

    void drawBars()
    {
        for (int i = 0; i < barList.Count; i++)
        {
            if (i < playerShipHealthSystem.GetCurrentHealth())
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject barPrefab;
    public ShipHealthSystem shipHealthSystem;
    private int maxHealth;
    private int currentHealth;
    List<GameObject> barList = new List<GameObject>();

    Vector2 initialBarPosition = new Vector2(85,0);
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = shipHealthSystem.maxHealth;
        currentHealth = maxHealth;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(80 + maxHealth * 20, 40);
        
        for(int i = 0; i < maxHealth; i++)
        {
            GameObject newBar = Instantiate(barPrefab);
            newBar.transform.SetParent(transform);
            newBar.transform.position = (Vector2)transform.position + initialBarPosition + new Vector2(i*20, -rectTransform.rect.height/2);
            barList.Add((GameObject)newBar);
        }

    }
    
    public void updateHealth()
    {
        drawBars();
    }

    void drawBars()
    {
        currentHealth = shipHealthSystem.currentHealth;
        for (int i = 0; i < barList.Count; i++)
        {
            if (i < currentHealth)
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gemsText;
    private int _gemCount;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(Constants.PREFS_KEY_GEM_COUNT))
        {
            _gemCount = PlayerPrefs.GetInt(Constants.PREFS_KEY_GEM_COUNT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

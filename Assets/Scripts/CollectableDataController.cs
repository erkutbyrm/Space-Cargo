using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDataController : MonoBehaviour
{
    public static event Action<int> OnGemCountIncrease;

    public Dictionary<string, int> CollectableCountDictionary;
    public int CollectedGemCount = 0;
    void Start()
    {
        CollectedGemCount = 0;
    }

    private void OnEnable()
    {
        GemBehaviour.OnGemCollected += IncreaseGemCount;
    }

    private void OnDisable()
    {
        GemBehaviour.OnGemCollected -= IncreaseGemCount;
    }

    public void IncreaseGemCount()
    {
        CollectedGemCount++;
        OnGemCountIncrease?.Invoke(CollectedGemCount);
    }
}

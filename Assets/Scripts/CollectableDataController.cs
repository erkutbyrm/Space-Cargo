using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDataController : MonoBehaviour
{
    public static event Action<int> OnGemCountIncrease;
    public static event Action<int> OnCargoCountIncrease;
    public int CollectedCargoCount = 0;
    public int TotalCargoCount = 5;

    public int CollectedGemCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        CollectedCargoCount = 0;
        CollectedGemCount = 0;
    }

    private void OnEnable()
    {
        GemBehaviour.OnGemCollected += IncreaseGemCount;
        CargoBehaviour.OnCargoCollected += IncreaseCargoCount;
    }

    private void OnDisable()
    {
        GemBehaviour.OnGemCollected -= IncreaseGemCount;
        CargoBehaviour.OnCargoCollected -= IncreaseCargoCount;
    }

    public void IncreaseGemCount()
    {
        CollectedGemCount++;
        OnGemCountIncrease?.Invoke(CollectedGemCount);
    }

    public void IncreaseCargoCount()
    {
        CollectedCargoCount++;
        OnCargoCountIncrease?.Invoke(CollectedCargoCount);
    }
}

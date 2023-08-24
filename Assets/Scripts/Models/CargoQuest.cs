using System;

public class CargoQuest : Quest
{
    public static event Action<int, int> OnCargoIncreased;

    public int TargetCargoCount { get;  set; }
    public int CollectedCargoCount { get;  set; }

    public CargoQuest(int targetCount)
    {
        TargetCargoCount = targetCount;
        CargoBehaviour.OnCargoCollected += OnCargoCollected;
    }

    ~CargoQuest()
    {
        CargoBehaviour.OnCargoCollected -= OnCargoCollected;
    }

    public override bool CheckCompleted()
    {
        IsCompleted = (CollectedCargoCount >= TargetCargoCount);
        return IsCompleted;
    }

    private void OnCargoCollected()
    {
        CollectedCargoCount++;
        OnCargoIncreased?.Invoke(CollectedCargoCount, TargetCargoCount);
    }
}

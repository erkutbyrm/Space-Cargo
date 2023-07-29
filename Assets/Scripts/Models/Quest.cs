using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public bool IsCompleted;
    public abstract bool CheckCompleted();
}

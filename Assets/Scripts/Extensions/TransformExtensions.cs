using UnityEngine;

public static class TransformExtensions
{
    public static void ClearChildren(this Transform container)
    {
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(container.GetChild(i).gameObject);
        }
    }
}

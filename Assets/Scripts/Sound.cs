using UnityEngine;

[System.Serializable]
public class Sound 
{
    [HideInInspector]
    public AudioSource Source;

    public string Name;
    public AudioClip Clip;
    public float Volume = 1;
    public bool loop;
}

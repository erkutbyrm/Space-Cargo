using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private List<Sound> Sounds;
    void Awake()
    {
        //TODO: mention singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.loop = sound.loop;
            sound.Source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void PlaySoundWithName(string name)
    {
        Sounds.Find( sound => sound.Name == name ).Source.Play();
    }
}

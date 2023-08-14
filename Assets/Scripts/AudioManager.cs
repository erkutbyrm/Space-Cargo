using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public static AudioManager Instance;

    public List<Sound> Sounds;
    // Start is called before the first frame update
    void Awake()
    {
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

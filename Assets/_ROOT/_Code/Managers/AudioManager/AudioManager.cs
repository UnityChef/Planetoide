using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<Sound> sfxSounds;

    private void Awake()
    {
        Instance = this;

        foreach (Sound sound in sfxSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
    }

    public void PlaySound(string p_name)
    {
        foreach (Sound sound in sfxSounds)
        {
            if (sound.clipName.Equals(p_name))
            {
                sound.source.Play();
                break;
            }
        }
    }
}

[Serializable]
public class Sound
{
    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource source;
}

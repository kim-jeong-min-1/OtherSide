using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEffect
{
    public static string Vibration= "Vibration";
    public static string GameClear = "GameClear";
    public static string Walk = "Walk";
}

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> sfxSounds = new Dictionary<string, AudioClip>();
    public AudioSource BGM;

    public AudioClip[] Sfxs;
    public AudioClip[] Bgms;

    private void Start()
    {
        foreach (AudioClip auido in Sfxs)
        {
            sfxSounds.Add(auido.name, auido);
        }
    }

    public void PlaySFX(string soundName, float volume = 1f, float speed = 1f, float deleteTime = 0f)
    {
        AudioSource audioSource = new GameObject("sound").AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
        audioSource.clip = sfxSounds[soundName];
        audioSource.pitch = speed;

        audioSource.Play();

        if(deleteTime != 0)
        {
            Destroy(audioSource.gameObject, deleteTime);
            return;
        }
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlayBGM(float volum = 0.8f)
    {
        BGM.volume = volum;

        BGM.clip = Bgms[0];
        BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _musicAudioSource;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioMixerGroup _effectsGroup;
    [SerializeField] private AudioMixerGroup _backgroundMusicGroup;
    [SerializeField] private AudioMixerGroup _interfaceGroup;

    [SerializeField] private SoundAudioClip[] _soundAudioClipArray;

    private GameObject _oneShotGameObject;
    private AudioSource _oneShotAudioSource;

    private const string BACKGROUNDMUSIC_VOLUME = "BackgroundMusicVolume";
    private const string EFFECTS_VOLUME = "EffectsVolume";
    private const string INTERFACE_VOLUME = "InterfaceVolume";

    new void Awake()
    {
        base.Awake();
    }

    public void PlaySound(ESound sound)
    {
        if (_oneShotGameObject == null)
        {
            _oneShotGameObject = new GameObject("One Shot Sound");
            _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
            _oneShotAudioSource.outputAudioMixerGroup = _effectsGroup;
        }
        _oneShotAudioSource.PlayOneShot(GetAudioClip(sound));

        Destroy(_oneShotGameObject, _oneShotAudioSource.clip.length);
    }

    public void PlaySound(ESound sound, Transform parent)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.SetParent(parent.transform);
        soundGameObject.transform.position = parent.position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _effectsGroup;
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 500f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();

        Destroy(soundGameObject, audioSource.clip.length);
    }

    public void PlaySound(ESound sound, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _effectsGroup;
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 500f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();

        Destroy(soundGameObject, audioSource.clip.length);
    }

    public void PlayMusic(ESound sound)
    {
        _musicAudioSource.loop = true;
        _musicAudioSource.clip = GetAudioClip(sound);
        _musicAudioSource.Play();
    }

    public void PlayMusic(ESound sound, float playbackTime)
    {
        _musicAudioSource.loop = true;
        _musicAudioSource.time = playbackTime;
        _musicAudioSource.clip = GetAudioClip(sound);
        _musicAudioSource.Play();
    }

    public void PauseMusic()
    {
        _musicAudioSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicAudioSource.UnPause();
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
    }

    public void SetMusicVolume(float value)
    {
        _mixer.SetFloat(BACKGROUNDMUSIC_VOLUME, Mathf.Log10(value) * 20);
    }

    public void SetEffectsVolume(float value)
    {
        _mixer.SetFloat(EFFECTS_VOLUME, Mathf.Log10(value) * 20);
    }

    public void SetInterfaceVolume(float value)
    {
        _mixer.SetFloat(INTERFACE_VOLUME, Mathf.Log10(value) * 20);
    }

    public AudioClip GetAudioClip(ESound sound)
    {
        foreach (SoundAudioClip soundAudioClip in _soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound" + sound + " not found");
        return null;
    }
}


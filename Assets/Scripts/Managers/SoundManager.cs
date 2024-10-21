using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioMixerGroup _effectsGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;

    [SerializeField] private SoundAudioClip[] _soundAudioClipArray;

    private GameObject _oneShotGameObject;
    private AudioSource _oneShotAudioSource;

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
        _musicSource.loop = true;
        _musicSource.clip = GetAudioClip(sound);
        _musicSource.Play();
    }

    public void PlayMusic(ESound sound, float playbackTime)
    {
        _musicSource.loop = true;
        _musicSource.time = playbackTime;
        _musicSource.clip = GetAudioClip(sound);
        _musicSource.Play();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void SetMusicVolume(float value)
    {
        _mixer.SetFloat("musicVolume", Mathf.Log10(value) * 20);
    }

    public void SetEffectsVolume(float value)
    {
        _mixer.SetFloat("effectsVolume", Mathf.Log10(value) * 20);
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


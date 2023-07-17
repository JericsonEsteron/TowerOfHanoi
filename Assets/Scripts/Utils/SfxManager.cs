using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    AudioSource _sfxAudioSource;
    [SerializeField] AudioClip _hoveSFX;
    [SerializeField] AudioClip _clickSFX;
    [SerializeField] AudioClip _moveSFX;

    private void Start()
    {
        _sfxAudioSource = GetComponent<AudioSource>();  
    }

    public void PlayHoveSFX()
    {
        _sfxAudioSource.PlayOneShot(_hoveSFX);
    }

    public void PlayClickSFX()
    {
        _sfxAudioSource.PlayOneShot(_clickSFX);
    }

    public void PlayMoveSFX()
    {
        _sfxAudioSource.PlayOneShot(_moveSFX);
    }


}

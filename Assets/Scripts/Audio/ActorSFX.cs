using System;
using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (!TryGetComponent(out _audioSource))
            throw new Exception("O componente não foi encontrado neste objeto");
    }

    public void Play(AudioClip audio)
        => _audioSource.PlayOneShot(audio);
}

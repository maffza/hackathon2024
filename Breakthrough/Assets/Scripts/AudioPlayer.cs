using NUnit.Framework;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField]
    public AudioClip[] audioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundIfNotPlaying(int clipId)
    {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClips[clipId];
                audioSource.Play();
            }
    }

    public void PlaySound(int clipId)
    {
        audioSource.clip = audioClips[clipId];
        audioSource.Play();
    }
}

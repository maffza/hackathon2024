using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("Audio source not found", this);
    }

    public void PlaySound(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length && !audioSource.isPlaying)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();  
        }
    }

    public void PlayJump()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}

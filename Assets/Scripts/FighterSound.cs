using UnityEngine;

public class FighterAudio : MonoBehaviour
{
    [Header("Fonte de Áudio Principal")]
    public AudioSource sfxSource;

    [Header("Sons de ACERTO (Impacto)")]
    public AudioClip hitNormalSound;
    [Range(0f, 1f)] public float hitNormalVolume = 1f;

    public AudioClip hitUpSound;
    [Range(0f, 1f)] public float hitUpVolume = 1f;

    public AudioClip hitDownSound;
    [Range(0f, 1f)] public float hitDownVolume = 1f;

    public void PlayHitNormalSound()
    {
        if (sfxSource != null && hitNormalSound != null) sfxSource.PlayOneShot(hitNormalSound, hitNormalVolume);
    }

    public void PlayHitUpSound()
    {
        if (sfxSource != null && hitUpSound != null) sfxSource.PlayOneShot(hitUpSound, hitUpVolume);
    }

    public void PlayHitDownSound()
    {
        if (sfxSource != null && hitDownSound != null) sfxSource.PlayOneShot(hitDownSound, hitDownVolume);
    }

}
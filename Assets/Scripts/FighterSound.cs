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

    [Header("Sons de Magia e Movimento")]
    public AudioClip throwSound;
    [Range(0f, 1f)] public float throwVolume = 1f;
    
    // NOVA VARIÁVEL: Som da carta acertando o inimigo
    public AudioClip cardHitSound; 
    [Range(0f, 1f)] public float cardHitVolume = 1f;
    
    public AudioClip jumpSound;
    [Range(0f, 1f)] public float jumpVolume = 1f;
    
    public AudioClip landSound;
    [Range(0f, 1f)] public float landVolume = 1f;

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

    public void PlayThrowSound()
    {
        if (sfxSource != null && throwSound != null) sfxSource.PlayOneShot(throwSound, throwVolume);
    }

    public void PlayCardHitSound()
    {
        if (sfxSource != null && cardHitSound != null) sfxSource.PlayOneShot(cardHitSound, cardHitVolume);
    }

    public void PlayJumpSound()
    {
        if (sfxSource != null && jumpSound != null) sfxSource.PlayOneShot(jumpSound, jumpVolume);
    }

    public void PlayLandSound()
    {
        if (sfxSource != null && landSound != null) sfxSource.PlayOneShot(landSound, landVolume);
    }
}
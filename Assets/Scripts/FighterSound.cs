using UnityEngine;

public class FighterAudio : MonoBehaviour
{
    [Header("Configurações de Áudio")]
    public AudioSource sfxSource;
    public AudioClip hitSound;

    public void PlayHitSound()
    {
        if (sfxSource != null && hitSound != null)
        {
            sfxSource.PlayOneShot(hitSound);
        }
    }
}
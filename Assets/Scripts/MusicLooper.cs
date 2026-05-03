using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    private AudioSource bgmSource;

    [Header("Configurações do Loop (Em Segundos)")]
    public float tempoDeInicio = 8.0f;
    public float tempoDeFim = 29.9f;

    private bool introTocada = false;

    void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!introTocada && bgmSource.time >= tempoDeInicio)
        {
            introTocada = true;
        }

        if (introTocada)
        {

            if (bgmSource.time >= tempoDeFim)
            {
                bgmSource.time = tempoDeInicio;
            }

            else if (bgmSource.time < tempoDeInicio)
            {
                bgmSource.time = tempoDeInicio;
            }
        }
    }
}
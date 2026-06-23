using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public Animator transition;
    public AudioSource menuSoundSource;
    public AudioClip toBattleSound;
    [Range(0f, 1f)] public float toBattleVolume = 1f;
    public float transitionTime = 1f;
    public float soundTime = 2f;

    public void OnStartClick()
    {
        StartCoroutine(LoadLevel("Combate"));
    }

    /*public void ToMenuClick()
    {
        StartCoroutine(LoadLevel("Menu"));
    }*/

    IEnumerator LoadLevel(string cena)
    {
        if (menuSoundSource != null && toBattleSound != null) menuSoundSource.PlayOneShot(toBattleSound, toBattleVolume);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(soundTime);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(cena);
    }

    public void OnExitCLick()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

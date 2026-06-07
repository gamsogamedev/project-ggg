using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    public void OnStartClick()
    {
        StartCoroutine(LoadLevel("Combate"));
    }

    public void ToMenuClick()
    {
        StartCoroutine(LoadLevel("Menu"));
    }

    IEnumerator LoadLevel(string cena)
    {
        transition.SetTrigger("Start");

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

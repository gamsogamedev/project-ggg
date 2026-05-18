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
        SceneManager.LoadScene("Combate");
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Combate");
    }

    public void OnExitCLick()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

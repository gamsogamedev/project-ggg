using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void ToRematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        // SceneManager.LoadScene("Menu");
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

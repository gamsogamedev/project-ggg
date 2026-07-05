using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject _mainMenuFirst;
    public GameObject _optionsFirst;

    [SerializeField] private GameObject _mainMenuGO;
    [SerializeField] private GameObject _optionsMenuGO;

    private void Start()
    {
        _mainMenuGO.SetActive(true);
        _optionsMenuGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    public void OpenMainMenuHandle()
    {
        _mainMenuGO.SetActive(true);
        _optionsMenuGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    public void onOptionsPress()
    {
        OpenOptionsMenuHandle();
    }

    public void OpenOptionsMenuHandle()
    {
        _mainMenuGO.SetActive(false);
        _optionsMenuGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_optionsFirst);
    }

    public void onOptionsBackPress()
    {
        OpenMainMenuHandle();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

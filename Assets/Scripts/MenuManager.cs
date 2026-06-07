using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject _mainMenuFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
}

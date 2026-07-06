using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rematchManager : MonoBehaviour
{
    public GameObject _rematchFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_rematchFirst);
    }
}

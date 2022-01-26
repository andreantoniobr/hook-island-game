using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    None  = -1,
    LevelCleared,
}

[System.Serializable]
public struct Screen
{
    public ScreenType screenType;
    public GameObject screenObject;    
}

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Screen[] screens;

    private void SetInactiveScreenObject(GameObject screenObject)
    {
        if (IsActiveScreen(screenObject))
        {
            screenObject.SetActive(false);
        }
    }

    private void SetActiveScreenObject(GameObject screenObject)
    {
        if (!IsActiveScreen(screenObject))
        {
            screenObject.SetActive(true);
        }
    }

    private bool IsActiveScreen(GameObject screenObject)
    {
        return screenObject && screenObject.activeSelf;
    }

    public void SetActiveScreen(ScreenType screenType)
    {
        foreach (Screen screen in screens)
        {
            if (screen.screenType == screenType)
            {
                SetActiveScreenObject(screen.screenObject);
            }
            else
            {
                SetInactiveScreenObject(screen.screenObject);
            }
        }
    }

    public void SetInactiveAllScreens()
    {
        foreach (Screen screen in screens)
        {
            SetInactiveScreenObject(screen.screenObject);
        }
    }
}

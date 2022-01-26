using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScreenController screenController;    

    private static UIManager instance;

    public static UIManager Instance => instance;
    public ScreenController ScreenController => screenController;

    private void Awake()
    {
        SetThisInstance();
    }

    private void SetThisInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }    
}

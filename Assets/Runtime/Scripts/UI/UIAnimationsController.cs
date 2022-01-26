using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HookIsland.Animations;

public class UIAnimationsController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float timeFlash = 0.5f;
    [SerializeField] private float timeFade = 0.5f;

    private UIAnimations animations;
    private static UIAnimationsController _instance;

    public static UIAnimationsController Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        image.enabled = false;
        animations = new UIAnimations();
    }

    public void PlayFlashUI()
    {
        StartCoroutine(animations.Flash(image, timeFlash, Color.white));
    }

    public IEnumerator PlayFadeIn()
    {
        yield return StartCoroutine(animations.FadeIn(image, timeFade, Color.black));
    }

    public IEnumerator PlayFadeOut()
    {
        StartCoroutine(animations.FadeOut(image, timeFade, Color.black));
        yield return null;
    }
}

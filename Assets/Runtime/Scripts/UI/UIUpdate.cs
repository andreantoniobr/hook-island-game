using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameInput swipeInput;
    [SerializeField] private PlayerController playerController;

    private void LateUpdate()
    {
        if (swipeInput)
        {
            text.text = "swiping:" + swipeInput.IsSwiping + " direction:" + swipeInput.SwipeDirection + " position:" + swipeInput.transform.position + " IsRetracting:" + playerController.IsRetracting;
        }
    }
}

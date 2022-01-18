using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoint : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;

    public Vector3 TargetPosition => targetPosition;

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}

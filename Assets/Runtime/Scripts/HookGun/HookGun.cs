using System;
using UnityEngine;

public class HookGun : MonoBehaviour
{
    [SerializeField] private GameObject hook;

    [Header("Line")]
    [SerializeField] private Vector3 lineInitialPoint;
    [SerializeField] private float lineSizeIncrement;
    [SerializeField] private float hookPositionAjust;

    private LineRenderer line;
    private int linePositionCount = 2;

    public LineRenderer Line => line;
    
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        SetInactive();
    }
   
    private void SetInactiveHook()
    {
        if (hook)
        {
            hook.SetActive(false);
        }
    }

    private void SetActiveHook()
    {
        if (hook)
        {
            hook.SetActive(true);
        }
    }

    private void SetActiveLine()
    {
        line.enabled = true;
        line.positionCount = linePositionCount;
    }

    private void SetInactiveLine()
    {
        line.enabled = false;
    }    

    private void UpdatePointPosition(int linePointIndex, Vector3 position, Vector3 direction)
    {
        line.SetPosition(linePointIndex, position + direction * lineSizeIncrement);
    }

    public void SetActive()
    {
        SetActiveLine();
        SetActiveHook();
    }

    public void SetInactive()
    {
        SetInactiveLine();
        SetInactiveHook();
    }

    public void UpdateInitialPointPosition(Vector3 position)
    {
        int initialPointIndex = 0;
        UpdatePointPosition(initialPointIndex, position + lineInitialPoint, Vector3.zero);        
    }    

    public void UpdateEndPointPosition(Vector3 position, Vector3 direction)
    {
        int endPointIndex = 1;
        UpdatePointPosition(endPointIndex, position, direction);
    }

    public void UpdateHookPosition(Vector3 position, Vector3 direction)
    {
        if (hook)
        {
            hook.transform.position = position + direction * hookPositionAjust;
        }
    }

    public void UpdateHookRotation(Vector3 direction)
    {
        float hookRotation = DirectionUtility.Angle(direction);
        hook.transform.rotation = Quaternion.Euler(Vector3.forward * hookRotation);
    }
}

using UnityEngine;

public class HookGun : MonoBehaviour
{
    private LineRenderer line;
    private int linePositionCount = 2;

    public LineRenderer Line => line;
    
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void SetActiveLine()
    {
        line.enabled = true;
        line.positionCount = linePositionCount;
    }

    public void SetInactiveLine()
    {
        line.enabled = false;
    }

    public void UpdateInitialPointPosition(Vector3 position)
    {
        int initialPointIndex = 0;
        UpdatePointPositon(initialPointIndex, position);
    }

    public void UpdateEndPointPosition(Vector3 position)
    {
        int endPointIndex = 1;
        UpdatePointPositon(endPointIndex, position);
    }

    public void UpdatePointPositon(int linePointIndex, Vector3 position)
    {
        line.SetPosition(linePointIndex, position);
    }
}

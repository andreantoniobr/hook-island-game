using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None = -1,
    Center,
    Left,
    Right,
    Up,
    Down
}

public struct HookPointObject 
{
    public Direction Position;
}

public class Hook : MonoBehaviour
{
    [SerializeField] private HookPoint hookPoint;
    [SerializeField] private Target target;

    [Header("Hook Points")]
    [SerializeField] private Direction[] directions;
    [SerializeField] private List<HookPoint> hookpoints;

    private void Start()
    {
        foreach (Direction direction in directions)
        {
            InstantiateHookPoint(direction);
        }
    }

    private void Update()
    {
        if (target && target.IsColliding)
        {
            foreach (HookPoint hookpoint in hookpoints)
            {
                //HookPointColliderController hookPointColliderController = hookPoint.GetComponent<HookPointColliderController>();
                //hookPointColliderController.SetInactiveCollider();
            }
        }
    }

    private void InstantiateHookPoint(Direction direction)
    {
        HookPoint hookPointObject = Instantiate(hookPoint, transform);
        if (hookPointObject)
        {
            hookpoints.Add(hookPointObject);
            Vector3 targetPosition = target.transform.position;

            if (direction == Direction.Center)
            {
                hookPointObject.transform.position = targetPosition;
            }
            else
            {
                hookPointObject.transform.position = targetPosition + GetHookPointPosition(direction, hookPointObject.gameObject);
            }            
            hookPointObject.SetTargetPosition(targetPosition);
        }        
    }

    private Vector3 GetHookPointPosition(Direction direction, GameObject gameObject)
    {
        float position = 0f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            Vector3 spriteSize = spriteRenderer.bounds.size;
            if (direction == Direction.Left || direction == Direction.Right)
            {
                position = spriteSize.x;
            }
            else if (direction == Direction.Up || direction == Direction.Down)
            {
                position = spriteSize.y;
            }
        }
        return position * GetVectorOfDirection(direction);
    }

    private static Vector3 GetVectorOfDirection(Direction direction)
    {
        Vector3 vectorOfDirection;
        switch (direction)
        {
            case Direction.Left:
                vectorOfDirection = Vector3.left;
                break;
            case Direction.Right:
                vectorOfDirection = Vector3.right;
                break;
            case Direction.Up:
                vectorOfDirection = Vector3.up;
                break;
            case Direction.Down:
                vectorOfDirection = Vector3.down;
                break;
            default:
                vectorOfDirection = Vector3.zero;
                break;
        }
        return vectorOfDirection;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
        foreach (Direction direction in directions)
        {
            Gizmos.color = Color.yellow;
            Vector3 targetPosition = target.transform.position;
            Vector3 gizmoPositon = targetPosition + GetHookPointPosition(direction, target.transform.gameObject);
            Gizmos.DrawWireCube(gizmoPositon, new Vector3(1, 1, 1));
        }
    }
}

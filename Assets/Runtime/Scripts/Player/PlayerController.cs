using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private HookGun hookGun;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    [SerializeField] private bool isGrappling = false;
    [SerializeField] private bool isRetracting = false;
    [SerializeField] private Vector2 targetPosition;

    Vector2 direction;

    public bool IsGrappling => isGrappling;
    public bool IsRetracting => isRetracting;
    public Vector2 Direction => direction;

    public static event Action OnRetractingEvent;

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
    }

    private void Update()
    {
        if (gameInput.IsSwiping && !isGrappling)
        {
            direction = gameInput.Direction;
            StartGrapple();
        }
    }

    private void FixedUpdate()
    {
        if (isRetracting)
        {
            UpdatePosition();
            hookGun.UpdateInitialPointPosition(transform.position);
        }
    }

    private void UpdatePosition()
    {
        Vector2 currentPosition = transform.position;
        Vector2 position = Vector2.Lerp(currentPosition, targetPosition, grappleSpeed * Time.fixedDeltaTime);

        if (IsInTarget(currentPosition))
        {
            position = targetPosition;
            isRetracting = false;
            isGrappling = false;
            hookGun.SetInactiveLine();
        }

        transform.position = position;
    }

    private bool IsInTarget(Vector2 currentPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) < 0.05f;
    }

    private void StartGrapple()
    {  
        //Debug.Log(direction);

        if (!isRetracting)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, layerMask);
            if (hit)
            {
                HookPoint hookPoint = hit.transform.GetComponent<HookPoint>();
                if (hookPoint != null && hit.collider != null)
                {
                    isGrappling = true;
                    targetPosition = hookPoint.TargetPosition;
                    Debug.Log(hit.transform.name);
                    hookGun.SetActiveLine();
                    StartCoroutine(Grapple());
                }
            }            
        }
    }

    IEnumerator Grapple()
    {
        float time = 10;

        hookGun.UpdateInitialPointPosition(transform.position);
        hookGun.UpdateEndPointPosition(transform.position);

        Vector2 newPos;

        for (float t = 0; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, targetPosition, t / time);
            hookGun.UpdateInitialPointPosition(transform.position);
            hookGun.UpdateEndPointPosition(newPos);
            yield return null;
        }

        hookGun.UpdateEndPointPosition(targetPosition);
        OnRetracting();
    }

    private void OnRetracting()
    {
        isRetracting = true;
        OnRetractingEvent?.Invoke();
    }
}

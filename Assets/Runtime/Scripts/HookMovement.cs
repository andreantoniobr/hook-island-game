using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour {
    
    private LineRenderer line;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    private bool isGrappling = false;

    [SerializeField] private bool isRetracting = false;
    public bool IsRetracting => isRetracting;

    private Vector2 target;

    [SerializeField] private GameInput gameInput;

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (gameInput.IsSwiping && !isGrappling)
        {
            StartGrapple();
        }        
    }

    private void FixedUpdate()
    {
        if (isRetracting)
        {
            UpdateHookPosition();
            UpdateInitialPointOfLinePosition();
        }
    }    

    private void UpdateHookPosition()
    {
        Vector2 currentPosition = transform.position;        
        Vector2 targetPosition = Vector2.Lerp(currentPosition, target, grappleSpeed * Time.fixedDeltaTime);

        if (IsInTarget(currentPosition))
        {
            targetPosition = target;
            isRetracting = false;
            isGrappling = false;
            line.enabled = false;
        }

        transform.position = targetPosition;
    }

    private void UpdateInitialPointOfLinePosition()
    {
        line.SetPosition(0, transform.position);
    }

    private bool IsInTarget(Vector2 currentPosition)
    {
        return Vector2.Distance(currentPosition, target) < 0.5f;
    }

    private void StartGrapple() {
        Vector2 direction = gameInput.Direction;
        if (direction != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);
            if (hit.collider != null)
            {
                isGrappling = true;
                target = hit.point;
                line.enabled = true;
                line.positionCount = 2;
                StartCoroutine(Grapple());
            }
        }        
    }

    IEnumerator Grapple() {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position); 

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime) {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        
        line.SetPosition(1, target);
        isRetracting = true;
    }
}

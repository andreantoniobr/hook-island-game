using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    private bool isGrappling = false;

    [SerializeField] private bool isRetracting = false;
    public bool IsRetracting => isRetracting;

    [SerializeField] private Vector2 targetPosition;

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isGrappling)
        {
            StartGrapple();
        }
    }

    private void FixedUpdate()
    {
        if (isRetracting)
        {
            UpdateHookPosition();
        }
    }

    private void UpdateHookPosition()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.Lerp(currentPosition, targetPosition, grappleSpeed * Time.fixedDeltaTime);

        if (IsInTarget(currentPosition))
        {
            newPosition = targetPosition;
            isRetracting = false;
            isGrappling = false;
        }

        transform.position = newPosition;
    }

    private bool IsInTarget(Vector2 currentPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) < 0.05f;
    }

    private void StartGrapple()
    {
        Vector2 direction = Vector2.zero;

#if UNITY_ANDROID
        direction = gameInput.Direction;
#endif


#if UNITY_EDITOR
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
#endif





        Debug.Log(direction);

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
                    //transform.position = target;
                    StartCoroutine(Grapple());
                }
            }            
        }
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, targetPosition, t / time);
            yield return null;
        }

        isRetracting = true;
    }
}

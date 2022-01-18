using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private bool isColliding;
    public bool IsColliding => isColliding;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetPlayerComponent(other))
        {
            isColliding = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (GetPlayerComponent(other))
        {
            isColliding = false;           
        }
    }

    private PlayerMovement GetPlayerComponent(Collider2D other)
    {
        return other.GetComponent<PlayerMovement>();
    }
}

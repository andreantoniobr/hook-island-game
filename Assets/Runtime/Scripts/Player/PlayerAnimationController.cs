using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private Animator animator;    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (animator && playerController)
        {
            animator.SetFloat(PlayerAnimationConstants.InputX, playerController.Direction.x);
            animator.SetFloat(PlayerAnimationConstants.InputY, playerController.Direction.y);
            //animator.SetBool(PlayerAnimationConstants.IsWalking, playerController.IsWalking);
        }
    }
}

using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Target : MonoBehaviour
{
    [SerializeField] private Sprite targetActive;
    [SerializeField] private Sprite targetNotActive;
    [SerializeField] private GameObject[] particlesOnActive;

    [Header("Target state")]
    [SerializeField] private bool isActive;
    [SerializeField] private bool isColliding;

    private SpriteRenderer spriteRenderer;    

    public bool IsActive => isActive;
    public bool IsColliding => isColliding;
    public static event Action OnTargetActivedEvent;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = targetNotActive;
    }

    private void Start()
    {
        LevelManager.Instance.AddTarget(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetPlayerComponent(other))
        {
            isColliding = true;
            SetActive();
        }
    }    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (GetPlayerComponent(other))
        {
            isColliding = false;
        }
    }

    private void SetActive()
    {
        if (!isActive)
        {
            isActive = true;
            spriteRenderer.sprite = targetActive;
            OnTargetActivedEvent?.Invoke();
            InstantiateEffects();
        }
    }

    private void InstantiateEffects()
    {
        if (particlesOnActive.Length > 0)
        {
            foreach (GameObject particle in particlesOnActive)
            {
                Instantiate(particle, transform);
            }            
        }
    }

    private PlayerController GetPlayerComponent(Collider2D other)
    {
        return other.GetComponent<PlayerController>();
    }
}

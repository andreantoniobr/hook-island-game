using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLoader : MonoBehaviour
{
    [SerializeField] private GameObject pillarSprite;

    private void Start()
    {
        if (pillarSprite)
        {
            Instantiate(pillarSprite, transform);
        }
    }
}

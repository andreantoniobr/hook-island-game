using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointColliderController : MonoBehaviour
{
    [SerializeField] private Collider2D coll;


    


    public void SetActiveCollider()
    {
        if (coll)
        {
            coll.enabled = true;
        }
    }

    public void SetInactiveCollider()
    {
        if (coll)
        {
            coll.enabled = false;
        }
    }    
}

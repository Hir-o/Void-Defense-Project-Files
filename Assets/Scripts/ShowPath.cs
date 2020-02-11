using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
    public Transform target;

    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = new Color(1f, 0.58f, 0.42f);
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeLine : MonoBehaviour
{
    public int segments = 50;
    public float radius = 15;

    private LineRenderer line;

    private float x, y, z, angle;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    public void CreatePoints()
    {
        angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius; 

            if (gameObject.activeSelf)
                line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}
using UnityEngine;
using System.Collections;
using System;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class drawCirc : MonoBehaviour
{
    [Range(1, 50)] public int segments = 50;
    [Range(1, 5)] public float xRadius = 2;
    [Range(1, 5)] public float yRadius = 2;
    [Range(0.001f, 5f)] public float width = 0.05f;
    public bool controlBothXradiusYradius = false;
    public bool draw = true;

    [SerializeField] private LineRenderer line;

    private void Start()
    {
        if (!line) line = GetComponent<LineRenderer>();

        CreatePoints();
    }

    public void CreatePoints()
    {
        line.enabled = true;
        line.widthMultiplier = width;
        line.useWorldSpace = false;
        line.widthMultiplier = width;
        line.positionCount = segments + 1;

        float x;
        float y;

        var angle = 20f;
        var points = new Vector3[segments + 1];
        for (int i = 0; i < segments + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;

            points[i] = new Vector3(x, 0f, y);

            angle += (380f / segments);
        }

        // it's way more efficient to do this in one go!
        line.SetPositions(points);
    }

#if UNITY_EDITOR
    private float prevXRadius, prevYRadius;
    private int prevSegments;
    private float prevWidth;

    private void OnValidate()
    {
        // Can't set up our line if the user hasn't connected it yet.
        if (!line) line = GetComponent<LineRenderer>();
        if (!line) return;

        if (!draw)
        {
            // instead simply disable the component
            line.enabled = false;
        }
        else
        {
            // Otherwise re-enable the component
            // This will simply re-use the previously created points
            line.enabled = true;

            if (xRadius != prevXRadius || yRadius != prevYRadius || segments != prevSegments || width != prevWidth)
            {
                CreatePoints();

                // Cache our most recently used values.
                prevXRadius = xRadius;
                prevYRadius = yRadius;
                prevSegments = segments;
                prevWidth = width;
            }

            if (controlBothXradiusYradius)
            {
                yRadius = xRadius;
            }
        }
    }
#endif
}


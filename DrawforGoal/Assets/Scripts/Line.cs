using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line 
{
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider;

    public Line(LineRenderer lineRenderer,EdgeCollider2D edgeCollider)
    {
         _lineRenderer = lineRenderer;
         _edgeCollider = edgeCollider;  
    }
    public void DrawLine(int index, Vector2 position)
    {
        _lineRenderer.SetPosition(0, position);
        _lineRenderer.SetPosition(1, position);
    }
}

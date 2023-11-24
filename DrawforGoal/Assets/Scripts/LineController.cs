using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _line;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;

    [SerializeField] private List<Vector2> touchPositionList;

    private IInput _input;
    private void Awake()
    {
        _input = new PcInput();
    }

    private void Update()
    {
        if (_input.InputButtonDown)        
            DrawLine();
        
        if (_input.InputButtonMove)
        {
            Vector2 touchPosition = GetScreenPoint();
            if (Vector2.Distance(touchPosition, touchPositionList[^1]) > .1f)
                UpdateLine(touchPosition);
        }
    }
    private void DrawLine()
    {
        _line = Instantiate(_linePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = _line.GetComponent<LineRenderer>();
        edgeCollider2D = _line.GetComponent<EdgeCollider2D>();
        touchPositionList.Clear();

        touchPositionList.Add(GetScreenPoint());
        touchPositionList.Add(GetScreenPoint());
        lineRenderer.SetPosition(0, touchPositionList[0]);
        lineRenderer.SetPosition(1, touchPositionList[1]);
        edgeCollider2D.points = touchPositionList.ToArray();
    }
    private void UpdateLine(Vector2 getTouchPosition)
    {
        touchPositionList.Add(getTouchPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, getTouchPosition);
        edgeCollider2D.points = touchPositionList.ToArray();
    }
    private Vector2 GetScreenPoint()
    {
       return Camera.main.ScreenToWorldPoint(_input.InputPosition);
    }
}

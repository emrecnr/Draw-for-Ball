using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _line;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider2D;

    [SerializeField] private List<Vector2> touchPositionList;
    [SerializeField] private List<LineRenderer> currentDrawedLine;

    public System.Action<int> OnDrawLine;

    private int _maxDrawLine = 3;
    private int _currentDrawLineCount;
    public int CurrentDrawLineCount => _currentDrawLineCount;

    private IInput _input;
    private void Awake()
    {
        _input = new PcInput();
    }
    private void Start()
    {
        _currentDrawLineCount = _maxDrawLine;
    }

    private void Update()
    {
        if (!GameManager.Instance.canStart || _currentDrawLineCount < 0) return;
        if (_input.InputButtonDown)
            DrawLine();

        if (_input.InputButtonMove)
        {
            Vector2 touchPosition = GetScreenPoint();
            if (Vector2.Distance(touchPosition, touchPositionList[^1]) > .1f)
                UpdateLine(touchPosition);
        }
        if (_input.InputButtonUp)
        {
            AudioManager.Instance.PlayDrawSFX(false);
        }
    }
    private void DrawLine()
    {
        if (_maxDrawLine <= 0) return;

        _currentDrawLineCount--;
        OnDrawLine?.Invoke(_currentDrawLineCount);
        
        _line = LinePool.Instance.Get();
        _line.transform.position = Vector2.zero;
        _line.positionCount = 2;
        _line.gameObject.SetActive(true);
        currentDrawedLine.Add(_line);
        lineRenderer = _line;
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
        AudioManager.Instance.PlayDrawSFX(true);
        touchPositionList.Add(getTouchPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, getTouchPosition);
        edgeCollider2D.points = touchPositionList.ToArray();
    }
    private Vector2 GetScreenPoint()
    {
        return Camera.main.ScreenToWorldPoint(_input.InputPosition);
    }
    public void Continue()
    {
        foreach (var drawedLine in currentDrawedLine)
        {
            LinePool.Instance.Set(drawedLine);
        }
        currentDrawedLine.Clear();
        _currentDrawLineCount = _maxDrawLine;
        OnDrawLine?.Invoke(_currentDrawLineCount);
    }
}

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField] private Transform _backGround;
    [SerializeField] private LineRenderer _linePrefab;
    private List<LineRenderer> _lines = new();

    private Color? _color;
    private bool _isDrawing;

    private LineRenderer _line;
    private Vector3 _lastPosition;


    public void Update()
    {
        if (_color == null) return;

        if (_isDrawing && Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }

        if (!_isDrawing && Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (_isDrawing)
        {
            var mousePosition = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hitInfo)) return;
            
            if (_line.positionCount > 0)
            {
                _lastPosition = _line.GetPosition(_line.positionCount - 1);

                if (Vector3.Distance(_lastPosition, hitInfo.point) < 0.5)
                {
                    return;
                }
            }


            _line.positionCount += 1;
            _line.SetPosition(_line.positionCount - 1, hitInfo.point);
        }
    }

    public void ChooseColor(Color color)
    {
        _color = color;
    }

    [UsedImplicitly]
    public void ClearLines()
    {
        foreach (var lineRenderer in _lines)
        {
            Destroy(lineRenderer.gameObject);
        }

        _lines.Clear();
    }

    private void StartDrawing()
    {
        if (_color == null)
        {
            return;
        }

        _line = Instantiate(_linePrefab);
        _line.material.color = _color.Value;
        _line.sortingOrder = _lines.Count;
        _lines.Add(_line);
        _isDrawing = true;
    }

    private void EndDrawing()
    {
        _isDrawing = false;
    }
}
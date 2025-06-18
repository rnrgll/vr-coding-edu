using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR.Interaction.Toolkit;

public class DragLine : MonoBehaviour
{
    private LineRenderer _line;
    private Transform _start;
    private Transform _end;
    private Color _lineColor;

    public void Init(Transform from, Transform to, Define.ColorName lineColor)
    {
        Debug.Log("DragLine Init called with from: " + from.name + ", to: " + to.name);
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _start = from;
        _end = to;

        SetLineColor(lineColor);
        _line.enabled = true;
    }

    void Update()
    {
        if (_line.enabled && _start && _end)
        {
            _line.SetPosition(0, _start.position);
            _line.SetPosition(1, _end.position);
        }

    }

    public void DestroyLine()
    {
        Destroy(gameObject);
    }

    public void SetLineColor(Define.ColorName colorName)
    {
        _lineColor = Define.ColorDict.TryGetValue(colorName, out var lineColor) ? lineColor : Color.gray;
        _line.startColor = _line.endColor = _lineColor;

    }

}

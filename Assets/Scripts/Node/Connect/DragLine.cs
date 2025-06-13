using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DragLine : MonoBehaviour
{
    private LineRenderer _line;
    private Transform _start;
    private Transform _end;

    public void Init(Transform from, Transform to)
    {
        Debug.Log("DragLine Init called with from: " + from.name + ", to: " + to.name);
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _start = from;
        _end = to;
        _line.enabled = true;
    }

    void Update()
    {
        if (_line.enabled && _start && _end)
        {
            Debug.Log("Updating line positions: " + _start.position + " to " + _end.position);
            _line.SetPosition(0, _start.position);
            _line.SetPosition(1, _end.position);
        }

    }

    public void DestroyLine()
    {
        Destroy(gameObject);
    }

}

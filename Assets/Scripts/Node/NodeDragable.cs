using System;
using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class NodeDragable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _lastPosition;
    private XRGrabInteractable _grab;
    public UnityEvent OnMoved = new();


    void Awake() => Init();
    void Update() => UpdatePosition();
    
    private void Init()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _lastPosition = transform.position;
    }

    private void OnEnable()
    {
        _grab.selectEntered.AddListener(OnGrabStart);
        _grab.selectExited.AddListener(OnGrabEnd);
    }
    private void OnDisable()
    {
        _grab.selectEntered.RemoveListener(OnGrabStart);
        _grab.selectExited.RemoveListener(OnGrabEnd);
    }

    void OnGrabStart(SelectEnterEventArgs args)
    {
        if (Manager.Node.DeleteMode)
        {
            DeleteNode();
            return;
        }

        _rigidbody.isKinematic = false; // 이동 가능
    }

    void OnGrabEnd(SelectExitEventArgs args)
    {
        _rigidbody.isKinematic = true; // 이동 고정
    }

    private void UpdatePosition()
    {
        if (_grab.isSelected && transform.position != _lastPosition)
        {
            OnMoved?.Invoke();
            _lastPosition = transform.position;
        }
    }

    private void DeleteNode()
    {
        BaseNode node = GetComponent<BaseNode>();
        if (node == null) return;
        foreach (Port inputPort in node.inputPorts)
        {
            if(inputPort.IsConnected)
                inputPort.Disconnect();
        }
        foreach (Port outputPort in node.outputPorts)
        {
            if(outputPort.IsConnected)
                outputPort.Disconnect();
        }
        Debug.Log("모든 포트 연결 해제 후 게임 오브젝트 파괴");
        Destroy(gameObject);
    }
}

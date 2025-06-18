using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PortDragable : MonoBehaviour
{
    [Header("Drag Line & Handle")]
    public DragLine dragLinePrefab;
    [SerializeField] private Transform fixedBase;
    [SerializeField] private Transform dragHandle;
    private Transform dragHandleOriginPos;


    [Header("Port Logic")]
    [SerializeField] private Port myPort; //현재 포트
    private List<Port> detectedPorts = new();
    private DragLine curentLine;
    private XRGrabInteractable grabInteractable;
    private Rigidbody _rigidbody;
    
    private void Awake() => Init();
    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        myPort = GetComponentInParent<Port>();

        dragHandle = transform;
        dragHandleOriginPos = myPort.HandlePos;

        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGragEnd);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabStart);
        grabInteractable.selectExited.RemoveListener(OnGragEnd);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (myPort.IsConnected) return;
        Debug.Log("ontriggerenter");
        if (other.gameObject.layer == (int)Define.Layer.Interaction && other.TryGetComponent<Port>(out var port))
        {
           
            if (!detectedPorts.Contains(port)) {
                Debug.Log(port.PortName + " 포트 감지됨");
                detectedPorts.Add(port);
            }
                
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Interaction && other.TryGetComponent<Port>(out var port))
        {
            detectedPorts.Remove(port);
        }
    }


    private void OnGrabStart(SelectEnterEventArgs args)
    {
        Debug.Log("OnGrabStart");
        _rigidbody.isKinematic = false;
        
        myPort.Disconnect();

        if (curentLine != null) return;
        curentLine = Instantiate(dragLinePrefab, fixedBase.position, Quaternion.identity, myPort.transform);
        curentLine.name = "Line";

        Debug.Log("curentLine != null line 생성");
        curentLine.Init(fixedBase, dragHandle, myPort.PortColor);
    }

    private void OnGragEnd(SelectExitEventArgs args)
    {
        Debug.Log("onGragEnd");
        _rigidbody.isKinematic = true;
        
        if (curentLine == null) return;
        Debug.Log("curentLine != null");


        bool isConnected = false;
        //감지한 포트가 있는 경우
        if (detectedPorts.Count!=0)
        {
            Debug.Log("감지된 포트 있음");
            //가장 가까운 포트 찾기
            Port detecedPort = GetClosestPort();
          
            //연결 시도
            // if (myPort != null && detecedPort != null && myPort.CanConntectTo(detecedPort))
            if (myPort != null && detecedPort != null)
            {
                Debug.Log($"[{myPort.name}] → [{detecedPort.name}] 연결 가능 여부: {myPort.CanConntectTo(detecedPort)}");

                if (myPort.CanConntectTo(detecedPort))
                {
                    Debug.Log(detecedPort.gameObject.name + " 포트가 가장 가까움");
                    //기존 연결 해제, 이벤트 구독 해제
                    if (myPort.IsConnected)
                    {
                        myPort.Disconnect();
                        
                        myPort.ParentNode.GetComponent<NodeDragable>().OnMoved.RemoveListener(UpdateHandlePosition);
                        myPort.ConnectedPort.ParentNode.GetComponent<NodeDragable>().OnMoved.RemoveListener(UpdateHandlePosition);

                        
                        Debug.Log("기존 연결 해제, 이벤트 구독 해제");
                    }

                    //새로운 연결 설정, 이벤트 구독
                    myPort.Connect(detecedPort);
                    
                    myPort.ParentNode.GetComponent<NodeDragable>().OnMoved.AddListener(UpdateHandlePosition);
                    detecedPort.ParentNode.GetComponent<NodeDragable>().OnMoved.AddListener(UpdateHandlePosition);

                    
                    dragHandle.position = detecedPort.HandlePos.position; //연결된 위치로 변경
                    isConnected = true;
                    Debug.Log("포트 연결");
                    return;
                }
            }
        }

        //감지된 포트가 없는 경우 OR 연결 불가능한 경우 ---
        // 연결 상태를 초기화한다.
        //myPort.Disconnect();
        // dragHandle을 원래 위치로 복구시킨다.
        if (!isConnected)
        {
            dragHandle.position = dragHandleOriginPos.position;
            curentLine.DestroyLine();
            curentLine = null;
            //detecedPort = null;
        }
      
    }

    private Port GetClosestPort()
    {
        Port closest = null;
        float minDistance = float.MaxValue;

        foreach (var port in detectedPorts)
        {
            if (port == null || port.IsConnected) continue; // 연결된 포트는 제외
            float distance = Vector3.Distance(dragHandle.position, port.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = port;
            }
        }
        return closest;
    }

    private void UpdateHandlePosition()
    {
        Debug.Log("Drag Handle Position Update");
        dragHandle.position = myPort.ConnectedPort.HandlePos.position;
    }
}
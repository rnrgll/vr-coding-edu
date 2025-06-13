using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PortInteractor : MonoBehaviour
{
    [Header("Drag Line")]
    public DragLine dragLinePrefab;
    [SerializeField] private Transform fixedBase;
    [SerializeField] private Transform dragHandle;
    private Transform dragHandleOriginPos;

    private DragLine curentLine;
    private XRGrabInteractable grabInteractable;

    [Header("Port Detection")]
    [SerializeField] private Port myPort; //현재 포트
    private List<Port> detectedPorts = new();

    private void Awake()
    {
        myPort = GetComponentInParent<Port>();

        dragHandle = transform;
        dragHandleOriginPos = myPort.HandlePos;

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGragEnd);

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
        if (curentLine == null) return;
        Debug.Log("curentLine != null");
        //감지한 포트가 있는 경우
        if (detectedPorts.Count!=0)
        {
            Debug.Log("감지된 포트 있음");
            //가장 가까운 포트 찾기
            Port detecedPort = GetClosestPort();
            Debug.Log(detecedPort.gameObject.name + " 포트가 가장 가까움");
            //연결 시도
            if (myPort != null && detecedPort != null && myPort.CanConntectTo(detecedPort))
            {
                //기존 연결 해제
                if(myPort.IsConnected)
                    myPort.Disconnect();
                myPort.Connect(detecedPort);
                transform.position = detecedPort.HandlePos.position; //연결된 위치로 변경
                Debug.Log("포트 연결");
                return;
            }
        }

        //감지된 포트가 없는 경우 OR 연결 불가능한 경우 ---
        // 연결 상태를 초기화한다.
        //myPort.Disconnect();
        // dragHandle을 원래 위치로 복구시킨다.
        dragHandle.position = dragHandleOriginPos.position;
        curentLine.DestroyLine();
        curentLine = null;
        //detecedPort = null;
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
}
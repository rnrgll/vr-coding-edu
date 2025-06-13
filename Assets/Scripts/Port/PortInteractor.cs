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
    [SerializeField] private Port myPort; //���� ��Ʈ
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
                Debug.Log(port.PortName + " ��Ʈ ������");
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

        Debug.Log("curentLine != null line ����");
        curentLine.Init(fixedBase, dragHandle, myPort.PortColor);
    }

    private void OnGragEnd(SelectExitEventArgs args)
    {
        Debug.Log("onGragEnd");
        if (curentLine == null) return;
        Debug.Log("curentLine != null");
        //������ ��Ʈ�� �ִ� ���
        if (detectedPorts.Count!=0)
        {
            Debug.Log("������ ��Ʈ ����");
            //���� ����� ��Ʈ ã��
            Port detecedPort = GetClosestPort();
            Debug.Log(detecedPort.gameObject.name + " ��Ʈ�� ���� �����");
            //���� �õ�
            if (myPort != null && detecedPort != null && myPort.CanConntectTo(detecedPort))
            {
                //���� ���� ����
                if(myPort.IsConnected)
                    myPort.Disconnect();
                myPort.Connect(detecedPort);
                transform.position = detecedPort.HandlePos.position; //����� ��ġ�� ����
                Debug.Log("��Ʈ ����");
                return;
            }
        }

        //������ ��Ʈ�� ���� ��� OR ���� �Ұ����� ��� ---
        // ���� ���¸� �ʱ�ȭ�Ѵ�.
        //myPort.Disconnect();
        // dragHandle�� ���� ��ġ�� ������Ų��.
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
            if (port == null || port.IsConnected) continue; // ����� ��Ʈ�� ����
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
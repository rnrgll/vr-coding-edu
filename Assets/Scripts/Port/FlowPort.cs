using UnityEngine;
using static Define;

public class FlowPort : Port
{
    [SerializeField]
    private PortDirection direction = PortDirection.In;
  
    
    private void Awake()
    {
        //��Ʈ ���� ����
        Direction = direction;
        PortName = $"Flow {Direction.ToString()} Port"; // ��Ʈ �̸� ����
        //TODO : �θ� ��� ����
        // ParentNode = GetComponentInParent<BaseNode>(); // �θ� ��� ���� (�ʿ��)

        ConnectedPort = null; // �ʱ� ���� ��Ʈ�� ����
    }



    public override bool CanConntectTo(Port otherPort)
    {
        return otherPort is FlowPort && Direction != otherPort.Direction;
    }


    public override void Connect(Port other)
    {
        if (CanConntectTo(other))
        {
            ConnectedPort = other;
            other.SetConnectedPort(this); // ��� ��Ʈ�� ���� ����(����� ����)
        }
    }
    public override void Disconnect()
    {
        if (ConnectedPort != null)
        {

            ConnectedPort.SetConnectedPort(null); // ��� ��Ʈ�� ���� ����
            ConnectedPort = null; // �ڽ� ���� ����
            Debug.Log($"���� ���� �ʱ�ȭ�� : {IsConnected}");

        }
    }
}
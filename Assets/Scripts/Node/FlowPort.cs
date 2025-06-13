using static Define;

public class FlowPort : Port
{
    //������
    public FlowPort(BaseNode parentNode, PortDirection direction) : base(parentNode, direction)
    {
       PortName = $"Flow {direction.ToString()} Port"; // ��Ʈ �̸� ����
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
         
        }
    }
}
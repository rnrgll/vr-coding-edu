using static Define;

public class FlowPort : Port
{
    //생성자
    public FlowPort(BaseNode parentNode, PortDirection direction) : base(parentNode, direction)
    {
       PortName = $"Flow {direction.ToString()} Port"; // 포트 이름 설정
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
            other.SetConnectedPort(this); // 상대 포트의 연결 설정(양방향 설정)
        }
    }
    public override void Disconnect()
    {
        if (ConnectedPort != null)
        {

            ConnectedPort.SetConnectedPort(null); // 상대 포트의 연결 해제
            ConnectedPort = null; // 자신 연결 해제
         
        }
    }
}
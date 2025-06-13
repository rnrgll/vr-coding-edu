using static Define;

public abstract class Port
{
    //포트 정보
    public string PortName { get; protected set; }
    public BaseNode ParentNode { get; protected set; }
    public Port ConnectedPort { get; protected set; }
    public PortDirection Direction { get; protected set; }

    public bool IsConnected => ConnectedPort != null;

    //추상 메소드 : 연결 / 연결 해제
    public abstract bool CanConntectTo(Port otherPort); // 연결 가능한지 확인하기
    public abstract void Connect(Port other); //연결하기
    public abstract void Disconnect(); //연결 해제하기


    //생성자
    protected Port(BaseNode parentNode, PortDirection direction)
    {
        ParentNode = parentNode;
        Direction = direction;
        ConnectedPort = null;
    }

    // 연결 포트 설정
    public void SetConnectedPort(Port port)
    {
        ConnectedPort = port;
    }
}
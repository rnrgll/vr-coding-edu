using UnityEngine;
using static Define;

public class FlowPort : Port
{
    [SerializeField]
    private PortDirection direction = PortDirection.In;
  
    
    private void Awake()
    {
        //포트 방향 설정
        Direction = direction;
        PortName = $"Flow {Direction.ToString()} Port"; // 포트 이름 설정
        //TODO : 부모 노드 설정
        // ParentNode = GetComponentInParent<BaseNode>(); // 부모 노드 설정 (필요시)

        ConnectedPort = null; // 초기 연결 포트는 없음
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
            Debug.Log($"연결 상태 초기화함 : {IsConnected}");

        }
    }
}
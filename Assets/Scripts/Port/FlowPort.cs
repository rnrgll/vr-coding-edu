using UnityEngine;
using static Define;

public class FlowPort : Port
{
    [SerializeField] private PortDirection direction = PortDirection.In;


    private void Awake() => Init();

    protected override void Init()
    {
        base.Init();
        
        //포트 방향 설정
        Direction = direction;
        PortName = $"Flow {Direction.ToString()} Port"; // 포트 이름 설정
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
            other.SetConnectedPort(this); // ��� ��Ʈ�� ���� ����(����� ����)
            OnConnectionChanged?.Invoke(true);
        }

        
    }
    public override void Disconnect()
    {
        if (ConnectedPort != null)
        {

            ConnectedPort.SetConnectedPort(null); // ��� ��Ʈ�� ���� ����
            ConnectedPort = null; // �ڽ� ���� ����
            Debug.Log($"���� ���� �ʱ�ȭ�� : {IsConnected}");
            OnConnectionChanged?.Invoke(false);
        }
    }
}
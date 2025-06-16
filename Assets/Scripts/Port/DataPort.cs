using UnityEngine;
using static Define;

public class DataPort<T> : Port
{
    [SerializeField] protected T value;
    public virtual T Value => value;
    public DataPort<T> ConnectedDataPort => ConnectedPort as DataPort<T>;

    private void Awake() => Init();

    protected override void Init()
    {
        base.Init();
        
        PortName = $"Data {Direction.ToString()} Port"; // 포트 이름 설정
        ConnectedPort = null; // 초기 연결 포트는 없음

    }

    public object GetRawData() => Value;  // 타입 상관없이 object로 받기



    public override bool CanConntectTo(Port otherPort)
    {
        return (otherPort is DataPort<T>||otherPort is FlexibleDataInPort) && Direction != otherPort.Direction;
    }

    public override void Connect(Port other)
    {
        if (CanConntectTo(other))
        {
            ConnectedPort = other;
            other.SetConnectedPort(this); // 상대 포트의 연결 설정(양방향 설정)
            
            OnConnectionChanged?.Invoke(true);
        }
    }
    public override void Disconnect()
    {
        if (ConnectedPort != null)
        {

            ConnectedPort.SetConnectedPort(null); // 상대 포트의 연결 해제
            ConnectedPort = null; // 자신 연결 해제
            Debug.Log($"연결 상태 초기화함 : {IsConnected}");
            
            OnConnectionChanged?.Invoke(false);
        
        }
    }
}


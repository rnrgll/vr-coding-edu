using UnityEngine;
using static Define;

public abstract class Port : MonoBehaviour
{
    //포트 정보
    [field: SerializeField] public string PortName { get; protected set; }
     [field: SerializeField] public BaseNode ParentNode { get; protected set; } = null;
    [field: SerializeField]  public Port ConnectedPort { get; protected set; } = null;
    public PortDirection Direction { get; protected set; }

    public Transform HandlePos;
    public Define.ColorName PortColor;

    public bool IsConnected => ConnectedPort != null;

    //추상 메소드 : 연결 / 연결 해제
    public abstract bool CanConntectTo(Port otherPort); // 연결 가능한지 확인하기
    public abstract void Connect(Port other); //연결하기
    public abstract void Disconnect(); //연결 해제하기


    // 연결 포트 설정
    public void SetConnectedPort(Port port)
    {
        ConnectedPort = port;
    }
}
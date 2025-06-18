using System;
using System.Collections;
using UnityEngine;

public class FlexibleDataInPort : Port
{
    [SerializeField] protected object value;
    public object Value => value;
    public Type CurrentType { get; private set; }   
    private void Awake() => Init();
    protected override void Init()
    {
        base.Init();
        Direction = Define.PortDirection.In; // 입력 포트로 설정
        PortName = $"Data {Direction.ToString()} Port"; // 포트 이름 설정
    }


    public override bool CanConntectTo(Port otherPort)
    {
        if (otherPort == null || otherPort.Direction == Direction) return false;
        return true;
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
    public IEnumerator FetchData()
    {
        if (ConnectedPort == null)
        {
            Debug.Log("연결된 포트가 없습니다.");
            yield break;
        }
        
        if (ConnectedPort.GetType().GetGenericTypeDefinition() != typeof(DataOutPort<>))
        {
            Debug.LogError("연결된 포트가 예상 타입과 일치하지 않습니다.");
            yield break;
        }
        
        if (ConnectedPort is DataOutPort<string> str)
        {
            yield return str.PrepareValue();
            value = str.Value;
            CurrentType = typeof(string);
        }
        else if (ConnectedPort is DataOutPort<int> i)
        {
            yield return i.PrepareValue();
            value = i.Value;
            CurrentType = typeof(int);
        }
        else if (ConnectedPort is DataOutPort<bool> b)
        {
            yield return b.PrepareValue();
            value = b.Value;
            CurrentType = typeof(bool);
        }
        else
        {
            Debug.LogError("지원되지 않는 포트 타입.");
            yield break;
        }

    }
}

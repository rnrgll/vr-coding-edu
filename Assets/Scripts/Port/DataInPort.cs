using System.Collections;
using UnityEngine;
using static Define;

public class DataInPort<T> : DataPort<T>
{

    protected override void Init()
    {
        base.Init();
        Direction = PortDirection.In; // 입력 포트로 설정
    }

    public IEnumerator FetchData()
    {
        if (ConnectedPort == null)
        {
            Debug.Log("연결된 포트가 없습니다.");
            yield break;
        }

        if (ConnectedPort is not DataOutPort<T> dataOutPort)
        {
            Debug.LogError("연결된 포트가 예상 타입과 일치하지 않습니다.");
            yield break;
        }
        
        yield return dataOutPort.PrepareValue(); // 값 준비
        value = dataOutPort.Value;
        
    }
}


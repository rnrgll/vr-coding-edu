using System.Collections;
using Node;
using UnityEngine;
using static Define;

public class DataOutPort<T> : DataPort<T>
{
    public bool IsReady { get; private set; } = false;
    protected override void Init()
    {
        base.Init();
        Direction = PortDirection.Out; // 입력 포트로 설정
    }
    public void SetValue(T value)
    {
        this.value = value;
        IsReady = true;
        ParentNode.errorFlag = false;
    }

    public IEnumerator PrepareValue()
    {
        if (!IsReady || ParentNode.errorFlag)
        {
            if (ParentNode is IDataNode dataNode)
                yield return dataNode.ProcessData();
            else
            {
                Debug.LogError($"[{gameObject.name}] ParentNode가 IDataNode를 구현하지 않았습니다.");
                yield break;
            }
        }
        
        yield return null;
    }
}


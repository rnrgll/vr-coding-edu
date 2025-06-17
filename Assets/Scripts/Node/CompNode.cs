using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Node;
using UnityEngine;

public class CompNode : BaseNode, IDataNode
{
    public enum ComparisonMethod
    {
        Equal,          // ==
        NotEqual,       // !=
        Greater,        // >
        GreaterEqual,   // >=
        Less,           // <
        LessEqual       // <=
    }
    
    [SerializeField] private ComparisonMethod method;

    private Port dataInPort1;
    private Port dataInPort2;
    private DataOutPort<bool> dataOutPort;
    
    // Start is called before the first frame update
    void Start()
    {
        dataInPort1 = inputPorts[0];
        dataInPort2 = inputPorts[1];
        dataOutPort = outputPorts[0] as DataOutPort<bool>;
    }
    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
        {
            Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
            Manager.Node.SetCompileError("port is not connected");
            yield break;
        }
        
        if (method == ComparisonMethod.Equal || method == ComparisonMethod.NotEqual)
        {
            if (dataInPort1 is FlexibleDataInPort port1 && dataInPort2 is FlexibleDataInPort port2)
            {
                yield return port1.FetchData();
                yield return port2.FetchData();

                // 타입이 달라 비교 불가
                if (port1.CurrentType != port2.CurrentType)
                {
                    Debug.Log("[TypeError] 서로 다른 타입의 포트는 비교할 수 없습니다.");
                    //todo:타입 에러
                    // Debug.Log("포트 간 자료형 다름");
                    // NodeManager.Instance.SetCompileError(true, "data type");
                    yield break;
                }
                bool result = CompareEquality(port1.Value, port2.Value, port1.CurrentType);
                dataOutPort.SetValue(result);
            }
            else
            {
                Debug.LogError("[PortError] ==, != 연산에는 FlexibleDataInPort가 필요합니다.");
                yield break;
            }
        }
        else
        {
            // 나머지 연산은 int 포트만 허용
            if (dataInPort1 is DataInPort<int> port1 && dataInPort2 is DataInPort<int> port2)
            {
                yield return port1.FetchData();
                yield return port2.FetchData();

                int a = port1.Value;
                int b = port2.Value;

                bool result = CompareRelational(a, b);
                dataOutPort.SetValue(result);
            }
            else
            {
                Debug.LogError("[TypeError] 비교 연산은 int 타입만 지원됩니다.");
                yield break;
            }
        }
        
    }
    private bool CompareEquality(object v1, object v2, Type type)
    {
        return method switch
        {
            ComparisonMethod.Equal => Equals(v1, v2),
            ComparisonMethod.NotEqual => !Equals(v1, v2),
            _ => false
        };
    }

    private bool CompareRelational(int a, int b)
    {
        return method switch
        {
            ComparisonMethod.Greater => a > b,
            ComparisonMethod.GreaterEqual => a >= b,
            ComparisonMethod.Less => a < b,
            ComparisonMethod.LessEqual => a <= b,
            _ => false
        };
        
    }
}

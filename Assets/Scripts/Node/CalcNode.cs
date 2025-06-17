using System.Collections;
using System.Collections.Generic;
using Managers;
using Node;
using UnityEngine;

public class CalcNode : BaseNode, IDataNode
{
    public enum Operation { Add, Subtract, Multiply, Divide, Modulo }

    [Header("Operation Type")]
    [SerializeField] private Operation calcType;

    private DataInPort<int> dataInPort1;
    private DataInPort<int> dataInPort2;
    private DataOutPort<int> dataOutPort;

    private int result;
    
    void Start()
    {
        dataInPort1 = inputPorts[0] as DataInPort<int>;
        dataInPort2 = inputPorts[0] as DataInPort<int>;
        dataOutPort = outputPorts[0] as DataOutPort<int>;

    }


    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
        {
            Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
            Manager.Node.SetCompileError("port is not connected");
            yield break;
        }
        
        yield return dataInPort1.FetchData();
        yield return dataInPort2.FetchData();

        int a = dataInPort1.Value;
        int b = dataInPort2.Value;

        bool hasError = false;
        
        switch (calcType)
        {
            case Operation.Add:
                result = a + b;
                break;
            case Operation.Subtract:
                result = a - b;
                break;
            case Operation.Multiply:
                result = a * b;
                break;
            case Operation.Divide:
                if (b == 0)
                {
                    hasError = true;
                    // NodeManager.Instance.SetCompileError(true, "0으로 나눌 수 없습니다.");
                }
                else result = a / b;
                break;
            case Operation.Modulo:
                if (b == 0)
                {
                    hasError = true;
                    // NodeManager.Instance.SetCompileError(true, "0으로 나머지 연산을 할 수 없습니다.");
                }
                else result = a % b;
                break;
        }

        if (!hasError)
        {
            dataOutPort.SetValue(result);
        }
        
    }
}
